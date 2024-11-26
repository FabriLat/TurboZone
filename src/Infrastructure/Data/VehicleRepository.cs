using Application.Models.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
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

        public List<Vehicle> GetPendingVehicles()
        {
            var appDbContext = (ApplicationContext)_dbContext;
            
            return appDbContext.Vehicles.Where(v => v.State == VehicleState.Pending).ToList();
        }

    }
}
