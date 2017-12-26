using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class Fear_Table
    {
        string id;
        string fear_name;
        string fear_id;
        string patientID_FK;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "Fear_Name")]
        public string Fear_name
        {
            get { return fear_name; }
            set { fear_name = value; }
        }

        [JsonProperty(PropertyName = "Fear_ID")]
        public string Fear_id
        {
            get { return fear_id; }
            set { fear_id = value; }
        }

        [JsonProperty(PropertyName = "PatientID_FK")]
        public string PatientID_FK
        {
            get { return patientID_FK; }
            set { patientID_FK = value; }
        }
    }
}
