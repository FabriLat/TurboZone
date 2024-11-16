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
    public class Purchase : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int VehicleId { get; set; } //FK

        public int ClientId { get; set; } //FK

        public int SellerId { get; set; } //FK

        public decimal TotalPrice { get; set; } 
    }
}
