using Application.Models.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class VehicleRepository : RepositoryBase<Vehicle>, IVehicleRepository
    {
        public VehicleRepository(ApplicationContext context) : base(context)
        {
        }

        public List<Vehicle> GetPendingCreateVehicles()
        {
            var appDbContext = (ApplicationContext)_dbContext;

            return appDbContext.Vehicles
                .Include(v => v.Images) // Incluye las imágenes
                .Where(v => v.State == VehicleState.PendingCreate)
                .ToList();
        }

        public List<Vehicle> GetPendingUpdateVehicles()
        {
            var appDbContext = (ApplicationContext)_dbContext;

            return appDbContext.Vehicles
                .Include(v => v.Images)
                .Where(v => v.State == VehicleState.PendingUpdate)
                .ToList();
        }


        public Vehicle? GetById(int id)
        {
            var appDbContext = (ApplicationContext)_dbContext;
            return appDbContext.Vehicles
             .Include(v => v.Images) 
            .FirstOrDefault(v => v.Id == id);
        }


        public List<Vehicle> GetActiveVehicles()
        {
            var appDbContext = (ApplicationContext)_dbContext;

            var vehicles = appDbContext.Vehicles
                .Include(v => v.Images.Take(1))
                .Where(v => v.State == VehicleState.Active)
                .ToList();


            return vehicles;
        }


        public List<Vehicle> GetAllVehicles()
        {
            var appDbContext = (ApplicationContext)_dbContext;
            return appDbContext.Vehicles.Include(v => v.Images).ToList();
            
        }


    }
}
