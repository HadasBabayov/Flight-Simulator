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
using System.Windows.Shapes;

namespace TimeControlSlider.windows
{
    /// <summary>
    /// Interaction logic for AppWindow.xaml
    /// </summary>
    public partial class AppWindow : Window
    {
        public AppWindow(FilesParser fp)
        {
            
            InitializeComponent();
            TimeController.setTimeControllerValues(fp.getCSV());
           // TimeController.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
        }
    }
}
