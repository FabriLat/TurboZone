using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class VehicleFeatureRepository : RepositoryBase<VehicleFeature>, IVehicleFeatureRepository
    {
        public VehicleFeatureRepository(ApplicationContext context) : base(context)
        {
        }

        public bool addVehicleFeature(VehicleFeature vehicleFeature)
        {
            var appDbContext = (ApplicationContext)_dbContext;
            return appDbContext.SaveChanges() > 0;
        }
    }
}
