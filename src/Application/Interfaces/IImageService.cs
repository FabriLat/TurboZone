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
        bool UploadImage(UploadImageDTO imageDTO);

        bool UpdateImage(int vehicleId, UpdateImageDTO imageDTO);
    }
}
