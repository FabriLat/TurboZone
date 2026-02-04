using Application.Models.Responses;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces
{
    public interface INotificationService
    {
        bool SendNotification(string type, int userId, string brand, string model);

        List<NotificationDTO> GetNotification(int userId);

        bool MarkAsRead(int notificationId);
    }
}
