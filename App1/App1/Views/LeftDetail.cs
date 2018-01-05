using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App1.Views
{
    public class LeftDetail
    {
        public string Title { get; set; }
        //public string Text { get; set; }
        public string LeftTitle { get; set; }
        public string RightTitle { get; set; }
        public string LeftArrow { get; set; }
        public string RightArrow { get; set; }
        public ObservableCollection<values> obscollection { get; set; }
    }
    public class values
    {
        public string name { get; set; }
        public string relation { get; set; }
        public string patientid { get; set; }
        public string phoneno { get; set; }
    }
}
