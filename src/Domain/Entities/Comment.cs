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
    public class Comment : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Text { get; set; }

        public int VehicleId { get; set; }

        public int UserId { get; set; }

        public Comment() { }

        public Comment(int id, string text, int vehicleId, int userId)
        {
            Id = id;
            Text = text;
            VehicleId = vehicleId;
            UserId = userId;
        }
    }
}
