using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.NotificationList
{
    public class Notification
    {
        public string notifName { get; set; }
        public string location { get; set; }
        public string timestamp { get; set; }
        public int notifType { get; set; }
        public int dangerCategoryId { get; set; }
    }
}
