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

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasDiscriminator<UserRol>("Rol")
                .HasValue<User>(UserRol.None)
                .HasValue<Client>(UserRol.Client)
                .HasValue<Moderator>(UserRol.Moderator)
                .HasValue<SysAdmin>(UserRol.SysAdmin);



            {//Relaciones de Comment
                modelBuilder.Entity<Comment>()
                    .HasOne<Vehicle>()
                    .WithMany()
                    .HasForeignKey(r => r.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Comment>()
                    .HasOne<Client>()
                    .WithMany()
                    .HasForeignKey(r => r.UserId);
            }


            {//RELACIONES DE VEHICLE
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


            }
        }
    }
}