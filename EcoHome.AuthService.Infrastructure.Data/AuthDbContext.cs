using EcoHome.AuthService.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace EcoHome.AuthService.Infrastructure.Data
{
    public class AuthDbContext : DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : base(options) { }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<DeviceEntity> Devices { get; set; }
        public DbSet<AlertEntity> Alerts { get; set; }
        public DbSet<ConsumptionLogEntity> ConsumptionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuração da UserEntity
            modelBuilder.Entity<UserEntity>().HasKey(u => u.Id);

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.Email)
                .IsRequired()
                .HasMaxLength(100);

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.PasswordHash)
                .IsRequired();

            modelBuilder.Entity<UserEntity>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("SYSTIMESTAMP AT TIME ZONE 'UTC'")
                .IsRequired();


            // Configuração da DeviceEntity
            modelBuilder.Entity<DeviceEntity>().HasKey(d => d.Id);

            modelBuilder.Entity<DeviceEntity>()
                .Property(d => d.Name)
                .IsRequired()
                .HasMaxLength(50);

            modelBuilder.Entity<DeviceEntity>()
                .Property(d => d.PowerConsumption)
                .IsRequired()
                .HasPrecision(18, 2); // Corrigido para especificar a precisão e escala

            modelBuilder.Entity<DeviceEntity>()
                .HasOne(d => d.User)
                .WithMany(u => u.Devices)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração da AlertEntity
            modelBuilder.Entity<AlertEntity>().HasKey(a => a.Id);

            modelBuilder.Entity<AlertEntity>()
                .Property(a => a.Message)
                .IsRequired()
                .HasMaxLength(200);

            modelBuilder.Entity<AlertEntity>()
                .HasOne(a => a.User)
                .WithMany(u => u.Alerts)
                .HasForeignKey(a => a.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Configuração da ConsumptionLogEntity
            modelBuilder.Entity<ConsumptionLogEntity>().HasKey(cl => cl.Id);

            modelBuilder.Entity<ConsumptionLogEntity>()
                .Property(cl => cl.Consumption)
                .IsRequired()
                .HasPrecision(18, 2); // Corrigido para especificar a precisão e escala

            modelBuilder.Entity<ConsumptionLogEntity>()
                .HasOne(cl => cl.Device)
                .WithMany(d => d.ConsumptionLogs)
                .HasForeignKey(cl => cl.DeviceId)
                .OnDelete(DeleteBehavior.Cascade);

            // Chamando o método base
            base.OnModelCreating(modelBuilder);
        }
    }
}
