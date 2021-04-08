using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FlightGearApp.windows
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow(FilesParser fp)
        {
            InitializeComponent();
            //this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "airplane.jpg")));
            TimeConroller.setTimeControllerValues(fp.getCSV());
            TimeConroller.PropertyChanged += delegate (object sender, PropertyChangedEventArgs e)
            {
                string property = e.PropertyName;
                string[] propertyValues = new string[2];
                propertyValues = property.Split('-');
                if (propertyValues[0] == "Time")
                {
                    FlightInfo.setTime(Int32.Parse(propertyValues[1]));
                    WheelController.setTime(Int32.Parse(propertyValues[1]));
                    GraphController.setTime(Int32.Parse(propertyValues[1]));

                }
                else if (propertyValues[0] == "ShouldStop")
                {
                    if (propertyValues[1] == "true")
                    {
                        FlightInfo.setShouldStop(true);
                        WheelController.setShouldStop(true);
                        GraphController.setShouldStop(true);
                    }
                    else
                    {
                        FlightInfo.setShouldStop(false);
                        WheelController.setShouldStop(false);
                        GraphController.setShouldStop(false);
                    }
                }
                else if (propertyValues[0] == "Pace")
                {
                    FlightInfo.setPace(Int32.Parse(propertyValues[1]));
                    WheelController.setPace(Int32.Parse(propertyValues[1]));
                    WheelController.setPace(Int32.Parse(propertyValues[1]));
                }
            };

            FlightInfo.setFlightValues(fp.getXML_CSVMap());
            FlightInfo.run();
            WheelController.setMapValues(fp.getXML_CSVMap());
            WheelController.run();
            GraphController.setMaps(fp.getXML_CSVMap(), fp.XMLlist);
            GraphController.run();

        }

        private void FlightInfo_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
