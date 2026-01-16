using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Domain.Entities;

public partial class Specification
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    [Required]
    public string? Engine { get; set; }

    public string? Power { get; set; }

    public string? Torque { get; set; }

    public string? Acceleration { get; set; }

    public int? Kilometers { get; set; }

    public string? Fuel { get; set; }

    public string? FuelConsumption { get; set; }

    public int? Doors { get; set; }

    public int? Seats { get; set; }

    public string? Weight { get; set; }

    [Required]
    public int VehicleId { get; set; }

}
