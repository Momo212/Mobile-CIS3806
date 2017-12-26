using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.NotificationList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotifChildTabPatient : ContentPage
    {
        public NotifChildTabPatient()
        {
            InitializeComponent();
            DisplayList(); //to discuss if we should make addiional calls on tab change
        }

        public List<Notification> fetchPatientNotifs()
        {
            //DUMMY DATA FOR NOW - FROM DATABASE LATER
            List<Notification> ln = new List<Notification>();

            Notification n = new Notification
            {
                notifName = "Patient fall",
                location = "Room A1",
                timestamp = "today 09:42 AM",
                notifType = 1,
                dangerCategoryId = 1
            };
            ln.Add(n);

            Notification n1 = new Notification
            {
                notifName = "Crowding detected",
                location = "Room B1",
                timestamp = "today 6:00 AM",
                notifType = 1,
                dangerCategoryId = 2
            };
            ln.Add(n1);

            Notification n2 = new Notification
            {
                notifName = "Sundowning",
                location = "Room C1",
                timestamp = "today 11:20 AM",
                notifType = 2,
                dangerCategoryId = 3
            };
            ln.Add(n2);

            return ln;
        }

        public void DisplayList()
        {
            List<Notification> ln = fetchPatientNotifs();

            foreach (Notification notif in ln) // foreach notification from list, 
                ListViewPatient.Children.Add(Utilities.BuildListElement(notif)); //build list element gridview and append as child element of <StackLayout x:Name="ListView">
        }
    }
}