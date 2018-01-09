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

            loadDataForCarouselViewAsync();
            
            if (currentUserId == null)
            {
                EditButton.IsEnabled = false;
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
            if (ButtonFlag == true && LeftCarousel.Position == 0)
            {
                if (NameEntry.Text == null || SurnameEntry.Text == null || IdNumberEntry.Text == null || AdditionalEntry.Text == null)
                {
                    DisplayAlert("Alert", "All the fields must be filled in.", "OK");
                }
                else
                {
                    var todo = new Relative_Table { Rel_id = IdNumberEntry.Text, Name = NameEntry.Text, Surname = SurnameEntry.Text, Phone_no = AdditionalEntry.Text,Rel_type = AdditionalEntry2.Text , PatientID_FK = currentUserId };
                    await AddRelativeItem(todo);
                }
                ButtonFlag = false;

                //Reset Components
                NameEntry.Placeholder = "Enter Name...";
                NameEntry.Text = null;
                SurnameEntry.Text = null;
                IdNumberEntry.Text = null;
                AdditionalEntry.IsVisible = false;
                AdditionalEntry2.IsVisible = false;
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

                var relative_items = await manager.GetRelativeItemsAsync(currentUserId);
            }
            else if (ButtonFlag == true && LeftCarousel.Position == 1)
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
                AdditionalEntry2.IsVisible = false;
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

                var hobby_items = await manager.GetHobbyItemsAsync(currentUserId);
            }
            else if (ButtonFlag == true && LeftCarousel.Position == 2)
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
                AdditionalEntry2.IsVisible = false;
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

                var fear_items = await manager.GetFearItemsAsync(currentUserId);
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
                    
                    //Set Image and Respective Entries
                    NameSurnameProfileLabel.Text = NameEntry.Text + " " + SurnameEntry.Text;
                    GenderProfileLabel.Text = selectedGender + " | ";
                    String dateTime = GetDateTime().ToString();
                    String year = "";
                    for (int i = 6; i <= 9; i++)
                    {
                        year += dateTime[i];
                    }
                    int ageDiff = 2018 - Convert.ToInt32(year);
                    AgeProfileLabel.Text = ageDiff.ToString() + " Years";
                    WardNoProfileLabel.Text = "Ward No: " + WardNo.Text + " | ";
                    WardColProfileLabel.Text = selectedColor + " | ";
                    RoomNoProfileLabel.Text = "Room No: " + RoomNo.Text + " | ";
                    BedNoProfileLabel.Text = "Bed No: " + BedNo.Text;
                    if (selectedGender.Equals("Male"))
                    {
                        imgProfile.Source = ImageSource.FromFile("Assets/male.png");
                    }
                    else if (selectedGender.Equals("Female"))
                    {
                        imgProfile.Source = ImageSource.FromFile("Assets/female.png");
                    }

                    IdNumberEntry.Text = String.Empty;
                    NameEntry.Text = String.Empty;
                    SurnameEntry.Text = String.Empty;
                    DatePickerT.Date = DatePickerT.MaximumDate;
                    genderPicker.SelectedIndex = -1;
                    wardColourPicker.SelectedIndex = -1;
                    WardNo.Text = String.Empty;
                    RoomNo.Text = String.Empty;
                    BedNo.Text = String.Empty;

                    EditButton.IsEnabled = true;
                }
            }
            loadDataForCarouselViewAsync();
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
            var index = LeftCarousel.Position;
            
            if(index == 0)
            {
                AdditionalEntry.IsVisible = true;
                AdditionalEntry2.IsVisible = true;
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

        private async Task loadDataForCarouselViewAsync()
        {
            var relative_items = await manager.GetRelativeItemsAsync(currentUserId);
            ObservableCollection<values> rels = new ObservableCollection<values>();
            foreach(Relative_Table r in relative_items)
            {
                rels.Add(new values
                {
                    name = r.Name + " " + r.Surname,
                    patientid = r.PatientID_FK,
                    relation = r.Rel_type,
                    phoneno = r.Phone_no
                });
            }
            var hobby_items = await manager.GetHobbyItemsAsync(currentUserId);
            ObservableCollection<values> hobbies = new ObservableCollection<values>();
            foreach (Hobby_Table h in hobby_items)
            {
                hobbies.Add(new values
                {
                    name = h.Hobby_name,
                    patientid = h.PatientID_FK,
                    relation = "",
                    phoneno = ""
                });
            }

            var fear_items = await manager.GetFearItemsAsync(currentUserId);
            ObservableCollection<values> fears = new ObservableCollection<values>();
            foreach (Fear_Table f in fear_items)
            {
                fears.Add(new values
                {
                    name = f.Fear_name,
                    patientid = f.PatientID_FK,
                    relation = "",
                    phoneno = ""
                });
            }

            List<LeftDetail> left = new List<LeftDetail>{
                new LeftDetail
                {
                    Title = "Relatives",
                    RightTitle = "Hobbies",
                    LeftTitle = "",
                    RightArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png",
                    LeftArrow = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAKlBMVEX9/f3Kysz////k5OXHx8nIyMrz8/P+/v7JycvV1dfGxsjDw8Xc3N3t7e2DX2ioAAACfUlEQVR4nO2au27EIBQFWfAT7/7/7ybFGky6SC4GM7eK0C1mJEvLISe8ykzLGr4T03yebnsM5Tj3txyChkBoDTXkQ2uoIR9awz+GU5ljiWVSPk/zXk/j0eFyWOq89zKpnn7q6d7jcljLxH3evjOlWE6XfJ5uOfa3vF2+17hv54c8p/Ihr8tUvvoc+1t+aUiE1lBDPrSGwxle7jf7NH8np3q65PN0PjpcnkN6+oQqm5tb7Hk6NVfeDpcHSE8aAqE11JAPreFwhvVB4/fXszx0pOk8nffLq0jub3kNl1epT32sSvX0fXnZ6nG5uXnXB8frLfbyOtnh8nT5XrsJROZDDfnQGmrIh9ZwOMPmfnDO0dwP6nGHyznUP5v/GNeV5pGrw+UB0pOGQGgNNeRDa6ghH9rmns09fhnP5t5w+VBDILSGGvKhNbS597SxuUcMROZDDfnQGmrIh9bQ5t5tlTnIss09YiAyH2rIh9ZQQz60hsMZNveDPsp4Nvds7vGhNdSQD62hhnxoDW3u3VaZgyzb3CMGIvOhhnxoDTXkQ2toc+9pY3OPGIjMhxryoTXUkA+toc292ypzkGWbe8RAZD7UkA+toYZ8aA2HM2zuB32U8Wzu2dzjQ2uoIR9aQw350Bra3LutMgdZtrlHDETmQw350BpqyIfW0Obe08bmHjEQmQ815ENrqCEfWkObe7dV5iDLNveIgch8qCEfWkMN+dAaDmfY3A/6KOPZ3LO5x4fWUEM+tIYa8qE1tLl3W2UOsmxzjxiIzIca8qE11JAPraHNvaeNzT1iIDIfasiH1lBDPrSGNvduq8xBlm3uEQOR+VBDPrSGGvKhNdSQD/0/wx/2J+/x6c2V+wAAAABJRU5ErkJggg==",
                    obscollection = rels
                },
                new LeftDetail
                {
                    Title = "Hobbies",
                    LeftTitle = "Relatives",
                    RightTitle = "Fears",
                    RightArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png",
                    LeftArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png",
                    obscollection = hobbies
                },
                new LeftDetail
                {
                    Title = "Fears",
                    LeftTitle = "Hobbies",
                    RightTitle = "",
                    RightArrow = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAKlBMVEX9/f3Kysz////k5OXHx8nIyMrz8/P+/v7JycvV1dfGxsjDw8Xc3N3t7e2DX2ioAAACfUlEQVR4nO2au27EIBQFWfAT7/7/7ybFGky6SC4GM7eK0C1mJEvLISe8ykzLGr4T03yebnsM5Tj3txyChkBoDTXkQ2uoIR9awz+GU5ljiWVSPk/zXk/j0eFyWOq89zKpnn7q6d7jcljLxH3evjOlWE6XfJ5uOfa3vF2+17hv54c8p/Ihr8tUvvoc+1t+aUiE1lBDPrSGwxle7jf7NH8np3q65PN0PjpcnkN6+oQqm5tb7Hk6NVfeDpcHSE8aAqE11JAPreFwhvVB4/fXszx0pOk8nffLq0jub3kNl1epT32sSvX0fXnZ6nG5uXnXB8frLfbyOtnh8nT5XrsJROZDDfnQGmrIh9ZwOMPmfnDO0dwP6nGHyznUP5v/GNeV5pGrw+UB0pOGQGgNNeRDa6ghH9rmns09fhnP5t5w+VBDILSGGvKhNbS597SxuUcMROZDDfnQGmrIh9bQ5t5tlTnIss09YiAyH2rIh9ZQQz60hsMZNveDPsp4Nvds7vGhNdSQD62hhnxoDW3u3VaZgyzb3CMGIvOhhnxoDTXkQ2toc+9pY3OPGIjMhxryoTXUkA+toc292ypzkGWbe8RAZD7UkA+toYZ8aA2HM2zuB32U8Wzu2dzjQ2uoIR9aQw350Bra3LutMgdZtrlHDETmQw350BpqyIfW0Obe08bmHjEQmQ815ENrqCEfWkObe7dV5iDLNveIgch8qCEfWkMN+dAaDmfY3A/6KOPZ3LO5x4fWUEM+tIYa8qE1tLl3W2UOsmxzjxiIzIca8qE11JAPraHNvaeNzT1iIDIfasiH1lBDPrSGNvduq8xBlm3uEQOR+VBDPrSGGvKhNdSQD/0/wx/2J+/x6c2V+wAAAABJRU5ErkJggg==",
                    LeftArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png",
                    obscollection = fears
                }
            };
            LeftCarousel.ItemsSource = left;
            ListView l = this.FindByName<ListView>("listV");
            
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

        private void ShowPhoneNo(object sender, ItemTappedEventArgs e)
        {
            if (LeftCarousel.Position == 0)
            {
                ListView l = (ListView)sender;
                values v = (values)l.SelectedItem;
                DisplayAlert("Contact Number", v.phoneno, "OK");
            }
        }
    }
}