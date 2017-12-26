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

            loadDataForCarouselView();
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
            if (IdNumberEntry.Text == null || NameEntry.Text == null || SurnameEntry.Text == null || selectedGender == null || WardNo.Text == null || RoomNo.Text == null || BedNo.Text == null || selectedColor == null)
            {
                DisplayAlert("Alert", "All the fields must be filled in.", "OK");
            }
            else
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
        }
        async Task AddItem(Patient_Table item)
        {
            await manager.SaveTaskAsync(item);
        }

        public void onCancel_OnClick(Object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new MainPage());
        }

        private void loadDataForCarouselView()
        {
            ObservableCollection<LeftDetail> left = new ObservableCollection<LeftDetail>{
            new LeftDetail
            {
                Title = "Relatives",
                Text = "Woodland Park Zoo1",
                RightTitle = "Hobbies",
                LeftTitle = "",
                RightArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png",
                LeftArrow = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAKlBMVEX9/f3Kysz////k5OXHx8nIyMrz8/P+/v7JycvV1dfGxsjDw8Xc3N3t7e2DX2ioAAACfUlEQVR4nO2au27EIBQFWfAT7/7/7ybFGky6SC4GM7eK0C1mJEvLISe8ykzLGr4T03yebnsM5Tj3txyChkBoDTXkQ2uoIR9awz+GU5ljiWVSPk/zXk/j0eFyWOq89zKpnn7q6d7jcljLxH3evjOlWE6XfJ5uOfa3vF2+17hv54c8p/Ihr8tUvvoc+1t+aUiE1lBDPrSGwxle7jf7NH8np3q65PN0PjpcnkN6+oQqm5tb7Hk6NVfeDpcHSE8aAqE11JAPreFwhvVB4/fXszx0pOk8nffLq0jub3kNl1epT32sSvX0fXnZ6nG5uXnXB8frLfbyOtnh8nT5XrsJROZDDfnQGmrIh9ZwOMPmfnDO0dwP6nGHyznUP5v/GNeV5pGrw+UB0pOGQGgNNeRDa6ghH9rmns09fhnP5t5w+VBDILSGGvKhNbS597SxuUcMROZDDfnQGmrIh9bQ5t5tlTnIss09YiAyH2rIh9ZQQz60hsMZNveDPsp4Nvds7vGhNdSQD62hhnxoDW3u3VaZgyzb3CMGIvOhhnxoDTXkQ2toc+9pY3OPGIjMhxryoTXUkA+toc292ypzkGWbe8RAZD7UkA+toYZ8aA2HM2zuB32U8Wzu2dzjQ2uoIR9aQw350Bra3LutMgdZtrlHDETmQw350BpqyIfW0Obe08bmHjEQmQ815ENrqCEfWkObe7dV5iDLNveIgch8qCEfWkMN+dAaDmfY3A/6KOPZ3LO5x4fWUEM+tIYa8qE1tLl3W2UOsmxzjxiIzIca8qE11JAPraHNvaeNzT1iIDIfasiH1lBDPrSGNvduq8xBlm3uEQOR+VBDPrSGGvKhNdSQD/0/wx/2J+/x6c2V+wAAAABJRU5ErkJggg=="
            },
            new LeftDetail
            {
                Title = "Hobbies",
                Text = "Woodland Park Zoo2",
                LeftTitle = "Relatives",
                RightTitle = "Fears",
                RightArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png",
                LeftArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png"
            },
            new LeftDetail
            {
                Title = "Fears",
                Text = "Woodland Park Zoo3",
                LeftTitle = "Hobbies",
                RightTitle = "",
                RightArrow = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAKlBMVEX9/f3Kysz////k5OXHx8nIyMrz8/P+/v7JycvV1dfGxsjDw8Xc3N3t7e2DX2ioAAACfUlEQVR4nO2au27EIBQFWfAT7/7/7ybFGky6SC4GM7eK0C1mJEvLISe8ykzLGr4T03yebnsM5Tj3txyChkBoDTXkQ2uoIR9awz+GU5ljiWVSPk/zXk/j0eFyWOq89zKpnn7q6d7jcljLxH3evjOlWE6XfJ5uOfa3vF2+17hv54c8p/Ihr8tUvvoc+1t+aUiE1lBDPrSGwxle7jf7NH8np3q65PN0PjpcnkN6+oQqm5tb7Hk6NVfeDpcHSE8aAqE11JAPreFwhvVB4/fXszx0pOk8nffLq0jub3kNl1epT32sSvX0fXnZ6nG5uXnXB8frLfbyOtnh8nT5XrsJROZDDfnQGmrIh9ZwOMPmfnDO0dwP6nGHyznUP5v/GNeV5pGrw+UB0pOGQGgNNeRDa6ghH9rmns09fhnP5t5w+VBDILSGGvKhNbS597SxuUcMROZDDfnQGmrIh9bQ5t5tlTnIss09YiAyH2rIh9ZQQz60hsMZNveDPsp4Nvds7vGhNdSQD62hhnxoDW3u3VaZgyzb3CMGIvOhhnxoDTXkQ2toc+9pY3OPGIjMhxryoTXUkA+toc292ypzkGWbe8RAZD7UkA+toYZ8aA2HM2zuB32U8Wzu2dzjQ2uoIR9aQw350Bra3LutMgdZtrlHDETmQw350BpqyIfW0Obe08bmHjEQmQ815ENrqCEfWkObe7dV5iDLNveIgch8qCEfWkMN+dAaDmfY3A/6KOPZ3LO5x4fWUEM+tIYa8qE1tLl3W2UOsmxzjxiIzIca8qE11JAPraHNvaeNzT1iIDIfasiH1lBDPrSGNvduq8xBlm3uEQOR+VBDPrSGGvKhNdSQD/0/wx/2J+/x6c2V+wAAAABJRU5ErkJggg==",
                LeftArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png"
            }
        };
        LeftCarousel.ItemsSource = left;
        }
    }
}