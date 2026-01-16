using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IVehicleLikeService
    {
        bool LikeVehicle(int vehicleId, int userId);

        bool UnlikeVehicle(int vehicleId, int userId);

        int GetTotalLikes(int vehicleId);

        bool IsVehicleLikedByUser(int vehicleId, int userId);
    }
}
