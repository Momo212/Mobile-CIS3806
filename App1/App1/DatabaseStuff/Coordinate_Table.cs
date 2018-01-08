using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.DatabaseStuff
{
    public class Coordinate_Table
    {
        string id;
        float coord_x;
        float coord_y;
        string patientId;
        DateTime timestamp;

        [JsonProperty(PropertyName = "id")]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        [JsonProperty(PropertyName = "coord_x")]
        public float Coord_x
        {
            get { return coord_x; }
            set { coord_x = value; }
        }

        [JsonProperty(PropertyName = "coord_y")]
        public float Coord_y
        {
            get { return coord_y; }
            set { coord_y = value; }
        }

        [JsonProperty(PropertyName = "patientId")]
        public string PatientID
        {
            get { return patientId; }
            set { patientId = value; }
        }

        [JsonProperty(PropertyName = "timestamp")]
        public DateTime Timestamp
        {
            get { return timestamp; }
            set { timestamp = System.DateTime.Now; }
        }
    }
}
