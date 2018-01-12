using App1.DatabaseStuff;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace App1.NotificationList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotifChildTabEnvironment : ContentPage
    {
        private static int catID = 2;
        ItemManager manager;

        public NotifChildTabEnvironment()
        {
            InitializeComponent();
            manager = ItemManager.DefaultManager;
            DisplayList();
        }

        public async Task<List<Notification>> GetAlarms()
        {
            ObservableCollection<Alarm_Table> alarmTable = await manager.GetAlarmTableItemsAsync();
            ObservableCollection<Danger_Table> dangerTable = await manager.GetDangerTableItemsAsync();
            ObservableCollection<LUT_Alarm_Danger_Category> lutAlarmDangerCategory = await manager.GetLUT_Alarm_Danger_CategoryTableItemsAsync();

            //for predictions
            var query = from alarm in alarmTable
                        join danger in dangerTable on alarm.DangerID equals danger.Danger_id
                        join lut_alarm_danger in lutAlarmDangerCategory on danger.AlarmDanger_CategoryID equals lut_alarm_danger.Lut_alarm_Danger_Category_ID
                        where ((alarm.Handled == false) && (lut_alarm_danger.AlarmCategoryID == catID))
                        select new Notification() { AlarmId = alarm.Alarm_id, Name = lut_alarm_danger.Name, Room = alarm.Room, TimeStamp = alarm.TimeCreated, Alarmtypeid = danger.Alarmtypeid, DangerCategoryID = lut_alarm_danger.Lut_alarm_Danger_Category_ID };

            List<Notification> ln = query.ToList<Notification>();

            return ln;
        }

        public async Task<List<string>> GetPatients(int alarmID)
        {
            List<string> patientNames = new List<string>();

            ObservableCollection<Patient_Alarm_Table> patientAlarmTable = await manager.GetPatientAlarmTableItemsAsync();

            //for predictions
            var query = from patientAlarm in patientAlarmTable
                        where (patientAlarm.Alarm_id == alarmID)
                        select new Patient_Alarm_Table() { Patient_id = patientAlarm.Patient_id };

            List<Patient_Alarm_Table> lpat = query.ToList<Patient_Alarm_Table>();

            foreach (Patient_Alarm_Table pat in lpat)
            {
                ObservableCollection<Patient_Table> p = await manager.GetPatient(pat.Patient_id);
                patientNames.Add(p[0].Name + " " + p[0].Surname);
            }

            return patientNames;
        }

        public async void DisplayList()
        {
            List<Notification> ln = await GetAlarms();
            List<string> patientsInvolved = null;

            foreach (Notification notif in ln)
            { // foreach notification from list,
                patientsInvolved = await GetPatients(notif.AlarmId);
                ListViewEnvironment.Children.Add(Utilities.BuildListElement(notif, patientsInvolved)); //build list element gridview and append as child element of <StackLayout x:Name="ListView">
            }

        }
    }
}