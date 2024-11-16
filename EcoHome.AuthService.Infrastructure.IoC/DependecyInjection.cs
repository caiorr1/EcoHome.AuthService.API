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
            // Configuração do DbContext (Oracle)
            services.AddDbContext<AuthDbContext>(options =>
                options.UseOracle(configuration.GetConnectionString("DefaultConnection")));

            // Registro de Repositórios
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IDeviceRepository, DeviceRepository>();
            services.AddScoped<IConsumptionLogRepository, ConsumptionLogRepository>();
            services.AddScoped<IAlertRepository, AlertRepository>();

            // Registro dos Serviços
            services.AddScoped<ConsumptionLogService>(); // Adicionando o serviço de logs de consumo
            services.AddScoped<DeviceService>(); // Adicionando o serviço de dispositivos
            services.AddScoped<UserService>(); // Adicionando o serviço de usuários
            services.AddScoped<AlertService>(); // Adicionando o serviço de alertas


            return services;
        }
    }
}
