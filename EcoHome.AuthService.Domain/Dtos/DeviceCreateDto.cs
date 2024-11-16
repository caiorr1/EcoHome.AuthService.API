using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Dtos
{
    public class DeviceCreateDto
    {
        public string Name { get; set; }
        public decimal PowerConsumption { get; set; }
        public string Location { get; set; }
        public int UserId { get; set; }
    }
}
