using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Image : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string ImageName { get; set; }

        [Required]
        public string ImageUrl { get; set; }


        public int VehicleId { get; set; }
        public Image() { }

        public Image(string imageUrl, int vehicleId, string imageName)
        {
            ImageUrl = imageUrl;
            ImageName = imageName;
            VehicleId = vehicleId;
        }

    }
}
