using App1.DatabaseStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPatient : ContentPage
    {
        ItemManager manager;

        string selectedGender;
        string selectedColor;

        public AddPatient()
        {
            InitializeComponent();

            manager = ItemManager.DefaultManager;

            imgProfile.Source = ImageSource.FromFile("Assets/profile.png");
            imgEditProfile.Source = ImageSource.FromFile("Assets/editImage.png");

            var dashboard = new ToolbarItem
            {
                Icon = "Assets/home.png",
                Text = "Dashboard",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(this.ShowDashboard),
            };
            var addPatient = new ToolbarItem
            {
                Icon = "Assets/addPatientToolbarIcon.png",
                Text = "Add Patient",
                Order = ToolbarItemOrder.Secondary,
                Command = new Command(this.ShowAddPatientPage),
            };

            this.ToolbarItems.Add(dashboard);
            this.ToolbarItems.Add(addPatient);
            GetDateTime();
        }

        private void ShowDashboard()
        {
            this.Navigation.PushAsync(new MainPage());
        }
        private void ShowAddPatientPage()
        {
            this.Navigation.PushAsync(new AddPatient());
        }
        DateTime GetDateTime()
        {
            DateTime date = DatePickerT.Date;
            return date;
        }
        void GenderPicker_IndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if(selectedIndex != -1)
            {
                List<string> genderList = new List<string>(new string[] { "Male", "Female" });
                selectedGender = genderList[selectedIndex];
            }
        }
        void WardColPicker_IndexChanged(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            int selectedIndex = picker.SelectedIndex;
            if(selectedIndex != -1)
            {
                List<string> colorList = new List<string>(new string[] { "Red", "Blue", "Green" });
                selectedColor = colorList[selectedIndex];
            }
        }
        public async void onAddPatient_OnClick(object sender, EventArgs e)
        {
            var todo = new Patient_Table { Patient_ID = IdNumberEntry.Text, Name = NameEntry.Text, Surname = SurnameEntry.Text, Dob = GetDateTime(), Gender = selectedGender, Ward_No = Int32.Parse(WardNo.Text), Room_No = Int32.Parse(RoomNo.Text), Bed_No = Int32.Parse(BedNo.Text), Ward_Col = selectedColor };
            await AddItem(todo);

            IdNumberEntry.Text = String.Empty;
            NameEntry.Text = String.Empty;
            SurnameEntry.Text = String.Empty;
            DatePickerT.Date = DatePickerT.MaximumDate;
            genderPicker.SelectedIndex = -1;
            wardColourPicker.SelectedIndex = -1;
            WardNo.Text = String.Empty;
            RoomNo.Text = String.Empty;
            BedNo.Text = String.Empty;
        }
        async Task AddItem(Patient_Table item)
        {
            await manager.SaveTaskAsync(item);
        }

        public async void onCancel_OnClick(Object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new MainPage());
        }
    }
}