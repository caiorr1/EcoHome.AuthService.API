using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Dtos
{
    public class ConsumptionLogCreateDto
    {
        public int DeviceId { get; set; }
        public decimal Consumption { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
