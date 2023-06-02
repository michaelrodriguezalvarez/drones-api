using Abp.Application.Services.Dto;

namespace Drones.Drones.Dto
{
    public class CheckAvailablesRequestDto : PagedResultRequestDto, ISortedResultRequest
    {
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public bool Descending { get; set; }
    }
}
