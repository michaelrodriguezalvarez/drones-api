using Abp.Application.Services.Dto;

namespace Drones.Medications.Dto
{
    public class PagedMedicationsResultRequestDto : PagedResultRequestDto, ISortedResultRequest
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public bool Descending { get; set; }
    }
}
