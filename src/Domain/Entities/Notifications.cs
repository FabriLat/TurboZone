using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Notifications
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsRead { get; set; }
        public Notifications() { }
        public Notifications(int id, int userId, string message, DateTime createdAt, bool isRead)
        {
            Id = id;
            UserId = userId;
            Message = message;
            CreatedAt = createdAt;
            IsRead = isRead;
        }
    }
}
