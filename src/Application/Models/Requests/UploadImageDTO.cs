using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests
{
    public class UploadImageDTO
    {
        public int VehicleId { get; set; }

        public string ImageName { get; set; }

        public string ImageUrl { get; set; }
    }
}
