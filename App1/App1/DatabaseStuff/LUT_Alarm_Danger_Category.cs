using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class LUT_Alarm_Danger_Category
    {
        int id;
        string name;
        int alarmCategoryID;
        int lut_alarm_Danger_Category_ID;

        [JsonProperty(PropertyName = "ID")]
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonProperty(PropertyName = "LUT_Alarm_Danger_Category_ID")]
        public int Lut_alarm_Danger_Category_ID
        {
            get { return lut_alarm_Danger_Category_ID; }
            set { lut_alarm_Danger_Category_ID = value; }
        }

        [JsonProperty(PropertyName = "AlarmCategoryID")]
        public int AlarmCategoryID { get => alarmCategoryID; set => alarmCategoryID = value; }
    }
}
