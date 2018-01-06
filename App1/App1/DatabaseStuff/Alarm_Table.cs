using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class Alarm_Table
    {
        string id;
        int alarm_id;
        int dangerID;
        float loc_X;
        float loc_Y;
        string room;
        bool falseAlarm;
        bool handled;
        string carerFeedback;
        string timeCreated;
        string timeUpdated;


        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "Alarm_ID")]
        public int Alarm_id
        {
            get { return alarm_id; }
            set { alarm_id = value; }
        }

        [JsonProperty(PropertyName = "DangerID")]
        public int DangerID
        {
            get { return dangerID; }
            set { dangerID = value; }
        }

        [JsonProperty(PropertyName = "Loc_X")]
        public float Loc_X { get => loc_X; set => loc_X = value; }

        [JsonProperty(PropertyName = "Loc_Y")]
        public float Loc_Y { get => loc_Y; set => loc_Y = value; }

        [JsonProperty(PropertyName = "Room")]
        public string Room { get => room; set => room = value; }

        [JsonProperty(PropertyName = "False_Alarm")]
        public bool FalseAlarm { get => falseAlarm; set => falseAlarm = value; }

        [JsonProperty(PropertyName = "Handled")]
        public bool Handled { get => handled; set => handled = value; }

        [JsonProperty(PropertyName = "Carer_Feedback")]
        public string CarerFeedback { get => carerFeedback; set => carerFeedback = value; }

        [JsonProperty(PropertyName = "Time_Created")]
        public string TimeCreated { get => timeCreated; set => timeCreated = value; }

        [JsonProperty(PropertyName = "Time_Updated")]
        public string TimeUpdated { get => timeUpdated; set => timeUpdated = value; }

    }
}
