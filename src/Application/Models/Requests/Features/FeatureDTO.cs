using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests.Features
{
    public class FeatureDTO
    {
        public int VehicleId { get; set; }
        public List<int> FeaturesIds { get; set; } = new List<int>();
    }
}
