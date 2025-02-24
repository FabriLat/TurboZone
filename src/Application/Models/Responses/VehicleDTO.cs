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

        public VehicleState State { get; set; }



        public static VehicleDTO Create(Vehicle vehicle)
        {
            VehicleDTO dto = new VehicleDTO();
            dto.Id = vehicle.Id;
            dto.SellerId = vehicle.SellerId;
            dto.Brand = vehicle.Brand;
            dto.Model = vehicle.Model;
            dto.Year = vehicle.Year;
            dto.Color = vehicle.Color;
            dto.Transmission = vehicle.Transmission;
            dto.MaxSpeed = vehicle.MaxSpeed;
            dto.Price = vehicle.Price;
            dto.State = vehicle.State;
            return dto;
        }
    }




}
