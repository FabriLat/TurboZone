using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests.Vehicles
{
    public class UpdateVehicleDTO
    {

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Year { get; set; }

        public string Color { get; set; }

        public string Transmission { get; set; }

        public decimal MaxSpeed { get; set; }

        public decimal Price { get; set; }

        public List<int> FeaturesIds { get; set; }  
    }
}
