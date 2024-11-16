using EcoHome.AuthService.Domain.Entities.Common;

namespace EcoHome.AuthService.Domain.Entities
{
    public class UserEntity : BaseEntity
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }

        public ICollection<DeviceEntity> Devices { get; set; }
        public ICollection<AlertEntity> Alerts { get; set; } 

    }
}
