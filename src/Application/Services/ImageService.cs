using Application.Interfaces;
using Application.Models.Requests;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ImageService : IImageService
    {
        private readonly IImageRepository _imageRepository;
        private readonly IUserService _userService;
        private readonly IVehicleRepository _vehicleRepository;
        public ImageService(IImageRepository imageRepository, IUserService userService, IVehicleRepository vehicleRepository)
        {
            _imageRepository = imageRepository;
            _userService = userService;
            _vehicleRepository = vehicleRepository;
        }


        public bool UploadImage(UploadImageDTO imageDTO, int userId)
        {
            List<Image> images = _imageRepository.GetImagesByVehicleId(imageDTO.VehicleId);


            if(images.Count < 6 && !string.IsNullOrWhiteSpace(imageDTO.ImageName) && !string.IsNullOrWhiteSpace(imageDTO.ImageUrl))
            {
                var vehicle = _vehicleRepository.GetById(imageDTO.VehicleId);
                if (vehicle.OwnerId == userId)
                {
                    Image newImage = new Image();
                    newImage.VehicleId = imageDTO.VehicleId;
                    newImage.ImageName = imageDTO.ImageName;
                    newImage.ImageUrl = imageDTO.ImageUrl;
                    _imageRepository.Upload(newImage);
                    return true;
                }
            }
            return false;
        }

        public bool UpdateImage(int id, UpdateImageDTO imageDTO, int userId)
        {
            Image? image = _imageRepository.GetById(id);
            UserDTO? user = _userService.GetUserById(userId);

            if (image == null || user == null)
                return false;

            var vehicle = _vehicleRepository.GetById(image.VehicleId);
            if (vehicle == null)
                return false;

            if (image != null && !string.IsNullOrWhiteSpace(imageDTO.NewImageName) && !string.IsNullOrWhiteSpace(imageDTO.NewImageUrl))
            {
                if(vehicle.OwnerId == userId || user.Rol == UserRol.Moderator || user.Rol == UserRol.SysAdmin)
                {
                    image.ImageUrl = imageDTO.NewImageUrl;
                    image.ImageName = imageDTO.NewImageName;
                    _imageRepository.Update(image);
                    return true;
                }
            }
            return false;

        }

        public bool DeleteImage(int imageId, int userId)
        {
            UserDTO? user = _userService.GetUserById(userId);
            Image? image = _imageRepository.GetById(imageId);

            if (image == null || user == null)
                return false;

            var vehicle = _vehicleRepository.GetById(image.VehicleId);
            if (vehicle == null)
                return false;

            if (vehicle.OwnerId == userId || user.Rol == UserRol.Moderator || user.Rol == UserRol.SysAdmin)
            {
                _imageRepository.Delete(image);
                return true;
            }
            return false;
        }

    }
}
