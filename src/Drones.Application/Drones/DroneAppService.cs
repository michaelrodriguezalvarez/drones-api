using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Authorization;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Abp.Timing;
using Drones.Drones.Dto;
using Drones.Medications.Dto;
using Drones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Drones.Drones
{
    [AbpAuthorize]
    public class DroneAppService : AsyncCrudAppService<Drone, DroneDto, long, PagedDronesResultRequestDto, DroneDto, DroneDto>, IDroneAppService
    {
        private readonly IRepository<DroneMedication, long> _droneMedicationRepository;
        private readonly IRepository<Medication, long> _medicationRepository;
        public DroneAppService(
            IRepository<Drone, long> repository,
            IRepository<DroneMedication, long> droneMedicationRepository,
            IRepository<Medication, long> medicationRepository
            ) : base(repository)
        {
            _droneMedicationRepository = droneMedicationRepository;
            _medicationRepository = medicationRepository;
        }

        protected override IQueryable<Drone> CreateFilteredQuery(PagedDronesResultRequestDto input)
        {
            string keyword = input?.Keyword?.ToLower();
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.SerialNumber.ToLower().Contains(keyword) ||
                    x.Model.ToLower().Contains(keyword) ||
                    x.Weight.ToString().ToLower().Contains(keyword) ||
                    x.BatteryCapacity.ToString().ToLower().Contains(keyword) || 
                    x.State.ToLower().Contains(keyword));
        }
        protected override IQueryable<Drone> ApplySorting(IQueryable<Drone> query, PagedDronesResultRequestDto input)
        {
            return input.Sorting switch
            {
                "SerialNumber" => input.Descending ? query.OrderByDescending(x => x.SerialNumber.ToLower()) : query.OrderBy(x => x.SerialNumber.ToLower()),
                "Model" => input.Descending ? query.OrderByDescending(x => x.Model.ToLower()) : query.OrderBy(x => x.Model.ToLower()),
                "Weight" => input.Descending ? query.OrderByDescending(x => x.Weight.ToString().ToLower()) : query.OrderBy(x => x.Weight.ToString().ToLower()),
                "BatteryCapacity" => input.Descending ? query.OrderByDescending(x => x.BatteryCapacity.ToString().ToLower()) : query.OrderBy(x => x.BatteryCapacity.ToString().ToLower()),
                "State" => input.Descending ? query.OrderByDescending(x => x.State.ToLower()) : query.OrderBy(x => x.State.ToLower()),
                _ => query.OrderBy(x => x.SerialNumber),
            };
        }


        [RemoteService(false)]       

        public override Task<DroneDto> CreateAsync(DroneDto input)
        {
            return base.CreateAsync(input);
        }

        // NOTE: registering a drone
        // Requirements:
        // Prevent the drone from being in LOADING state if the battery level is below 25%;
        [UnitOfWork]
        public async Task<bool> Register(DroneDto input)
        {
            using (UnitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                if (input.BatteryCapacity <= 25 && input.State.Equals("LOADING"))
                {
                    return false;
                }
                else
                {
                    var drone = ObjectMapper.Map<Drone>(input);
                    drone.TenantId = AbpSession.TenantId;
                    drone.CreatorUserId = AbpSession.UserId;
                    drone.CreationTime = Clock.Now;
                    drone = await Repository.InsertAsync(drone);
                    if (drone == null)
                    {
                        return false;
                    }
                }
                return true;
            }                
        }

        // NOTE: loading a drone with medication items
        // Requirements:
        // Prevent the drone from being loaded with more weight that it can carry
        // Prevent the drone from being in LOADING state if the battery level is below 25%;
        [UnitOfWork]
        public async Task<bool> Load(LoadDronesRequestDto input)
        {
            using (UnitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                var drone = await Repository.GetAsync(input.DroneId);
                drone.State = "LOADING";
                await CurrentUnitOfWork.SaveChangesAsync();
                if (drone.BatteryCapacity >= 25)
                {
                    decimal totalWeigthToLoad = 0;

                    foreach (var medicationId in input.MedicationItems)
                    {
                        var item = await _medicationRepository.GetAsync(medicationId);
                        totalWeigthToLoad += item.Weight;
                    }

                    if (drone.Weight <= totalWeigthToLoad)
                    {
                        drone.State = "LOADED";
                        await CurrentUnitOfWork.SaveChangesAsync();
                        return false;
                    }

                    await _droneMedicationRepository.DeleteAsync(x => x.DroneId == input.DroneId);

                    foreach (var medicationId in input.MedicationItems)
                    {
                        var medication = await _medicationRepository.GetAsync(medicationId);
                        _ = await _droneMedicationRepository.InsertAsync(new DroneMedication()
                        {
                            TenantId = AbpSession.TenantId,
                            DroneId = drone.Id,
                            MedicationId = medication.Id,
                            Drone = drone,
                            Medication = medication
                        });
                    }
                    drone.State = "LOADED";
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return true;
                }
                else
                {
                    drone.State = "LOADED";
                    await CurrentUnitOfWork.SaveChangesAsync();
                    return false;
                }
            }

        }

        // NOTE: checking loaded medication items for a given drone
        [UnitOfWork]
        public async Task<PagedResultDto<MedicationDto>> CheckLoaded(CheckDronesRequestDto input)
        {
            using (UnitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                string keyword = input?.Keyword?.ToLower();
                var queryMedications = (from dm in _droneMedicationRepository.GetAll()
                                         join m in _medicationRepository.GetAll() on dm.MedicationId equals m.Id
                                         where dm.DroneId == input.DroneId
                                         select (m)).WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                        x.Name.ToLower().Contains(keyword) ||
                        x.Weight.ToString().ToLower().Contains(keyword) ||
                    x.Code.ToLower().Contains(keyword));

               

                var loadedMedicationItems = queryMedications.Skip(input.SkipCount).Take(input.MaxResultCount);

                return new PagedResultDto<MedicationDto>(queryMedications.Count(), ObjectMapper.Map<List<MedicationDto>>(loadedMedicationItems));
            }
        }

        // NOTE: checking available drones for loading
        [UnitOfWork]
        public async Task<PagedResultDto<DroneDto>> CheckAvailables()
        {
            // TODO
            using (UnitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                List<Drone> availableDrones = new List<Drone>();
                var drones = await Repository.GetAllListAsync();
                drones.ForEach((drone) =>
                {
                    var totalWeightLoaded = (from dm in _droneMedicationRepository.GetAll()
                                             join m in _medicationRepository.GetAll() on dm.MedicationId equals m.Id
                                             where dm.DroneId == drone.Id
                                             select (m.Weight)).Sum();
                    if (drone.Weight > totalWeightLoaded)
                    {
                        availableDrones.Add(drone);
                    }
                });
                return new PagedResultDto<DroneDto>(availableDrones.Count(), ObjectMapper.Map<List<DroneDto>>(availableDrones));
            }
        }

        // NOTE: check drone battery level for a given drone
        [UnitOfWork]
        public async Task<int> CheckBatteryLevel(CheckDronesRequestDto input)
        {
            using (UnitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                var drone = await Repository.GetAsync(input.DroneId);
                return drone.BatteryCapacity;
            }
        }

        [UnitOfWork]
        public async Task<bool> Unload(UnloadDroneDto input)
        {
            using (UnitOfWorkManager.Current.SetTenantId(AbpSession.TenantId))
            {
                var droneMedication = await _droneMedicationRepository.FirstOrDefaultAsync(x =>
                                    x.MedicationId == input.MedicationId && x.DroneId == input.DroneId
                                    );
                if (droneMedication is null)
                {
                    return false;
                }
                else
                {
                    await _droneMedicationRepository.DeleteAsync(droneMedication);
                    return true;
                }
            }
        }
    }
}
