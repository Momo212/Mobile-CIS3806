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
    public partial class Map : ContentPage
    {

        ItemManager itemManager;
        Image mapImage;

        public Map()
        {
            InitializeComponent();

            itemManager = ItemManager.DefaultManager;

            //list = new List<Coordinate_Table>();
            //GetCoordinates();

            // Change text
            //MessagingCenter.Subscribe<CusomMap, string>(this, "LabelText", (sender, e) => { label1.Text = e; });

            // Set map image
            mapImage = new Image();
            mapImage.Source = ImageSource.FromFile("plan.png");
            mapImage.Aspect = Aspect.AspectFill;
            mapArea.Children.Add(mapImage);

            // Add markers
            //PlaceMarkers();
            GetPatients();

        }

        public async void GetPatients()
        {
            Label label1 = new Label();
            label1.Text = "Patient List: \n";
            label1.FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label));
            patientsArea.Children.Add(label1);
            
            var patients = await itemManager.GetPatientItemsAsync();
            foreach (Patient_Table patient in patients)
            {
                Label label = new Label();
                label.Text += patient.Patient_ID + " " + patient.Name + " " + patient.Surname + "\n";
                TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += (sender, e) =>
                {
                    PlaceMarkers(patient.Patient_ID);

                    mapArea.Children.Clear();
                    mapArea.Children.Add(mapImage);
                };

                label.GestureRecognizers.Add(tap);

                patientsArea.Children.Add(label);
            }
        }

        public async void PlaceMarkers(String patientIdIn)
        {
            var coordinates = await itemManager.GetCoordinateItemsAsync(patientIdIn);

            foreach (Coordinate_Table coordinate in coordinates)
            {


                Frame frame = new Frame
                {
                    CornerRadius = 5,
                    HeightRequest = 10,
                    WidthRequest = 10,
                    BackgroundColor = Color.Blue,
                    OutlineColor = Color.Black,
                    Margin = 0,
                    Padding = 0,
                    TranslationX = 1000 * coordinate.Coord_x,
                    TranslationY = 470 * coordinate.Coord_y
                };

                if (coordinate.Coord_x < 0.13 || coordinate.Coord_x > 0.79 || coordinate.Coord_y < 0.35 || coordinate.Coord_y > 0.9)
                {
                    frame.BackgroundColor = Color.Red;
                }

                /*TapGestureRecognizer tap = new TapGestureRecognizer();
                tap.Tapped += (sender, e) =>
                {
                    //info.Text = "id: " + coordinate.Id + "\nx: " + coordinate.Coord_x + "\ny: " + coordinate.Coord_y + "\npatientid: " + coordinate.PatientID + "\ndateTime: " + coordinate.Timestamp; ;
                };

                frame.GestureRecognizers.Add(tap);*/

                await Task.Delay(1000);

                mapArea.Children.Add(frame);

            }
        }

        /*public async void GetCoordinates()
        {
            /*Coordinate_Table coordinate = new Coordinate_Table();
            coordinate.Id = "8";
            coordinate.Coord_x = 0.25F;
            coordinate.Coord_y = 0.75F;
            coordinate.PatientID = "2993m";
            coordinate.Timestamp = DateTime.Now;
            var item = await itemManager.GetFearItemsAsync("2993m");
            await itemManager.SaveTaskAsyncCoordinate(coordinate);

            var item2 = await itemManager.GetCoordinateItemsAsync();
        }*/
    }
}