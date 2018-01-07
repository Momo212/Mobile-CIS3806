using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.NotificationList
{
    public class Notification
    {
        public int AlarmId { get; set; }
        public string Name { get; set; }
        public string Room { get; set; }
        public string TimeStamp { get; set; }
        public int Alarmtypeid { get; set; }
        public int DangerCategoryID { get; set; }
    }
}
