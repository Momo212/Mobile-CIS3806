using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class Hobby_Table
    {
        string id;
        string hobby_id;
        string hobby_name;
        string patientID_FK;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "Hobby_ID")]
        public string Hobby_id
        {
            get { return hobby_id; }
            set { hobby_id = value; }
        }

        [JsonProperty(PropertyName = "Hobby_Name")]
        public string Hobby_name
        {
            get { return hobby_name; }
            set { hobby_name = value; }
        }

        [JsonProperty(PropertyName = "PatientID_FK")]
        public string PatientID_FK
        {
            get { return patientID_FK; }
            set { patientID_FK = value; }
        }
    }
}
