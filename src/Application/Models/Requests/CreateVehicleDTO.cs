using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class CreateVehicleDTO
    {
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string Color { get; set; }

        public string Transmission { get; set; }

        public string ImageUrl { get; set; }

        public string Description { get; set; }

        public decimal MaxSpeed { get; set; }

        public decimal Price { get; set; }
    }
}
