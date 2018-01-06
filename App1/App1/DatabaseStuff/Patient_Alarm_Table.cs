using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class Patient_Alarm_Table
    {
        string id;
        int patient_alarm_id;
        string patient_id;
        int alarm_id;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "Patient_Alarm_ID")]
        public int Patient_alarm_id
        {
            get { return patient_alarm_id; }
            set { patient_alarm_id = value; }
        }

        [JsonProperty(PropertyName = "Patient_ID")]
        public string Patient_id
        {
            get { return patient_id; }
            set { patient_id = value; }
        }

        [JsonProperty(PropertyName = "Alarm_ID")]
        public int Alarm_id
        {
            get { return alarm_id; }
            set { alarm_id = value; }
        }
    }
}
