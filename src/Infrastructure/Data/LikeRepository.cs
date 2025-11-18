using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class LikeRepository :  RepositoryBase<VehicleLike>, ILikeRepository
    {
        public LikeRepository(ApplicationContext context) : base(context)
        {
        }

        public VehicleLike? GetLikeByVehicleAndUser(int vehicleId, int userId)
        {
            var appDbContext = (ApplicationContext)_dbContext;
            var like = appDbContext.VehicleLikes.FirstOrDefault(l => l.VehicleId == vehicleId && l.UserId == userId);
            return like;
        }


    }
   
}
