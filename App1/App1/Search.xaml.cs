using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.DatabaseStuff;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Search : ContentPage
    {
        public Search()
        {

            InitializeComponent();

            MainListView.ItemsSource = _names;

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
        private readonly List<string> _names = new List<string>
        {
            "Matthew", "Annalise", "Daniel", "Conrad", "Jean", "Jonathan"
        };
        private void MainSearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            //get the keyword
            string keyword = MainSearchBar.Text;


            //selects the name containing the keyword

            //1 Method
            /*IEnumerable<string> SearchResult = from name
                                               in _names
                                               where name.Contains(keyword)
                                               select name;*/

            //2 Method
            //search through the list (which will be altered later on to traverse the db
            IEnumerable<string> SearchResult = _names.Where(name => name.ToLower().Contains(keyword.ToLower()));
            
            //get the result
            MainListView.ItemsSource = SearchResult;
        }
    }
}