using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<Specification> Specifications { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<VehicleLike> VehicleLikes { get; set; }
        public DbSet<VehicleView> VehicleViews { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Discriminador para usuarios
            modelBuilder.Entity<User>().HasDiscriminator<UserRol>("Rol")
                .HasValue<User>(UserRol.None)
                .HasValue<Client>(UserRol.Client)
                .HasValue<Moderator>(UserRol.Moderator)
                .HasValue<SysAdmin>(UserRol.SysAdmin);

            // -------------------------------
            // RELACIÓN: Comment
            // -------------------------------
            modelBuilder.Entity<Comment>()
                .HasOne<Vehicle>()
                .WithMany()
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Comment>()
                .HasOne<Client>()
                .WithMany()
                .HasForeignKey(r => r.UserId);

            // -------------------------------
            // RELACIÓN: Vehicle
            // -------------------------------
            modelBuilder.Entity<Vehicle>()
                .HasOne<Client>()
                .WithMany()
                .HasForeignKey(v => v.OwnerId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Images)
                .WithOne()
                .HasForeignKey(i => i.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Comments)
                .WithOne()
                .HasForeignKey(r => r.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            // ✅ RELACIÓN N–N: Vehicles ↔ Features
            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Features)
                .WithMany(f => f.Vehicles)
                .UsingEntity<Dictionary<string, object>>(
                    "VehicleFeatures",
                    j => j
                        .HasOne<Feature>()
                        .WithMany()
                        .HasForeignKey("FeatureId")
                        .HasConstraintName("FK_VehicleFeatures_Features_FeatureId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j => j
                        .HasOne<Vehicle>()
                        .WithMany()
                        .HasForeignKey("VehicleId")
                        .HasConstraintName("FK_VehicleFeatures_Vehicles_VehicleId")
                        .OnDelete(DeleteBehavior.Cascade),
                    j =>
                    {
                        j.HasKey("VehicleId", "FeatureId");
                        j.ToTable("VehicleFeatures");
                    }
                );

            modelBuilder.Entity<Vehicle>()
                .HasMany(v => v.Specifications)
                .WithOne()
                .HasForeignKey(s => s.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            // -------------------------------
            // RELACIÓN: VehicleLikes
            // -------------------------------
            modelBuilder.Entity<VehicleLike>()
                .HasOne(vl => vl.Vehicle)
                .WithMany(v => v.VehicleLikes)
                .HasForeignKey(vl => vl.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);

            // -------------------------------
            // RELACIÓN: VehicleViews
            // -------------------------------
            modelBuilder.Entity<VehicleView>()
                .HasOne(vv => vv.Vehicle)
                .WithMany(v => v.VehicleViews)
                .HasForeignKey(vv => vv.VehicleId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
