using EcoHome.AuthService.Domain.Dtos.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoHome.AuthService.Domain.Dtos
{
    public class UserResponseDto : BaseDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
    }
}
