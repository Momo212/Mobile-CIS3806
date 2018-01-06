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
        public string currentUserId="12345";
        ItemManager manager;

        public PatientProfile()
        {
            InitializeComponent();
            manager = ItemManager.DefaultManager;
            imgProfile.Source = ImageSource.FromFile("Assets/profile.png");
            imgEditProfile.Source = ImageSource.FromFile("Assets/editImage.png");

            loadLeftCarousel(currentUserId);
            loadMainCarousel(currentUserId);
        }

        public async void GetPatientAlarmsRealtime(string currentUserID) //ALARM/REALTIME - NOT PREDICTION/OBSERVATIONS
        {
            ObservableCollection<Patient_Alarm_Table> patientAlarmTable = await manager.GetPatientAlarmTableItemsAsync(currentUserID);
            ObservableCollection<Alarm_Table> alarmTable = await manager.GetAlarmTableItemsAsync();
            ObservableCollection<Danger_Table> dangerTable = await manager.GetDangerTableItemsAsync();
            ObservableCollection<LUT_Alarm_Danger_Category> lutAlarmDangerCategory = await manager.GetLUT_Alarm_Danger_CategoryTableItemsAsync();

            //for predictions
            var query = from patientAlarm in patientAlarmTable
                        join alarm in alarmTable on patientAlarm.Alarm_id equals alarm.Alarm_id
                        join danger in dangerTable on alarm.DangerID equals danger.Danger_id
                        join lut_alarm_danger in lutAlarmDangerCategory on danger.AlarmDanger_CategoryID equals lut_alarm_danger.Lut_alarm_Danger_Category_ID
                        where (danger.Alarmtypeid == 3)
                        select new { lut_alarm_danger.Name};

            uint i = 1;
        }

        public async void GetPatientAlarmsPrediction(string currentUserID) //PREDICTION/OBSERVATIONS - NOT ALARM/REALTIME
        {
            ObservableCollection<Patient_Alarm_Table> patientAlarmTable = await manager.GetPatientAlarmTableItemsAsync(currentUserID);
            ObservableCollection<Alarm_Table> alarmTable = await manager.GetAlarmTableItemsAsync();
            ObservableCollection<Danger_Table> dangerTable = await manager.GetDangerTableItemsAsync();
            ObservableCollection<LUT_Alarm_Danger_Category> lutAlarmDangerCategory = await manager.GetLUT_Alarm_Danger_CategoryTableItemsAsync();

            //for predictions
            var query = from patientAlarm in patientAlarmTable
                        join alarm in alarmTable on patientAlarm.Alarm_id equals alarm.Alarm_id
                        join danger in dangerTable on alarm.DangerID equals danger.Danger_id
                        join lut_alarm_danger in lutAlarmDangerCategory on danger.AlarmDanger_CategoryID equals lut_alarm_danger.Lut_alarm_Danger_Category_ID
                        where (danger.Alarmtypeid == 4)
                        select new { lut_alarm_danger.Name };

            uint i = 1;
        }

        public async Task<List<Patient_History>> getHistory()
        {
            var history_items = await manager.GetHistoryItemsAsync("301997m");
            return new List<Patient_History>(history_items);
        }

        private async Task loadMainCarousel(string currentUserId)
        {
            //var relative_items = await manager.GetRelativeItemsAsync(currentUserId);
            //ObservableCollection<values> rels = new ObservableCollection<values>();
            //foreach (Relative_Table r in relative_items)
            //{
            //    rels.Add(new values
            //    {
            //        name = r.Name + " " + r.Surname,
            //        patientid = r.PatientID_FK,
            //        relation = r.Rel_id,
            //        phoneno = r.Phone_no
            //    });
            //}
            //var hobby_items = await manager.GetHobbyItemsAsync(currentUserId);
            //ObservableCollection<values> hobbies = new ObservableCollection<values>();
            //foreach (Hobby_Table h in hobby_items)
            //{
            //    hobbies.Add(new values
            //    {
            //        name = h.Hobby_name,
            //        patientid = h.PatientID_FK,
            //        relation = "",
            //        phoneno = ""
            //    });
            //}

            //var fear_items = await manager.GetFearItemsAsync(currentUserId);
            //ObservableCollection<values> fears = new ObservableCollection<values>();
            //foreach (Fear_Table f in fear_items)
            //{
            //    fears.Add(new values
            //    {
            //        name = f.Fear_name,
            //        patientid = f.PatientID_FK,
            //        relation = "",
            //        phoneno = ""
            //    });
            //}

            //List<LeftDetail> left = new List<LeftDetail>{
            //    new LeftDetail
            //    {
            //        Title = "Relatives",
            //        RightTitle = "Hobbies",
            //        LeftTitle = "",
            //        RightArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png",
            //        LeftArrow = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAKlBMVEX9/f3Kysz////k5OXHx8nIyMrz8/P+/v7JycvV1dfGxsjDw8Xc3N3t7e2DX2ioAAACfUlEQVR4nO2au27EIBQFWfAT7/7/7ybFGky6SC4GM7eK0C1mJEvLISe8ykzLGr4T03yebnsM5Tj3txyChkBoDTXkQ2uoIR9awz+GU5ljiWVSPk/zXk/j0eFyWOq89zKpnn7q6d7jcljLxH3evjOlWE6XfJ5uOfa3vF2+17hv54c8p/Ihr8tUvvoc+1t+aUiE1lBDPrSGwxle7jf7NH8np3q65PN0PjpcnkN6+oQqm5tb7Hk6NVfeDpcHSE8aAqE11JAPreFwhvVB4/fXszx0pOk8nffLq0jub3kNl1epT32sSvX0fXnZ6nG5uXnXB8frLfbyOtnh8nT5XrsJROZDDfnQGmrIh9ZwOMPmfnDO0dwP6nGHyznUP5v/GNeV5pGrw+UB0pOGQGgNNeRDa6ghH9rmns09fhnP5t5w+VBDILSGGvKhNbS597SxuUcMROZDDfnQGmrIh9bQ5t5tlTnIss09YiAyH2rIh9ZQQz60hsMZNveDPsp4Nvds7vGhNdSQD62hhnxoDW3u3VaZgyzb3CMGIvOhhnxoDTXkQ2toc+9pY3OPGIjMhxryoTXUkA+toc292ypzkGWbe8RAZD7UkA+toYZ8aA2HM2zuB32U8Wzu2dzjQ2uoIR9aQw350Bra3LutMgdZtrlHDETmQw350BpqyIfW0Obe08bmHjEQmQ815ENrqCEfWkObe7dV5iDLNveIgch8qCEfWkMN+dAaDmfY3A/6KOPZ3LO5x4fWUEM+tIYa8qE1tLl3W2UOsmxzjxiIzIca8qE11JAPraHNvaeNzT1iIDIfasiH1lBDPrSGNvduq8xBlm3uEQOR+VBDPrSGGvKhNdSQD/0/wx/2J+/x6c2V+wAAAABJRU5ErkJggg==",
            //        obscollection = rels
            //    },
            //    new LeftDetail
            //    {
            //        Title = "Hobbies",
            //        LeftTitle = "Relatives",
            //        RightTitle = "Fears",
            //        RightArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png",
            //        LeftArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png",
            //        obscollection = hobbies
            //    },
            //    new LeftDetail
            //    {
            //        Title = "Fears",
            //        LeftTitle = "Hobbies",
            //        RightTitle = "",
            //        RightArrow = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAOEAAADhCAMAAAAJbSJIAAAAKlBMVEX9/f3Kysz////k5OXHx8nIyMrz8/P+/v7JycvV1dfGxsjDw8Xc3N3t7e2DX2ioAAACfUlEQVR4nO2au27EIBQFWfAT7/7/7ybFGky6SC4GM7eK0C1mJEvLISe8ykzLGr4T03yebnsM5Tj3txyChkBoDTXkQ2uoIR9awz+GU5ljiWVSPk/zXk/j0eFyWOq89zKpnn7q6d7jcljLxH3evjOlWE6XfJ5uOfa3vF2+17hv54c8p/Ihr8tUvvoc+1t+aUiE1lBDPrSGwxle7jf7NH8np3q65PN0PjpcnkN6+oQqm5tb7Hk6NVfeDpcHSE8aAqE11JAPreFwhvVB4/fXszx0pOk8nffLq0jub3kNl1epT32sSvX0fXnZ6nG5uXnXB8frLfbyOtnh8nT5XrsJROZDDfnQGmrIh9ZwOMPmfnDO0dwP6nGHyznUP5v/GNeV5pGrw+UB0pOGQGgNNeRDa6ghH9rmns09fhnP5t5w+VBDILSGGvKhNbS597SxuUcMROZDDfnQGmrIh9bQ5t5tlTnIss09YiAyH2rIh9ZQQz60hsMZNveDPsp4Nvds7vGhNdSQD62hhnxoDW3u3VaZgyzb3CMGIvOhhnxoDTXkQ2toc+9pY3OPGIjMhxryoTXUkA+toc292ypzkGWbe8RAZD7UkA+toYZ8aA2HM2zuB32U8Wzu2dzjQ2uoIR9aQw350Bra3LutMgdZtrlHDETmQw350BpqyIfW0Obe08bmHjEQmQ815ENrqCEfWkObe7dV5iDLNveIgch8qCEfWkMN+dAaDmfY3A/6KOPZ3LO5x4fWUEM+tIYa8qE1tLl3W2UOsmxzjxiIzIca8qE11JAPraHNvaeNzT1iIDIfasiH1lBDPrSGNvduq8xBlm3uEQOR+VBDPrSGGvKhNdSQD/0/wx/2J+/x6c2V+wAAAABJRU5ErkJggg==",
            //        LeftArrow = "http://www.clker.com/cliparts/1/8/Y/K/v/W/orange-left-arrow-md.png",
            //        obscollection = fears
            //    }
            //};
            //MainCarousel.ItemsSource = left;
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
                    relation = r.Rel_id,
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
    }
}