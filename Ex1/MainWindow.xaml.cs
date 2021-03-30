using System;
using System.Collections.Generic;
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


namespace Ex1
{
   
    public partial class MainWindow : Window
    {
        RunFlight runFlight;
        public MainWindow()
        {
            runFlight = new RunFlight();
            InitializeComponent();
            this.Background = new ImageBrush(new BitmapImage(new Uri(BaseUriHelper.GetBaseUri(this), "Images/airplane.jpg")));
        }

        private void Fly_Button_Click(object sender, RoutedEventArgs e)
        {
            runFlight.Run();
        }

        private void Stop_Flight_Click(object sender, RoutedEventArgs e)
        {
            runFlight.StopFlight();
        }

        private void Close_Connection_Click(object sender, RoutedEventArgs e)
        {
            runFlight.CloseConnection();
            this.Close();
        }

        private void Start_Flight_Over_Click(object sender, RoutedEventArgs e)
        {
            runFlight.StartFlightOver();
        }
    }
}
