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
    }
}
