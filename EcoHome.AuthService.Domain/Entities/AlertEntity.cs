using EcoHome.AuthService.Domain.Entities.Common;

namespace EcoHome.AuthService.Domain.Entities
{
    public class AlertEntity : BaseEntity
    {
        public string Message { get; set; } 
        public StatusEnum Status { get; set; } 

        public int UserId { get; set; }
        public UserEntity User { get; set; }
    }
}
