using Abp.Application.Services;
using Drones.Drones.Dto;
using Drones.Drones;
using Drones.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drones.Medications.Dto;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Abp.Linq.Extensions;

namespace Drones.Medications
{
    public class MedicationAppService : AsyncCrudAppService<Medication, MedicationDto, long, PagedMedicationsResultRequestDto, MedicationDto, MedicationDto>, IMedicationAppService
    {
        public MedicationAppService(IRepository<Medication, long> repository) : base(repository)
        {
        }

        protected override IQueryable<Medication> CreateFilteredQuery(PagedMedicationsResultRequestDto input)
        {
            string keyword = input?.Keyword?.ToLower();
            return Repository.GetAll()
                .WhereIf(!input.Keyword.IsNullOrWhiteSpace(), x =>
                    x.Name.ToLower().Contains(keyword) ||
                    x.Weight.ToString().ToLower().Contains(keyword) ||
                    x.Code.ToLower().Contains(keyword));
        }
        protected override IQueryable<Medication> ApplySorting(IQueryable<Medication> query, PagedMedicationsResultRequestDto input)
        {
            return input.Sorting switch
            {
                "Name" => input.Descending ? query.OrderByDescending(x => x.Name.ToLower()) : query.OrderBy(x => x.Name.ToLower()),
                "Weight" => input.Descending ? query.OrderByDescending(x => x.Weight.ToString().ToLower()) : query.OrderBy(x => x.Weight.ToString().ToLower()),
                "Code" => input.Descending ? query.OrderByDescending(x => x.Code.ToLower()) : query.OrderBy(x => x.Code.ToLower()),
                _ => query.OrderBy(x => x.Name),
            };
        }
    }
}
