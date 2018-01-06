using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class Relative_Table
    {
        string id;
        string rel_id;
        string rel_type;
        string name;
        string surname;
        string phone_no;
        string patientID_FK;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "Relative_ID")]
        public string Rel_id
        {
            get { return rel_id; }
            set { rel_id = value; }
        }

        [JsonProperty(PropertyName = "Relative_Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        [JsonProperty(PropertyName = "Relative_Type")]
        public string Rel_type
        {
            get { return rel_type; }
            set { rel_type = value; }
        }

        [JsonProperty(PropertyName = "Relative_Surname")]
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        [JsonProperty(PropertyName = "Relative_Phone")]
        public string Phone_no
        {
            get { return phone_no; }
            set { phone_no = value; }
        }

        [JsonProperty(PropertyName = "PatientID_FK")]
        public string PatientID_FK
        {
            get { return patientID_FK; }
            set { patientID_FK = value; }
        }
    }
}
