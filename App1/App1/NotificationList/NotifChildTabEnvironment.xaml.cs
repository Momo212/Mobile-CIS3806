using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1.NotificationList
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NotifChildTabEnvironment : ContentPage
    {
        public NotifChildTabEnvironment()
        {
            InitializeComponent();
            DisplayList(); //to discuss if we should make addiional calls on tab change
        }

        public List<Notification> fetchPatientNotifs()
        {
            //DUMMY DATA FOR NOW - FROM DATABASE LATER
            List<Notification> ln = new List<Notification>();

            Notification n3 = new Notification
            {
                notifName = "Open Door",
                location = "Corridor",
                timestamp = "Just now",
                notifType = 2,
                dangerCategoryId = 4
            };
            ln.Add(n3);

            return ln;
        }

        public void DisplayList()
        {
            List<Notification> ln = fetchPatientNotifs();

            foreach (Notification notif in ln) // foreach notification from list, 
                ListViewEnvironment.Children.Add(Utilities.BuildListElement(notif)); //build list element gridview and append as child element of <StackLayout x:Name="ListView">
        }
    }
}