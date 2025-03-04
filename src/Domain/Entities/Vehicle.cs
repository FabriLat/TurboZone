using Domain.Enums;
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
    public class Vehicle : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Brand { get; set; }

        [Required]
        public string Model { get; set; }

        [Required]
        public string Year { get; set; }

        [Required]
        public string Color { get; set; }

        [Required]
        public string Transmission { get; set; }

        [Required]
        public decimal MaxSpeed { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public int SellerId { get; set; } //FK para saber quien es el cliente que lo vende

        public VehicleState State { get; set; } = VehicleState.Active;

        public List<Image> Images { get; set; }

        public Vehicle()
        {
            Images = new List<Image>();
        }

        public Vehicle(int id, string brand, string model, string year, string color, string transmission, decimal price, int sellerId)
        {
            Id = id;
            Brand = brand;
            Model = model;
            Year = year;
            Color = color;
            Transmission = transmission;
            Price = price;
            SellerId = sellerId;
            Images = new List<Image>();
        }
    }
}
