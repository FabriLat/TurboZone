using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Review : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Description { get; set; }

        public decimal Rating { get; set; }

        public int VehicleId { get; set; }

        public int ClientId { get; set; }

        public Review() { }

        public Review(int id, string description, decimal rating, int vehicleId)
        {
            Id = id;
            Description = description;
            Rating = rating;
            VehicleId = vehicleId;
        }
    }
}
