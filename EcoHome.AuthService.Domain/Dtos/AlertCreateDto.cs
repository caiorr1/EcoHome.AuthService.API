using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EcoHome.AuthService.Domain.Entities.Common;

namespace EcoHome.AuthService.Domain.Dtos
{
    public class AlertCreateDto
    {
        public string Message { get; set; }
        public int UserId { get; set; }
        public StatusEnum Status { get; set; }
    }
}
