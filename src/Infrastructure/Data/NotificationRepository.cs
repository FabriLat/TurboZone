using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class NotificationRepository : RepositoryBase<Notification>, INotificationRepository
    {
        public NotificationRepository(ApplicationContext context) : base(context)
        {
        }

        public void SendNotificationAsync(Notification notification)
        {
            Add(notification);
        }

        public List<Notification> GetNotifications(int userId)
        {
            var appDbContext = (ApplicationContext)_dbContext;
            return appDbContext.Notifications
                .Where(n => n.UserId == userId)
                .ToList();
        }


    }
}
