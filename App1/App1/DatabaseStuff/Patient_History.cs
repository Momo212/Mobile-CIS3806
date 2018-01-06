using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class Patient_History
    {
        string id;
        string history_id;
        string type;
        string text;
        string year;
        string patientID_FK;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "History_ID")]
        public string History_id
        {
            get { return history_id; }
            set { history_id = value; }
        }

        [JsonProperty(PropertyName = "Type")]
        public string Type
        {
            get { return type; }
            set { type = value; }
        }

        [JsonProperty(PropertyName = "Text")]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        [JsonProperty(PropertyName = "Year")]
        public string Year
        {
            get { return year; }
            set { year = value; }
        }

        [JsonProperty(PropertyName = "PatientID_FK")]
        public string PatientID_FK
        {
            get { return patientID_FK; }
            set { patientID_FK = value; }
        }
    }
}
