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
        public AppWindow(FilesParser fpForLearnCsv, FilesParser fpForAnomalyCsv)
        {
            InitializeComponent();
            TimeConroller.setTimeControllerValues(fpForAnomalyCsv.getCSV());
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

            FlightInfo.setFlightValues(fpForAnomalyCsv.getXML_CSVMap());
            FlightInfo.run();
            WheelController.setMapValues(fpForAnomalyCsv.getXML_CSVMap());
            WheelController.run();
            GraphController.setMaps(fpForLearnCsv.getXML_CSVMap(), fpForLearnCsv.XMLlist, fpForAnomalyCsv.getXML_CSVMap());
            GraphController.run();

        }

        private void FlightInfo_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
