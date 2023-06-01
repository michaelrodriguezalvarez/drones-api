using Abp.Application.Services.Dto;

namespace Drones.Drones.Dto
{
    public class CheckDronesRequestDto : PagedResultRequestDto, ISortedResultRequest
    {
        public long DroneId { get; set; }
        public string Keyword { get; set; }
        public string Sorting { get; set; }
        public bool Descending { get; set; }
    }
}
