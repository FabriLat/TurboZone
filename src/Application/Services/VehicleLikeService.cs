using Application.Interfaces;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class VehicleLikeService : IVehicleLikeService
    {
        private readonly IVehicleLikeRepository _likeRepository;
        public VehicleLikeService(IVehicleLikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
        }


        public int GetTotalLikes(int vehicleId)
        {
            return _likeRepository.GetLikesByVehicleId(vehicleId);
        }

        public bool LikeVehicle(int vehicleId, int userId)
        {

            VehicleLike? existingLike = _likeRepository.GetLikeByVehicleAndUser(vehicleId, userId);
            if (existingLike != null)
            {
                return false; 
            }

            VehicleLike like = new VehicleLike
            {
                VehicleId = vehicleId,
                UserId = userId,
                LikedAt = DateTime.UtcNow
            };
            _likeRepository.Add(like);
            return true;

        }
        public bool UnlikeVehicle(int vehicleId, int userId)
        {
           VehicleLike? like = _likeRepository.GetLikeByVehicleAndUser(vehicleId, userId);
            if (like != null)
            {
                _likeRepository.Delete(like);
                return true;
            }
            return false;
        }

        public bool IsVehicleLikedByUser(int vehicleId, int userId)
        {
            VehicleLike? like = _likeRepository.GetLikeByVehicleAndUser(vehicleId, userId);
            return like != null;
        }

    }
}
