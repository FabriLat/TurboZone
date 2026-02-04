using Application.Interfaces;
using Application.Models.Responses;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class NotificationService : INotificationService
    {

        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }
        public bool SendNotification(string type,int userId, string brand, string model)
        {
            if(type == "AcceptedVehicle")
            {
                Notification notification = new Notification
                {
                    UserId = userId,
                    Type = NotificationType.AcceptedVehicle,
                    Brand = brand,
                    Model = model,
                    CreatedAt = DateTime.Now,
                    IsRead = false
                };
                _notificationRepository.Add(notification);
                return true;
            }
            else if (type == "RejectedVehicle")
            {
                Notification notification = new Notification
                {
                    UserId = userId,
                    Type = NotificationType.RejectedVehicle,
                    Brand = brand,
                    Model = model,
                    CreatedAt = DateTime.Now,
                    IsRead = false
                };
                _notificationRepository.Add(notification);
                return true;

            }
            return false;
        }

        public List<NotificationDTO> GetNotification(int userId)
        {
            var notifications = _notificationRepository.GetNotifications(userId);

            var notReadedNotifications = notifications.Where(n => n.IsRead == false).ToList();

            return notReadedNotifications.Select(n => new NotificationDTO
            {
                Id = n.Id,
                Brand = n.Brand,
                Model = n.Model,
                State = n.Type == NotificationType.AcceptedVehicle ? "Accepted" : "Rejected"
            }).ToList();


        }

        public bool MarkAsRead(int notificationId)
        {
            var notification = _notificationRepository.GetById(notificationId);
            if (notification == null)
            {
                return false;
            }
            notification.IsRead = true;
            _notificationRepository.Update(notification);
            return true;
        }


    }
}
