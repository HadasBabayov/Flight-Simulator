﻿using System;
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

namespace TimeControlSlider.controls
{
    /// <summary>
    /// Interaction logic for TimeController.xaml
    /// </summary>
    public partial class TimeController : UserControl, INotifyPropertyChanged
    {
        TimeControllerViewModel timeControllerVM;

        public event PropertyChangedEventHandler PropertyChanged;

        public TimeController()
        {
            InitializeComponent();
            timeControllerVM = new TimeControllerViewModel(new TimeControllerModel());
            timeControllerVM.StartApp();
            Pause.Visibility = Visibility.Hidden;
            Stop.Visibility = Visibility.Hidden;
            DataContext = timeControllerVM;
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            timeControllerVM.Run();
            Pause.Visibility = Visibility.Visible;
            Stop.Visibility = Visibility.Visible;
            Play.Visibility = Visibility.Hidden;
            Stop.IsEnabled = false;
        }

        private void Pause_Click(object sender, RoutedEventArgs e)
        {
            timeControllerVM.PauseFlight();
            Pause.Visibility = Visibility.Hidden;
            Play.Visibility = Visibility.Visible;
            Play.Content = "Resume";
            Stop.IsEnabled = true;
        }

        private void Stop_Click(object sender, RoutedEventArgs e)
        {
            timeControllerVM.StartFlightOver();
            timeControllerVM.PauseFlight();
            Pause.Visibility = Visibility.Hidden;
            Stop.Visibility = Visibility.Hidden;
            Play.Content = "Play";
        }

        public void setTimeControllerValues(Dictionary<int, string> values)
        {
            timeControllerVM.setMap(values);
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            timeControllerVM.VM_Time = (int)TimeSlider.Value;
            NotifyPropertyChanged(timeControllerVM.VM_Time.ToString());
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (timeControllerVM != null)
            {
                timeControllerVM.VM_Pace = 100 / (PlaySpeed.SelectedIndex + 1);
            }
        }
    }
}
