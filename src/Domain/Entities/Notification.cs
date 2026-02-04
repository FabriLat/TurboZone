using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Notification : IEntity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public int UserId { get; set; }

    public User User { get; set; } = null!;

    public string Brand { get; set; }

    public string Model { get; set; }

    public NotificationType Type { get; set; }

    public DateTime CreatedAt { get; set; }

    public bool IsRead { get; set; }
}
