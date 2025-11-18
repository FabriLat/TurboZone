using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities;

public partial class VehicleLike
{

    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int VehicleId { get; set; }

    public int UserId { get; set; }

    public DateTime? LikedAt { get; set; }

    public virtual Vehicle Vehicle { get; set; } = null!;
}
