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
    public partial class MainViewModel : Window
    {
        public MainViewModel()
        {
            MyModel = new PlotModel { Title = "" };
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Bottom, Minimum = 0, Maximum = 12000 });
            MyModel.Axes.Add(new LinearAxis { Position = AxisPosition.Left, Minimum = 0, Maximum = 500 });
        }
        public PlotModel MyModel { get; private set; }
        public IList<DataPoint> Points { get; private set; }
        
      

    }
}
