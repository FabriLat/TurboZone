using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ViewRepository : RepositoryBase<VehicleView>, IViewRepository
    {
        public ViewRepository(ApplicationContext context) : base(context)
        {
        }

        public int GetTotalViewsByVehicle(int vehicleId)
        {
            var appDbContext = (ApplicationContext)_dbContext;

            return appDbContext.VehicleViews.Count(v => v.VehicleId == vehicleId);
        }



    }
}
