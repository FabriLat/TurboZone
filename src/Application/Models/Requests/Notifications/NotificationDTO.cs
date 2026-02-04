using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Models.Requests.Notifications
{
    public class NotificationDTO
    {
        public int UserId { get; set; }
        
        public string Brand { get; set; }

        public string Model { get; set; }

        public string Type { get; set; }
    }

}