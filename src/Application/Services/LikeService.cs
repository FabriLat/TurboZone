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
    public class LikeService : ILikeService
    {
        private readonly ILikeRepository _likeRepository;
        public LikeService(ILikeRepository likeRepository)
        {
            _likeRepository = likeRepository;
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

    }
}
