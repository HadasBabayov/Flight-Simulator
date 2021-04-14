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
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Reflection;
using FlightGearApp.src;
using OxyPlot;

namespace FlightGearApp.windows
{
    /// <summary>
    /// Interaction logic for LoadDllAlgorithm.xaml
    /// </summary>
    public partial class LoadDllAlgorithm : Window
    {
        private string csvPath;
        private string DllPath;
        private string dllPath;
        private volatile Dictionary<string, List<float>> XML_CSVMap;
        private List<string> keysList;
        private List<AnomalyReports> anomalyReports;
        private List<string> corrFeatures;
        private string selectedItem;
        private PlotModel graphToDraw;
        private object instance;
        public LoadDllAlgorithm()
        {
            InitializeComponent();
            loadDllErrorMsg.Visibility = Visibility.Hidden;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        private void loadDllButton_Click(object sender, RoutedEventArgs e)
        {
            string dllPath = DllBox.Text;
            try
            {
                Assembly assembly2 = Assembly.LoadFile(dllPath);
                Type type2 = assembly2.GetType("AnomalyReportAlgorithm.AnomalyReportAlgorithm");
                var obj2 = Activator.CreateInstance(type2);
                loadDllErrorMsg.Visibility = Visibility.Hidden;
                DllPath = DllBox.Text;
                if (csvPath != "")
                {
                    this.Close();
                    NotifyPropertyChanged(DllPath);
                }
            }
            catch
            {
                loadDllErrorMsg.Visibility = Visibility.Visible;
            }
        }
    }
}
