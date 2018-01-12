using System.Collections.Generic;
using Xamarin.Forms;

namespace App1.NotificationList
{
    public class Utilities
    {
        public static Grid BuildListElement(Notification n, List<string> patientsInvolved)
        {
            string dangerType = resolveDangerType(n.Alarmtypeid);

            Color c = resolveAlarmNotifColor(n.DangerCategoryID);

            string imageName = resolveImgName(n.DangerCategoryID);
            Image i = new Image { Source = ImageSource.FromFile("Assets/NotifIcons/" + imageName), BackgroundColor = c, VerticalOptions = LayoutOptions.Center, HorizontalOptions = LayoutOptions.Center };

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute) });
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Absolute) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            grid.Children.Add(new BoxView() { BackgroundColor = Color.White }, 0, 2);
            grid.Children.Add(new BoxView(), 0, 3);

            Grid gridRow1Outer = new Grid();
            gridRow1Outer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridRow1Outer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridRow1Outer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
            gridRow1Outer.Children.Add(new BoxView() { BackgroundColor = c }, 0, 0);
            gridRow1Outer.Children.Add(i, 0, 0);

            Grid gridRow1Column2 = new Grid();
            gridRow1Column2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridRow1Column2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridRow1Column2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

            Grid gridRow1Column2Column1 = new Grid() { RowSpacing = 0, ColumnSpacing = 0 };
            gridRow1Column2Column1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridRow1Column2Column1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridRow1Column2Column1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridRow1Column2Column1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridRow1Column2Column1.Children.Add(new Label() { Text = n.Name, FontSize = 28, FontAttributes = FontAttributes.Bold, TextColor = c }, 0, 0);
            gridRow1Column2Column1.Children.Add(new Label() { Text = n.Room, FontSize = 20, TextColor = c }, 0, 1);

            if(patientsInvolved.Count > 0)
            {
                if (patientsInvolved.Count == 1)
                {
                    gridRow1Column2Column1.Children.Add(new Label() { Text = patientsInvolved[0], FontSize = 20, TextColor = Color.FromHex("#c6c6c6") }, 0, 2);
                }
                else
                {
                    gridRow1Column2Column1.Children.Add(new Label() { Text = patientsInvolved[0] + " and " + (patientsInvolved.Count - 1) + " more.", FontSize = 20, TextColor = Color.FromHex("#c6c6c6") }, 0, 2);
                }
            }
            else
            {
                gridRow1Column2Column1.Children.Add(new Label() { Text = "No information about patients involved.", FontSize = 20, TextColor = Color.FromHex("#c6c6c6") }, 0, 2);
            }

            Grid gridRow1Column2Column2 = new Grid() { RowSpacing = 0, ColumnSpacing = 0 };
            gridRow1Column2Column2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridRow1Column2Column2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridRow1Column2Column2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridRow1Column2Column2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridRow1Column2Column2.Children.Add(new Label() { Text = n.TimeStamp, HorizontalTextAlignment = TextAlignment.End, TextColor = Color.White, FontSize = 20 }, 0, 0);
            gridRow1Column2Column2.Children.Add(new Label() { Text = dangerType, HorizontalTextAlignment = TextAlignment.End, TextColor = Color.White, FontSize = 20 }, 0, 1);

            Grid gridRow2Outer = new Grid() { RowSpacing = 3 };
            gridRow2Outer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gridRow2Outer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
            gridRow2Outer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridRow2Outer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
            gridRow2Outer.Children.Add(new Entry() { Text = "Write feedback...", TextColor = Color.FromHex("#c6c6c6"), FontSize = 15 }, 0, 0);
            gridRow2Outer.Children.Add(new Button() { Text = "Accept", BackgroundColor = Color.LightBlue }, 1, 0);
            gridRow2Outer.Children.Add(new Button() { Text = "Busy", BackgroundColor = Color.Crimson }, 2, 0);

            gridRow1Column2.Children.Add(gridRow1Column2Column1, 0, 0);
            gridRow1Column2.Children.Add(gridRow1Column2Column2, 1, 0);

            gridRow1Outer.Children.Add(gridRow1Column2, 1, 0);

            grid.Children.Add(gridRow1Outer, 0, 0);
            grid.Children.Add(gridRow2Outer, 0, 1);

            return grid;
        }

        public static Grid Trespassing(Notification n, string name)
        {
            if (Map.red != true)
            {
                string dangerType = resolveDangerType(n.Alarmtypeid);
                Color c = resolveAlarmNotifColor(n.DangerCategoryID);

                Grid grid = new Grid();
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(3, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Absolute) });
                grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(2, GridUnitType.Absolute) });
                grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                grid.Children.Add(new BoxView() { BackgroundColor = Color.White }, 0, 2);
                grid.Children.Add(new BoxView(), 0, 3);

                Grid gridRow1Outer = new Grid();
                gridRow1Outer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridRow1Outer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridRow1Outer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) });
                gridRow1Outer.Children.Add(new BoxView() { BackgroundColor = c }, 0, 0);
                //gridRow1Outer.Children.Add(i, 0, 0);

                Grid gridRow1Column2 = new Grid();
                gridRow1Column2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridRow1Column2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridRow1Column2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });

                Grid gridRow1Column2Column1 = new Grid() { RowSpacing = 0, ColumnSpacing = 0 };
                gridRow1Column2Column1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridRow1Column2Column1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridRow1Column2Column1.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridRow1Column2Column1.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridRow1Column2Column1.Children.Add(new Label() { Text = "Trespassing", FontSize = 28, FontAttributes = FontAttributes.Bold, TextColor = c }, 0, 0);
                gridRow1Column2Column1.Children.Add(new Label() { Text = n.Room, FontSize = 20, TextColor = c }, 0, 1);


                Grid gridRow1Column2Column2 = new Grid() { RowSpacing = 0, ColumnSpacing = 0 };
                gridRow1Column2Column2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridRow1Column2Column2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridRow1Column2Column2.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridRow1Column2Column2.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridRow1Column2Column2.Children.Add(new Label() { Text = n.TimeStamp, HorizontalTextAlignment = TextAlignment.End, TextColor = Color.White, FontSize = 20 }, 0, 0);
                gridRow1Column2Column2.Children.Add(new Label() { Text = "REALTIME ISSUE", HorizontalTextAlignment = TextAlignment.End, TextColor = Color.White, FontSize = 20 }, 0, 1);

                Grid gridRow2Outer = new Grid() { RowSpacing = 3 };
                gridRow2Outer.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
                gridRow2Outer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) });
                gridRow2Outer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridRow2Outer.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
                gridRow2Outer.Children.Add(new Entry() { Text = "Write feedback...", TextColor = Color.FromHex("#c6c6c6"), FontSize = 15 }, 0, 0);
                gridRow2Outer.Children.Add(new Button() { Text = "Accept", BackgroundColor = Color.LightBlue }, 1, 0);
                gridRow2Outer.Children.Add(new Button() { Text = "Busy", BackgroundColor = Color.Crimson }, 2, 0);

                gridRow1Column2Column1.Children.Add(new Label() { Text = Map.name, FontSize = 20, TextColor = Color.FromHex("#c6c6c6") }, 0, 2);

                gridRow1Column2.Children.Add(gridRow1Column2Column1, 0, 0);
                gridRow1Column2.Children.Add(gridRow1Column2Column2, 1, 0);

                gridRow1Outer.Children.Add(gridRow1Column2, 1, 0);

                grid.Children.Add(gridRow1Outer, 0, 0);
                grid.Children.Add(gridRow2Outer, 0, 1);
                return grid;
            }
            return null;
        }

        private static string resolveImgName(int dangerCategoryID)
        {
            string imgName = "";

            switch (dangerCategoryID)
            {
                case 1:
                    imgName = "falling-man--inverted.png"; //Falling
                    break;
                case 2:
                    imgName = "human-group-with-questions-and-doubts-inverted.png"; //Crowding
                    break;
                case 3:
                    imgName = "sleepwalker-inverted.png"; //Sundowning
                    break;
                case 4:
                    imgName = "opened-door-aperture-inverted.png"; //Open Doors
                    break;
                case 5:
                    imgName = "drop-silhouette-inverted.png"; //Liquid Spillage
                    break;
                case 6:
                    imgName = "light-bulb-inverted.png"; //Lack of Lighting
                    break;
                case 7:
                    imgName = "icon-inverted.png"; //Open Windows
                    break;
                default:
                    imgName = "image.png";
                    break;
            }

            return imgName;
        }

        public static string resolveDangerType(int dangerTypeID)
        {
            string message = "";

            switch (dangerTypeID)
            {
                case 1:
                    message = "REALTIME ISSUE";
                    break;
                case 2:
                    message = "PREDICTION";
                    break;
            }

            return message;
        }

        private static Color resolveAlarmNotifColor(int dangerCategoryID)
        {
            Color c = new Color();

            switch (dangerCategoryID)
            {
                case 1:
                case 3:
                case 5://Falling, Sundowning, Liquid Spillage
                    c = Color.FromHex("#fa7470");
                    break;
                case 2:
                case 4:
                case 6:
                case 7://Crowding, Open Doors, Lack of Lighting, Open Windows
                    c = Color.FromHex("#e4e500");
                    break;
            }

            return c;
        }
    }
}
