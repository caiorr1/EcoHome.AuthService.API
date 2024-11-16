using EcoHome.AuthService.Domain.Entities.Common;

namespace EcoHome.AuthService.Domain.Entities
{
    public class DeviceEntity : BaseEntity
    {
        public string Name { get; set; }
        public decimal PowerConsumption { get; set; } 
        public string Location { get; set; } 

        public StatusEnum Status { get; set; }

        public int UserId { get; set; }
        public UserEntity User { get; set; }

        public ICollection<ConsumptionLogEntity> ConsumptionLogs { get; set; }
    }
}
