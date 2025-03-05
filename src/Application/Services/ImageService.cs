using Application.Interfaces;
using Application.Models.Requests;
using Domain.Entities;
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
        public ImageService(IImageRepository imageRepository)
        {
            _imageRepository = imageRepository;
        }


        public bool UploadImage(UploadImageDTO imageDTO)
        {
            List<Image> images = _imageRepository.GetImagesByVehicleId(imageDTO.VehicleId);

            if(images.Count() < 6 && !string.IsNullOrWhiteSpace(imageDTO.ImageName) && !string.IsNullOrWhiteSpace(imageDTO.ImageUrl))
            {
                Image newImage = new Image();
                newImage.VehicleId = imageDTO.VehicleId;
                newImage.ImageName = imageDTO.ImageName;
                newImage.ImageUrl = imageDTO.ImageUrl;
                _imageRepository.Upload(newImage);
                return true;
            }
            return false;


        }

        public bool UpdateImage(int id, UpdateImageDTO imageDTO)
        {
            var image = _imageRepository.GetById(id);
            if (image != null && !string.IsNullOrWhiteSpace(imageDTO.NewImageName) && !string.IsNullOrWhiteSpace(imageDTO.NewImageUrl))
            {
                image.ImageUrl = imageDTO.NewImageUrl;
                image.ImageName = imageDTO.NewImageName;
                _imageRepository.Update(image);
                return true;
            }
            return false;

        }
    }   
}
