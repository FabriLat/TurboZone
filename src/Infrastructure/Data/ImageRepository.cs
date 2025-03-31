using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ImageRepository : RepositoryBase<Image>, IImageRepository
    {
        public ImageRepository(ApplicationContext context) : base(context)
        {
        }


        public void Upload(Image image) 
        {
            var appDbContext = (ApplicationContext)_dbContext;
            appDbContext.Images.Add(image);
            appDbContext.SaveChanges();

        }

        public List<Image> GetImagesByVehicleId(int vehicleId)
        {
            var appDbContext = (ApplicationContext)_dbContext;
            return appDbContext.Images.Where(i => i.VehicleId == vehicleId).ToList();
        }
    }
}
