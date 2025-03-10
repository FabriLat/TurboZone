using Application.Models.Requests;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface IImageService
    {
        public bool UploadImage(UploadImageDTO imageDTO, int userId);

        bool UpdateImage(int id, UpdateImageDTO imageDTO, int userId);

        bool DeleteImage(int imageId, int userId);
    }
}
