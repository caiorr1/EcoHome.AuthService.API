using EcoHome.AuthService.Domain.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Dtos
{
    public class DeviceResponseDto : BaseDto
    {
        public string Name { get; set; }
        public decimal PowerConsumption { get; set; }
        public string Location { get; set; }
        public string Status { get; set; }
    }
}
