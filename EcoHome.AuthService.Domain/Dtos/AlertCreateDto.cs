using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Dtos
{
    public class AlertCreateDto
    {
        public string Message { get; set; }
        public int UserId { get; set; }
    }
}
