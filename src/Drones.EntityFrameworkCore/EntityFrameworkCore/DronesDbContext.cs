using Microsoft.EntityFrameworkCore;
using Abp.Zero.EntityFrameworkCore;
using Drones.Authorization.Roles;
using Drones.Authorization.Users;
using Drones.MultiTenancy;
using Drones.Models;
using System.Reflection.Emit;

namespace Drones.EntityFrameworkCore
{
    public class DronesDbContext : AbpZeroDbContext<Tenant, Role, User, DronesDbContext>
    {
        /* Define a DbSet for each entity of the application */
        public virtual DbSet<Drone> Drones { get; set; }
        public virtual DbSet<Medication> Medications { get; set; }
        public virtual DbSet<DroneMedication> DronesMedications { get; set; }

        public DronesDbContext(DbContextOptions<DronesDbContext> options)
            : base(options)
        { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Drone>(entity =>
            {                
                entity.HasKey(e => e.Id); entity.Property(e => e.Id).UseIdentityColumn(seed: 0, increment: 1);
                entity.Property(e => e.SerialNumber).HasMaxLength(100);
                entity.Property(e => e.Weight).HasColumnType("decimal(14, 2)");
            });

            modelBuilder.Entity<Medication>(entity =>
            {
                entity.HasKey(e => e.Id); entity.Property(e => e.Id).UseIdentityColumn(seed: 0, increment: 1);
                entity.Property(e => e.Weight).HasColumnType("decimal(14, 2)");
            });

            modelBuilder.Entity<DroneMedication>(entity =>
            {
                entity.HasKey(e => e.Id); entity.Property(e => e.Id).UseIdentityColumn(seed: 0, increment: 1);
                entity.HasOne(d => d.Drone)
                    .WithMany(p => p.DronesMedications)
                    .HasForeignKey(d => d.DroneId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DronesMedications_Drone");
                entity.HasOne(d => d.Medication)
                    .WithMany(p => p.DronesMedications)
                    .HasForeignKey(d => d.MedicationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_DronesMedications_Medication");
            });
        }
    }
}
