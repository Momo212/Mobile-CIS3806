using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class Patient_Table
    {
        string id;
        string patient_ID;
        string name;
        string surname;
        DateTime dob;
        string gender;
        int ward_no;
        string ward_col;
        int bed_no;
        int room_no;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "Patient_ID")]
        public string Patient_ID
        {
            get { return patient_ID; }
            set { patient_ID = value; }
        }

        [JsonProperty(PropertyName = "Name")]
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        [JsonProperty(PropertyName = "Surname")]
        public string Surname
        {
            get { return surname; }
            set { surname = value; }
        }

        [JsonProperty(PropertyName = "DOB")]
        public DateTime Dob
        {
            get { return dob; }
            set { dob = value; }
        }

        [JsonProperty(PropertyName = "Gender")]
        public string Gender
        {
            get { return gender; }
            set { gender = value; }
        }

        [JsonProperty(PropertyName = "Ward_No")]
        public int Ward_No
        {
            get { return ward_no; }
            set { ward_no = value; }
        }

        [JsonProperty(PropertyName = "Ward_Col")]
        public string Ward_Col
        {
            get { return ward_col; }
            set { ward_col = value; }
        }

        [JsonProperty(PropertyName = "Bed_No")]
        public int Bed_No
        {
            get { return bed_no; }
            set { bed_no = value; }
        }

        [JsonProperty(PropertyName = "Room_No")]
        public int Room_No
        {
            get { return room_no; }
            set { room_no = value; }
        }
    }
}
