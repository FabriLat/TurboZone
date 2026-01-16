using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Responses
{
    public class VehicleDTO
    {
        public int Id { get; set; }

        public int SellerId { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string Color { get; set; }

        public string Transmission { get; set; }

        public decimal MaxSpeed { get; set; }

        public decimal Price { get; set; }

        public string Location { get; set; }

        public string Description { get; set; }

        public VehicleState State { get; set; }

        public List<Image> Images { get; set; }

        public List<string> Features { get; set; }

        public int TotalLikes { get; set; }

        public int TotalViews { get; set; }





        public static VehicleDTO Create(Vehicle vehicle, List<string> featuresNames = null)
        {
            VehicleDTO dto = new VehicleDTO
            {
                Id = vehicle.Id,
                SellerId = vehicle.OwnerId,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                Year = vehicle.Year,
                Color = vehicle.Color,
                Transmission = vehicle.Transmission,
                MaxSpeed = vehicle.MaxSpeed,
                Price = vehicle.Price,
                State = vehicle.State,
                Images = new List<Image>(),
                Description = vehicle.Description,
                Location = vehicle.Location,
                Features = featuresNames,
                TotalLikes = vehicle.VehicleLikes?.Count ?? 0,
                TotalViews = vehicle.VehicleViews?.Count ?? 0
            };

            foreach (var image in vehicle.Images)
            {
                dto.Images.Add(image);
            }

            return dto;
        }

    }
}


    
