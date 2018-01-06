using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class Danger_Table
    {
        string id;
        int danger_id;
        int alarmDanger_CategoryID;
        int alarmtypeid;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "Danger_ID")]
        public int Danger_id
        {
            get { return danger_id; }
            set { danger_id = value; }
        }

        [JsonProperty(PropertyName = "AlarmDangerCategoryID")]
        public int AlarmDanger_CategoryID
        {
            get { return alarmDanger_CategoryID; }
            set { alarmDanger_CategoryID = value; }
        }

        [JsonProperty(PropertyName = "AlarmTypeID")]
        public int Alarmtypeid
        {
            get { return alarmtypeid; }
            set { alarmtypeid = value; }
        }
    }
}
