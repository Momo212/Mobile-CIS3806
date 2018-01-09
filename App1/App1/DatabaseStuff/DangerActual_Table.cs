using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class DangerActual_Table
    {
        string id;
        string text;
        string patientID_FK;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "Text")]
        public string Text
        {
            get { return text; }
            set { text = value; }
        }

        [JsonProperty(PropertyName = "PatientID_FK")]
        public string PatientID_FK
        {
            get { return patientID_FK; }
            set { patientID_FK = value; }
        }
    }
}
