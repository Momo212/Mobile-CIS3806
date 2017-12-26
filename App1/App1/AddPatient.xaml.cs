using App1.DatabaseStuff;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using App1.Views;
using System.Collections.ObjectModel;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddPatient : ContentPage
    {
        public bool ButtonFlag = false;
        public int countHobbyID;
        public int countFearID;
        public string currentUserId;
        ItemManager manager;

        string selectedGender;
        string selectedColor;

        public AddPatient()
        {
            InitializeComponent();

            manager = ItemManager.DefaultManager;

            imgProfile.Source = ImageSource.FromFile("Assets/profile.png");
            imgEditProfile.Source = ImageSource.FromFile("Assets/editImage.png");
            
            if (currentUserId == null)
            {
                createNewButton.IsEnabled = false;
            }

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
            
            var pages = new List < string >{ "Relatives","Hobbies","Fears"};

            MainCarouselView.ItemsSource = pages;
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
            if (ButtonFlag == true && MainCarouselView.Position == 0)
            {
                if (NameEntry.Text == null || SurnameEntry.Text == null || IdNumberEntry.Text == null || AdditionalEntry.Text == null)
                {
                    DisplayAlert("Alert", "All the fields must be filled in.", "OK");
                }
                else
                {
                    var todo = new Relative_Table { Rel_id = IdNumberEntry.Text, Name = NameEntry.Text, Surname = SurnameEntry.Text, Phone_no = AdditionalEntry.Text, PatientID_FK = currentUserId };
                    await AddRelativeItem(todo);
                }
                ButtonFlag = false;

                //Reset Components
                NameEntry.Placeholder = "Enter Name...";
                NameEntry.Text = null;
                SurnameEntry.Text = null;
                IdNumberEntry.Text = null;
                AdditionalEntry.IsVisible = false;
                SurnameEntry.Placeholder = "Enter Surname...";
                IdNumberEntry.Placeholder = "Enter ID Number...";
                DOB.IsVisible = true;
                DatePickerT.IsVisible = true;
                Gender.IsVisible = true;
                genderPicker.IsVisible = true;
                RoomNo.IsVisible = true;
                BedNo.IsVisible = true;
                WardNo.IsVisible = true;
                WardCol.IsVisible = true;
                wardColourPicker.IsVisible = true;
            }
            else if (ButtonFlag == true && MainCarouselView.Position == 1)
            {
                if (NameEntry.Text == null)
                {
                    DisplayAlert("Alert", "All the fields must be filled in.", "OK");
                }
                else
                {
                    var todo = new Hobby_Table { Hobby_name = NameEntry.Text, PatientID_FK = currentUserId, Hobby_id = countHobbyID.ToString() };
                    await AddHobbyItem(todo);
                    countHobbyID++;
                }
                ButtonFlag = false;

                //Reset Components
                NameEntry.Placeholder = "Enter Name...";
                SurnameEntry.Placeholder = "Enter Surname...";
                NameEntry.Text = null;
                SurnameEntry.Text = null;
                IdNumberEntry.Text = null;
                AdditionalEntry.IsVisible = false;
                SurnameEntry.IsVisible = true;
                IdNumberEntry.IsVisible = true;
                IdNumberEntry.Placeholder = "Enter ID Number...";
                DOB.IsVisible = true;
                DatePickerT.IsVisible = true;
                Gender.IsVisible = true;
                genderPicker.IsVisible = true;
                RoomNo.IsVisible = true;
                BedNo.IsVisible = true;
                WardNo.IsVisible = true;
                WardCol.IsVisible = true;
                wardColourPicker.IsVisible = true;
            }
            else if (ButtonFlag == true && MainCarouselView.Position == 2)
            {
                if (NameEntry.Text == null)
                {
                    DisplayAlert("Alert", "All the fields must be filled in.", "OK");
                }
                else
                {
                    var todo = new Fear_Table { Fear_name = NameEntry.Text, PatientID_FK = currentUserId, Fear_id = countFearID.ToString() };
                    await AddFearItem(todo);
                    countFearID++;
                }
                ButtonFlag = false;

                //Reset Components
                NameEntry.Placeholder = "Enter Name...";
                SurnameEntry.Placeholder = "Enter Surname...";
                NameEntry.Text = null;
                SurnameEntry.Text = null;
                IdNumberEntry.Text = null;
                AdditionalEntry.IsVisible = false;
                SurnameEntry.IsVisible = true;
                IdNumberEntry.IsVisible = true;
                IdNumberEntry.Placeholder = "Enter ID Number...";
                DOB.IsVisible = true;
                DatePickerT.IsVisible = true;
                Gender.IsVisible = true;
                genderPicker.IsVisible = true;
                RoomNo.IsVisible = true;
                BedNo.IsVisible = true;
                WardNo.IsVisible = true;
                WardCol.IsVisible = true;
                wardColourPicker.IsVisible = true;
            }
            else
            {
                if (IdNumberEntry.Text == null || NameEntry.Text == null || SurnameEntry.Text == null || selectedGender == null || WardNo.Text == null || RoomNo.Text == null || BedNo.Text == null || selectedColor == null)
                {
                    DisplayAlert("Alert", "All the fields must be filled in.", "OK");
                }
                else
                {
                    var todo = new Patient_Table { Patient_ID = IdNumberEntry.Text, Name = NameEntry.Text, Surname = SurnameEntry.Text, Dob = GetDateTime(), Gender = selectedGender, Ward_No = Int32.Parse(WardNo.Text), Room_No = Int32.Parse(RoomNo.Text), Bed_No = Int32.Parse(BedNo.Text), Ward_Col = selectedColor };
                    await AddPatientItem(todo);

                    currentUserId = IdNumberEntry.Text;

                    IdNumberEntry.Text = String.Empty;
                    NameEntry.Text = String.Empty;
                    SurnameEntry.Text = String.Empty;
                    DatePickerT.Date = DatePickerT.MaximumDate;
                    genderPicker.SelectedIndex = -1;
                    wardColourPicker.SelectedIndex = -1;
                    WardNo.Text = String.Empty;
                    RoomNo.Text = String.Empty;
                    BedNo.Text = String.Empty;

                    createNewButton.IsEnabled = true;
                }
            }
        }
        async Task AddPatientItem(Patient_Table item)
        {
            await manager.SaveTaskAsyncPatient(item);
        }

        async Task AddRelativeItem(Relative_Table item)
        {
            await manager.SaveTaskAsyncRelative(item);
        }

        async Task AddHobbyItem(Hobby_Table item)
        {
            await manager.SaveTaskAsyncHobby(item);
        }

        async Task AddFearItem(Fear_Table item)
        {
            await manager.SaveTaskAsyncFear(item);
        }

        public void onCancel_OnClick(Object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new MainPage());
        }
        public async void createNew_OnClick(Object sender, EventArgs e)
        {
            var index = MainCarouselView.Position;
            
            if(index == 0)
            {
                AdditionalEntry.IsVisible = true;
                NameEntry.Placeholder = "Relative Name...";
                SurnameEntry.Placeholder = "Relative Surname...";
                IdNumberEntry.Placeholder = "Relative ID...";
                DOB.IsVisible = false;
                DatePickerT.IsVisible = false;
                Gender.IsVisible = false;
                genderPicker.IsVisible = false;
                RoomNo.IsVisible = false;
                BedNo.IsVisible = false;
                WardNo.IsVisible = false;
                wardColourPicker.IsVisible = false;
                WardCol.IsVisible = false;

                ButtonFlag = true;
            }
            else if(index == 1)
            {
                NameEntry.Placeholder = "Hobby Name...";
                SurnameEntry.IsVisible = false;
                IdNumberEntry.IsVisible = false;
                DOB.IsVisible = false;
                DatePickerT.IsVisible = false;
                Gender.IsVisible = false;
                genderPicker.IsVisible = false;
                RoomNo.IsVisible = false;
                BedNo.IsVisible = false;
                WardNo.IsVisible = false;
                wardColourPicker.IsVisible = false;
                WardCol.IsVisible = false;

                ButtonFlag = true;
            }
            else if(index == 2)
            {
                NameEntry.Placeholder = "Fear Name...";
                SurnameEntry.IsVisible = false;
                IdNumberEntry.IsVisible = false;
                DOB.IsVisible = false;
                DatePickerT.IsVisible = false;
                Gender.IsVisible = false;
                genderPicker.IsVisible = false;
                RoomNo.IsVisible = false;
                BedNo.IsVisible = false;
                WardNo.IsVisible = false;
                wardColourPicker.IsVisible = false;
                WardCol.IsVisible = false;

                ButtonFlag = true;
            }
        }

        public void ListViewDemoPage()
        {
            Label header = new Label
            {
                Text = "ListView",
                FontSize = Device.GetNamedSize(NamedSize.Large, typeof(Label)),
                HorizontalOptions = LayoutOptions.Center
            };

            // Define some data.
            List<String> fromdb = new List<String>();

            // Create the ListView.
            ListView listView = new ListView
            {
                // Source of data items.
                ItemsSource = fromdb,

                // Define template for displaying each item.
                // (Argument of DataTemplate constructor is called for 
                //      each item; it must return a Cell derivative.)
                ItemTemplate = new DataTemplate(() =>
                {
                    // Create views with bindings for displaying each property.
                    Label nameLabel = new Label();
                    //nameLabel.SetBinding(Label.TextProperty, "Name");
                    

                    Label birthdayLabel = new Label();
                    //birthdayLabel.SetBinding(Label.TextProperty,
                    //    new Binding("Birthday", BindingMode.OneWay,
                    //        null, null, "Born {0:d}"));

                    //BoxView boxView = new BoxView();
                    //boxView.SetBinding(BoxView.ColorProperty, "FavoriteColor");

                    // Return an assembled ViewCell.
                    return new ViewCell
                    {
                        View = new StackLayout
                        {
                            Padding = new Thickness(0, 5),
                            Orientation = StackOrientation.Horizontal,
                            Children =
                                {
                                    //boxView,
                                    new StackLayout
                                    {
                                        VerticalOptions = LayoutOptions.Center,
                                        Spacing = 0,
                                        Children =
                                        {
                                            nameLabel,
                                            birthdayLabel
                                        }
                                        }
                                }
                        }
                    };
                })
            };

            // Accomodate iPhone status bar.
            this.Padding = new Thickness(10, Device.OnPlatform(20, 0, 0), 10, 5);

            // Build the page.
            this.Content = new StackLayout
            {
                Children =
                {
                    header,
                    listView
                }
            };
        }
    }
}