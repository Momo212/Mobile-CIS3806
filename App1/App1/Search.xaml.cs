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
    public partial class Search : ContentPage
    {
        List<String> patient = new List<string>();
        ObservableCollection<MedicalHistoryContent> PIdslist = new ObservableCollection<MedicalHistoryContent>();
        ItemManager manager;
        public Search()
        {

            InitializeComponent();
            manager = ItemManager.DefaultManager;

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
        }


        private void ShowDashboard()
        {
            this.Navigation.PushAsync(new MainPage());
        }

        private void ShowAddPatientPage()
        {
            this.Navigation.PushAsync(new AddPatient());
        }
        public ObservableCollection<string> _names = new ObservableCollection<string>
        {
            "Matthew", "Annalise", "Daniel", "Conrad", "Jean", "Jonathan"
        };


        private async void MainSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            ObservableCollection<Patient_Table> patienttable = await manager.GetPatientIDAsync();

            string keyword = MainSearchBar.Text;
            foreach (var p in patienttable)
            {
                patient.Add(p.Patient_ID);
                patient.Add(p.Name);
                patient.Add(p.Surname);
            }

            for (int i = 0; i < patient.Count; i++)
            {
                PIdslist.Add(new MedicalHistoryContent
                {
                    description = patient[i].ToString()
                });
            }
            //MainContentView.Content = new ContentView
            //{
            //    Content = new ListView { ItemsSource = PIdslist, RowHeight = 40, Margin = 20 },
            //};

            //2 Method
            //search through the list (which will be altered later on to traverse the db
            if (keyword.Length >= 1)
            {
                IEnumerable<string> SearchResult = patient.Where(name => name.ToLower().Contains(keyword.ToLower()));
                ObservableCollection<values1> searchR = new ObservableCollection<values1>();
                foreach (Patient_Table p in patienttable)
                {
                    if (p.Name.ToLower().Contains(keyword.ToLower()))
                    {
                        searchR.Add(
                            new values1
                            {
                                name = p.Name,
                                surname = p.Surname,
                                id = p.Patient_ID
                            }
                            );
                    }
                }



                //get the result
                //MainListView.ItemsSource = SearchResult;
                MainListView.ItemsSource = searchR;

                MainListView.IsVisible = true;
            }
            else
            {
                MainListView.IsVisible = false;
            }
        }



        private async void MainListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {
            string check = e.Item.ToString();
               ListView l = (ListView)sender;
                values1 v = (values1)l.SelectedItem;
                
            var type = e.Item.GetType();
            ObservableCollection<Patient_Table> patienttable = await manager.GetPatientIDAsync();
            
            foreach (var p in patienttable)
            {
                //if(p.Patient_ID == check || p.Name == check || p.Surname == check)
                if(p.Patient_ID == v.id || p.Name == v.name || p.Surname == v.surname)
                {
                    PatientProfile patient = new PatientProfile(p.Patient_ID);
                    await Navigation.PushAsync(patient);
                    break;
                }
            }

            


        }



        private void MainSearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

    }
}
