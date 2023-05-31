using Abp.Application.Services;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;
using Drones.Drones.Dto;
using Drones.Models;
using System.Linq;

namespace Drones.Drones
{
    public class DroneAppService : AsyncCrudAppService<Drone, DroneDto, long, PagedDronesResultRequestDto, DroneDto, DroneDto>, IDroneAppService
    {
        public DroneAppService(IRepository<Drone, long> repository) : base(repository)
        {
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
    }
}
