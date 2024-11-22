using EcoHome.AuthService.Domain.Interfaces;
using EcoHome.AuthService.Infrastructure.Data;
using EcoHome.AuthService.Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EcoHome.AuthService.Infrastructure.IoC
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddProjectDependencies(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<AuthDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IConsumptionLogRepository, ConsumptionLogRepository>();
            services.AddScoped<IAlertRepository, AlertRepository>();

            services.AddScoped<ConsumptionLogService>(); 
            services.AddScoped<DeviceService>(); 
            services.AddScoped<UserService>(); 
            services.AddScoped<AlertService>();

            return services;
        }
    }
}
