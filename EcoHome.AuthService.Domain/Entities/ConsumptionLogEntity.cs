using EcoHome.AuthService.Domain.Entities.Common;

namespace EcoHome.AuthService.Domain.Entities
{
    public class ConsumptionLogEntity : BaseEntity
    {
        public int DeviceId { get; set; }
        public DeviceEntity Device { get; set; }
        public DateTime Timestamp { get; set; } 
        public decimal Consumption { get; set; }
    }
}
