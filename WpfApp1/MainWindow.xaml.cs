using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;

namespace WpfApp1
{
    public partial class MainWindow : Window
    {

        //https://qna.habr.com/q/1060454#answers


        public MainWindow()
        {
            InitializeComponent();

            DataContext = new MainViewModel();
        }

       
        private void ButtonClear(object sender, RoutedEventArgs e)
        {
            TextBoxReadJson.Clear();
            //canvas.Children.Clear();
        }
        private void ButtonOpenJson(object sender, RoutedEventArgs e)
        {

        }
        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            TextBoxReadJson.Visibility = Visibility.Hidden;
            
        }
        private void CheckForText_Unchecked(object sender, RoutedEventArgs e)
        {
            TextBoxReadJson.Visibility = Visibility.Visible;
        }
    }
}
