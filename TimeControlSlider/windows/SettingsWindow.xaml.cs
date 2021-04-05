﻿using System;
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

namespace TimeControlSlider.windows
{
    /// <summary>
    /// Interaction logic for SettingsWindow.xaml
    /// </summary>
    public partial class SettingsWindow : Window
    {
        private FilesLoaderViewModel flViewModel;

        public SettingsWindow()
        {
            InitializeComponent();
            flViewModel = new FilesLoaderViewModel(new FilesLoaderModel());
            DataContext = flViewModel;
        }

        private void loadCSV_button(object sender, RoutedEventArgs e)
        {

            flViewModel.initializeParser(xmlBox.Text, csvBox.Text);

            string s1 = flViewModel.parseXML();
            string s2 = flViewModel.MatchXMLToCSV();
            if (s1 == "" && s2 == "")
            {
                windows.AppWindow appWindow = new AppWindow(flViewModel.getFilesParser());
                this.Close();
                appWindow.Show();
            }
        }        

        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void csvBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
