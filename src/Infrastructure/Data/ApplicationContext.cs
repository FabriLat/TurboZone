using Domain.Entities;
using Domain.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Purchase> Purchases { get; set; }



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



            {//Relaciones de Review
                modelBuilder.Entity<Review>()
                    .HasOne<Vehicle>()
                    .WithMany()
                    .HasForeignKey(r => r.VehicleId)
                    .OnDelete(DeleteBehavior.Cascade);

                modelBuilder.Entity<Review>()
                    .HasOne<Client>()
                    .WithMany()
                    .HasForeignKey(r => r.ClientId);
            }



            {//RELACIONES DE PURCHASE
                modelBuilder.Entity<Purchase>()
                    .HasOne<Client>()
                    .WithMany()
                    .HasForeignKey(p => p.ClientId)
                    .OnDelete(DeleteBehavior.Restrict);

                modelBuilder.Entity<Purchase>()
                    .HasOne<Client>()
                    .WithMany()
                    .HasForeignKey(p => p.SellerId)
                    .OnDelete(DeleteBehavior.Restrict);

               modelBuilder.Entity<Purchase>()
                    .HasOne<Vehicle>()
                    .WithMany()
                    .HasForeignKey(p => p.VehicleId)
                    .OnDelete(DeleteBehavior.Restrict);
            }

            {//RELACIONES DE VEHICLE
                modelBuilder.Entity<Vehicle>()
                    .HasOne<Client>()
                    .WithMany()
                    .HasForeignKey(v => v.SellerId)
                    .OnDelete(DeleteBehavior.Cascade);
            }

        }

    }
}
