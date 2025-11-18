using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ILikeRepository : IRepositoryBase<VehicleLike>
    {
        VehicleLike? GetLikeByVehicleAndUser(int vehicleId, int userId);
    }
}
