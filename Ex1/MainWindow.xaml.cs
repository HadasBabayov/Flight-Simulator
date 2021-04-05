using System;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace Ex1
{
    public partial class MainWindow : Window
    {
        RunFlight runFlight;
        /*
         * IFgClient client;
         * IFilesParser parser;
         * 
         */


        public MainWindow()
        {
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
