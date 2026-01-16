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
                .Include(v => v.Images)
                .Include(v => v.Features)
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
             .Include(f => f.Features)
             .Include(v => v.VehicleLikes)
             .Include(v => v.VehicleViews)
            .FirstOrDefault(v => v.Id == id);
        }


        public List<Vehicle> GetActiveVehicles()
        {
            var appDbContext = (ApplicationContext)_dbContext;

            var vehicles = appDbContext.Vehicles
                .Include(v => v.Images.Take(1))
                .Include(v => v.VehicleLikes)
                .Include(v => v.VehicleViews)
                .Where(v => v.State == VehicleState.Active)
                .ToList();


            return vehicles;
        }


        public List<Vehicle> GetAllVehicles()
        {
            var appDbContext = (ApplicationContext)_dbContext;
            return appDbContext.Vehicles.Include(v => v.Images).ToList();
            
        }


        public void AddFeaturesToVehicle(int vehicleId, List<int> featureIds)
        {
            var appDbContext = (ApplicationContext)_dbContext;
            var vehicle = appDbContext.Vehicles
                .Include(v => v.Features)
                .FirstOrDefault(v => v.Id == vehicleId);

            if (vehicle == null) return;

            var features = appDbContext.Features
                .Where(f => featureIds.Contains(f.Id))
                .ToList();

            foreach (var feature in features)
            {
                if (!vehicle.Features.Contains(feature))
                {
                    vehicle.Features.Add(feature);
                }
            }

            appDbContext.SaveChanges();
        }


    }
}
