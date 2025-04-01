using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IImageRepository : IRepositoryBase<Image>
    {
        void Upload(Image image);

        List<Image> GetImagesByVehicleId(int vehicleId);
    }
}

