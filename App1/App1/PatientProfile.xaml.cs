using App1.DatabaseStuff;
using App1.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PatientProfile : ContentPage
    {
        public string currentUserId = "1234567890";
        ItemManager manager;
        string selectedMedical;
        int historyID = 1000;
        
        bool isMedical = false;
        bool isDanger = false;

        public PatientProfile()
        {
            InitializeComponent();
            manager = ItemManager.DefaultManager;
            imgProfile.Source = ImageSource.FromFile("Assets/profile.png");
            imgEditProfile.Source = ImageSource.FromFile("Assets/editImage.png");

            loadLeftCarousel(currentUserId);
        }
        
        private async Task loadLeftCarousel(string currentUserId)
        {
            var relative_items = await manager.GetRelativeItemsAsync(currentUserId);
            ObservableCollection<values> rels = new ObservableCollection<values>();
            foreach (Relative_Table r in relative_items)
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
            LeftCarouselMain.ItemsSource = left;
        }


        private async void MedHist_Clicked(object sender, EventArgs e)
        {

            isMedical = true;
            isDanger = false;

            var history_items = await manager.GetHistoryItemsAsync(currentUserId);
            ObservableCollection<MedicalHistoryContent> med = new ObservableCollection<MedicalHistoryContent>();
            foreach (Patient_History h in history_items)
            {
                med.Add(new MedicalHistoryContent
                {
                    description = h.Text,
                    type = h.Type,
                    year = h.Year,
                    patientid = h.PatientID_FK
                });
            }
            MainContentView.Content = new ContentView
            {
                Content = new ListView { ItemsSource = med, RowHeight = 40, Margin = 20 },
            };

            MedHistButton.BackgroundColor =Color.FromHex("#0080F0");
            MedHistButton.TextColor = Color.White;

            //MapButton.BackgroundColor = Color.White;
            //MapButton.TextColor = Color.Black;
            AlarmsButton.BackgroundColor = Color.White;
            AlarmsButton.TextColor = Color.Black;
            ObservationsButton.BackgroundColor = Color.White;
            ObservationsButton.TextColor = Color.Black;
            DangersButton.BackgroundColor = Color.White;
            DangersButton.TextColor = Color.Black;

            EditButton2.IsVisible = true;
        }

        private void createNewMedical_OnClick(object sender, EventArgs e)
        {
            if (isMedical == true && isDanger == false)
            {
                DangerEntry.IsVisible = false;
                typePicker.SelectedIndex = 1;
                EditButton2.IsVisible = false;
                MainContentView.IsVisible = false;
                fields.IsVisible = true;

                List<string> values = new List<string>() { "Surgical", "Medical", "Allergies" };
                typePicker.ItemsSource = values;
            }
            else if (isMedical == false && isDanger == true)
            {
                EditButton2.IsVisible = false;
                MainContentView.IsVisible = false;
                typePicker.IsVisible = false;
                pickerLabel.IsVisible = false;
                TextEntry.IsVisible = false;
                YearEntry.IsVisible = false;
                fields.IsVisible = true;
                DangerEntry.IsVisible = true;
            }
        }

        private void medType_IndexChanged(object sender, EventArgs e)
        {
            selectedMedical = typePicker.Items[typePicker.SelectedIndex];
        }

        private void Cancel_OnClick(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new PatientProfile());
        }

        private async void Submit_OnClick(object sender, EventArgs e)
        {
                if (isMedical == true && isDanger == false)
                {
                    if (TextEntry.Text == null || YearEntry.Text == null || typePicker.SelectedItem == null)
                    {
                        DisplayAlert("Alert", "All the fields must be filled in.", "OK");
                    }
                    else
                    {
                        var todo = new Patient_History { Type = selectedMedical, Year = YearEntry.Text, Text = TextEntry.Text, PatientID_FK = currentUserId, History_id = historyID.ToString() };
                        await AddMedicalItem(todo);
                    }
                    historyID++;

                    //unassign values
                    typePicker.SelectedIndex = 0;
                    TextEntry.Text = String.Empty;
                    YearEntry.Text = String.Empty;

                    EditButton2.IsVisible = true;
                    fields.IsVisible = false;
                    MainContentView.IsVisible = true;
                    MedHist_Clicked(sender, e);
                }
            
                 if (isMedical == false && isDanger == true)
                 {
                    if (DangerEntry.Text == null)
                    {
                        DisplayAlert("Alert", "All the fields must be filled in.", "OK");
                    }
                    else
                    {
                        var todo = new DangerActual_Table { PatientID_FK = currentUserId, Text = DangerEntry.Text };
                        await AddActualDangerItem(todo);
                    }

                    DangerEntry.Text = String.Empty;
                    fields.IsVisible = false;
                    MainContentView.IsVisible = true;
                    Dangers_Clicked(sender, e);
                 }
        }

        async Task AddMedicalItem(Patient_History item)
        {
            await manager.SaveTaskAsyncPatientMedical(item);
        }

        async Task AddActualDangerItem(DangerActual_Table item)
        {
            await manager.SaveTaskAsyncDangerActualItem(item);
        }
        //private void Map_Clicked(object sender, EventArgs e)
        //{
        //    MainContentView.Content = new ContentView
        //    {
        //        Content = new Image { Source = "https://www.mcmaster.ca/uts/maps/images/bsb1.gif" },
        //    };

        //    MapButton.BackgroundColor = Color.FromHex("#0080F0");
        //    MapButton.TextColor = Color.White;

        //    MedHistButton.BackgroundColor = Color.White;
        //    MedHistButton.TextColor = Color.Black;
        //    AlarmsButton.BackgroundColor = Color.White;
        //    AlarmsButton.TextColor = Color.Black;
        //    ObservationsButton.BackgroundColor = Color.White;
        //    ObservationsButton.TextColor = Color.Black;
        //    DangersButton.BackgroundColor = Color.White;
        //    DangersButton.TextColor = Color.Black;
        //}

        private async void Alarms_Clicked(object sender, EventArgs e)
        {
            isMedical = false;
            isDanger = false;

            EditButton2.IsVisible = false;
            fields.IsVisible = false;
            MainContentView.IsVisible = true;

            ObservableCollection<Patient_Alarm_Table> patientAlarmTable = await manager.GetPatientAlarmTableItemsAsync();
            ObservableCollection<Alarm_Table> alarmTable = await manager.GetAlarmTableItemsAsync();
            ObservableCollection<Danger_Table> dangerTable = await manager.GetDangerTableItemsAsync();
            ObservableCollection<LUT_Alarm_Danger_Category> lutAlarmDangerCategory = await manager.GetLUT_Alarm_Danger_CategoryTableItemsAsync();

            //for predictions
            var alarms = (from patientAlarm in patientAlarmTable
                         join alarm in alarmTable on patientAlarm.Alarm_id equals alarm.Alarm_id
                         join danger in dangerTable on alarm.DangerID equals danger.Danger_id
                         join lut_alarm_danger in lutAlarmDangerCategory on danger.AlarmDanger_CategoryID equals lut_alarm_danger.Lut_alarm_Danger_Category_ID
                         where (danger.Alarmtypeid == 1)
                         select new { lut_alarm_danger.Name, alarm.TimeCreated }).ToList().Distinct();

            List<String> alarmsList = new List<string>();
            List<String> timeList = new List<String>();
            foreach (var q in alarms)
            {
                alarmsList.Add(q.Name);
            }
            foreach (var t in alarms)
            {
                timeList.Add(t.TimeCreated);
            }

            ObservableCollection<AlarmsContent> alarmslist = new ObservableCollection<AlarmsContent>();

            for (int i = 0; i < alarmsList.Count; i++)
            {
                alarmslist.Add(new AlarmsContent
                {
                    description = alarmsList[i].ToString(),
                    time = timeList[i].ToString()
                });
            }
            MainContentView.Content = new ContentView
            {
                Content = new ListView { ItemsSource = alarmslist, RowHeight = 40, Margin = 20 },
            };

            AlarmsButton.BackgroundColor = Color.FromHex("#0080F0");
            AlarmsButton.TextColor = Color.White;

            //MapButton.BackgroundColor = Color.White;
           // MapButton.TextColor = Color.Black;
            MedHistButton.BackgroundColor = Color.White;
            MedHistButton.TextColor = Color.Black;
            ObservationsButton.BackgroundColor = Color.White;
            ObservationsButton.TextColor = Color.Black;
            DangersButton.BackgroundColor = Color.White;
            DangersButton.TextColor = Color.Black;
        }

        private async void Observations_Clicked(object sender, EventArgs e)
        {
            isMedical = false;
            isDanger = false;

            EditButton2.IsVisible = false;
            fields.IsVisible = false;
            MainContentView.IsVisible = true;

            ObservableCollection<Patient_Alarm_Table> patientAlarmTable = await manager.GetPatientAlarmTableItemsAsync();
            ObservableCollection<Alarm_Table> alarmTable = await manager.GetAlarmTableItemsAsync();
            ObservableCollection<Danger_Table> dangerTable = await manager.GetDangerTableItemsAsync();
            ObservableCollection<LUT_Alarm_Danger_Category> lutAlarmDangerCategory = await manager.GetLUT_Alarm_Danger_CategoryTableItemsAsync();

            //for predictions
            var obs = (from patientAlarm in patientAlarmTable
                         join alarm in alarmTable on patientAlarm.Alarm_id equals alarm.Alarm_id
                         join danger in dangerTable on alarm.DangerID equals danger.Danger_id
                         join lut_alarm_danger in lutAlarmDangerCategory on danger.AlarmDanger_CategoryID equals lut_alarm_danger.Lut_alarm_Danger_Category_ID
                         where (danger.Alarmtypeid == 2)
                         select new { lut_alarm_danger.Name, alarm.TimeCreated }).ToList().Distinct();
            List<String> obsList = new List<string>();
            List<String> timeList = new List<String>();
            foreach (var q in obs)
            {
                obsList.Add(q.Name);
            }
            foreach (var t in obs)
            {
                timeList.Add(t.TimeCreated);
            }
            ObservableCollection<AlarmsContent> alarmslist = new ObservableCollection<AlarmsContent>();
            for (int i = 0; i < obsList.Count; i++)
            {
                alarmslist.Add(new AlarmsContent
                {
                    description = obsList[i].ToString(),
                    time = timeList[i].ToString()
                });
            }
            MainContentView.Content = new ContentView
            {
                Content = new ListView { ItemsSource = alarmslist, RowHeight = 40, Margin = 20 },
            };

            ObservationsButton.BackgroundColor = Color.FromHex("#0080F0");
            ObservationsButton.TextColor = Color.White;

            //MapButton.BackgroundColor = Color.White;
            //MapButton.TextColor = Color.Black;
            AlarmsButton.BackgroundColor = Color.White;
            AlarmsButton.TextColor = Color.Black;
            MedHistButton.BackgroundColor = Color.White;
            MedHistButton.TextColor = Color.Black;
            DangersButton.BackgroundColor = Color.White;
            DangersButton.TextColor = Color.Black;
        }

        private async void Dangers_Clicked(object sender, EventArgs e)
        {
            isMedical = false;
            isDanger = true;

            EditButton2.IsVisible = true;
            fields.IsVisible = false;
            MainContentView.IsVisible = true;

            var danger = await manager.GetDangerActualItemsAsync(currentUserId); 
            ObservableCollection<Dangers> dangers = new ObservableCollection<Dangers>();
            foreach (DangerActual_Table d in danger)
            {
                dangers.Add(new Dangers
                {
                    description = d.Text,
                    patientID = d.PatientID_FK
                });
            }

            //ObservableCollection<Dangers> dangers = new ObservableCollection<Dangers>();
            //dangers.Add(new Dangers { description = "Tends to get aggressive near people" });
            //dangers.Add(new Dangers { description = "Racist" });
            //dangers.Add(new Dangers { description = "Forgetful due to dimensia" });


            MainContentView.Content = new ContentView
            {
                Content = new ListView { ItemsSource = dangers , RowHeight = 40, Margin = 20 },
            };

            DangersButton.BackgroundColor = Color.FromHex("#0080F0");
            DangersButton.TextColor = Color.White;

            //MapButton.BackgroundColor = Color.White;
            //MapButton.TextColor = Color.Black;
            AlarmsButton.BackgroundColor = Color.White;
            AlarmsButton.TextColor = Color.Black;
            ObservationsButton.BackgroundColor = Color.White;
            ObservationsButton.TextColor = Color.Black;
            MedHistButton.BackgroundColor = Color.White;
            MedHistButton.TextColor = Color.Black;
        }

        private void ShowPhoneNo(object sender, ItemTappedEventArgs e)
        {
            if(LeftCarouselMain.Position == 0)
            {
                ListView l = (ListView)sender;
                values v = (values)l.SelectedItem;
                DisplayAlert("Contact Number", v.phoneno, "OK");
            }
        }

        private void createNew_OnClick2(object sender, ItemTappedEventArgs e)
        {
            var index = LeftCarouselMain.Position;
            MedHistButton.IsEnabled = false;
            AlarmsButton.IsEnabled = false;
            ObservationsButton.IsEnabled = false;
            DangersButton.IsEnabled = false;
            MainContentView.IsVisible = false;

            if (index == 0)
            {
                typePicker.IsVisible = false;
                pickerLabel.IsVisible = false;
                TextEntry.IsVisible = false;
                YearEntry.IsVisible = false;
                DangerEntry.IsVisible = false;
                fields.IsVisible = true;
                ProfileNameEntry.IsVisible = true;
                ProfileSurnameEntry.IsVisible = true;
                ProfileNameEntry.Placeholder = "Relative Name...";
                ProfileSurnameEntry.Placeholder = "Relative Surname...";
                ProfileIdNumberEntry.Placeholder = "Relative ID...";
                ProfileAdditionalEntry.IsVisible = true;
                ProfileAdditionalEntry.Placeholder = "Relative Phone...";
                ProfileAdditionalEntry2.IsVisible = true;
                ProfileAdditionalEntry2.Placeholder = "Relative Type...";

                tabButtons.IsVisible = false;
                carouselButtons.IsVisible = true;
            }
            else if (index == 1)
            {
                typePicker.IsVisible = false;
                pickerLabel.IsVisible = false;
                TextEntry.IsVisible = false;
                YearEntry.IsVisible = false;
                DangerEntry.IsVisible = false;
                fields.IsVisible = true;
                ProfileNameEntry.Placeholder = "Hobby Name...";
                ProfileSurnameEntry.IsVisible = false;
                ProfileIdNumberEntry.IsVisible = false;
                ProfileAdditionalEntry.IsVisible = false;
                ProfileAdditionalEntry2.IsVisible = false;

                tabButtons.IsVisible = false;
                carouselButtons.IsVisible = true;
            }
            else if (index == 2)
            {
                typePicker.IsVisible = false;
                pickerLabel.IsVisible = false;
                TextEntry.IsVisible = false;
                YearEntry.IsVisible = false;
                DangerEntry.IsVisible = false;
                fields.IsVisible = true;
                ProfileNameEntry.Placeholder = "Fear Name...";
                ProfileSurnameEntry.IsVisible = false;
                ProfileIdNumberEntry.IsVisible = false;
                ProfileAdditionalEntry.IsVisible = false;
                ProfileAdditionalEntry2.IsVisible = false;

                tabButtons.IsVisible = false;
                carouselButtons.IsVisible = true;
            }
        }

        private async void Submit_OnClick2(object sender, EventArgs e)
        {
            MedHistButton.IsEnabled = true;
            AlarmsButton.IsEnabled = true;
            ObservationsButton.IsEnabled = true;
            DangersButton.IsEnabled = true;
            fields.IsVisible = false;
            tabButtons.IsVisible = true;
            carouselButtons.IsVisible = false;

            if (LeftCarouselMain.Position == 0)
            {
                if (ProfileNameEntry.Text == null || ProfileSurnameEntry.Text == null || ProfileIdNumberEntry.Text == null || ProfileAdditionalEntry.Text == null)
                {
                    DisplayAlert("Alert", "All the fields must be filled in.", "OK");
                }
                else
                {
                    var todo = new Relative_Table { Rel_id = ProfileIdNumberEntry.Text, Name = ProfileIdNumberEntry.Text, Surname = ProfileSurnameEntry.Text, Phone_no = ProfileAdditionalEntry.Text, Rel_type = ProfileAdditionalEntry2.Text, PatientID_FK = currentUserId };
                    await AddRelativeItem(todo);
                }

                ProfileNameEntry.Text = null;
                ProfileSurnameEntry.Text = null;
                ProfileIdNumberEntry.Text = null;
                ProfileAdditionalEntry.Text = null;
                ProfileAdditionalEntry2.Text = null;
                loadLeftCarousel(currentUserId);
            }
            else if (LeftCarouselMain.Position == 1)
            {
                if (ProfileNameEntry.Text == null)
                {
                    DisplayAlert("Alert", "All the fields must be filled in.", "OK");
                }
                else
                {
                    var totalHobbies = await manager.GetHobbyItemsCountAsync();

                    var todo = new Hobby_Table { Hobby_name = ProfileNameEntry.Text, PatientID_FK = currentUserId, Hobby_id = totalHobbies.Count.ToString() };
                    await AddHobbyItem(todo);
                }

                ProfileNameEntry.Text = null;
                ProfileSurnameEntry.Text = null;
                ProfileIdNumberEntry.Text = null;
                ProfileAdditionalEntry.Text = null;
                ProfileAdditionalEntry2.Text = null;
                loadLeftCarousel(currentUserId);
            }
            else if (LeftCarouselMain.Position == 2)
            {
                if (ProfileNameEntry.Text == null)
                {
                    DisplayAlert("Alert", "All the fields must be filled in.", "OK");
                }
                else
                {
                    var totalFears = await manager.GetFearItemsCountAsync();

                    var todo = new Fear_Table { Fear_name = ProfileNameEntry.Text, PatientID_FK = currentUserId, Fear_id = totalFears.Count.ToString() };
                    await AddFearItem(todo);
                }

                ProfileNameEntry.Text = null;
                ProfileSurnameEntry.Text = null;
                ProfileIdNumberEntry.Text = null;
                ProfileAdditionalEntry.Text = null;
                ProfileAdditionalEntry2.Text = null;
                loadLeftCarousel(currentUserId);
            }
        }

        private void Cancel_OnClick2(object sender, EventArgs e)
        {
            this.Navigation.PushAsync(new PatientProfile());
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
    }
}