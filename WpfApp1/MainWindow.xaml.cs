using System;
using System.IO;
using System.Collections.Generic;
using System.Windows;
using System.Text.Json;
using System.Windows.Shapes;
using System.Windows.Media;

namespace WpfApp1
{
  
    public partial class MainWindow : Window
    {

        //private readonly string __path = $"C:\\Users\\Anton\\source\\repos\\WpfApp1\\spindleSpeedData.json";
        private readonly string __path = $"C:\\Users\\vacal\\source\\repos\\WpfApp1\\spindleSpeedData.json";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string jsonFromFile;
                using (var reader = new StreamReader(__path)) 
                {
                    jsonFromFile = reader.ReadToEnd();
                }
                Root SpindleFromJson = JsonSerializer.Deserialize<Root>(jsonFromFile);

                var count = 0;
                var width = canvas.ActualWidth / (Convert.ToDouble(SpindleFromJson.payload.events.Count) * 0.1);
                double stepWidth = 0;
                double topHeight = canvas.ActualHeight;
                double downHeight = canvas.ActualHeight;

                foreach (var item in SpindleFromJson.payload.events) 
                {
                    string temp = "";
                    string fromJsonID = Convert.ToString(item.id);
                    string fromJsonEventTypeID = Convert.ToString(item.event_type_id);
                    string fromJsonWorkshopID = Convert.ToString(item.workshop_id);
                    string fromJsonMachineID = Convert.ToString(item.machine_id);
                    string fromJsonTime1 = Convert.ToString(item.time1);
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTime = dateTime.AddMilliseconds(Convert.ToDouble(fromJsonTime1));
                    string fromJsonFloat = Convert.ToString(item.@float);
                    temp += "\n" + "Идентификатор события: " + fromJsonID + "\n" +
                            "Скорость шпинделя со станка: " + fromJsonEventTypeID + "\n" +
                            "Идентификатор цеха: " + fromJsonWorkshopID + "\n" +
                            "Идентификатор станка: " + fromJsonMachineID + "\n" +
                            "Время выхода: " + dateTime.ToString() + "\n" +
                            "Скорость шпинделя во время выхода: " + fromJsonFloat + "\n";
                    // TextBoxReadJson.Text += temp; 
                    TextBoxReadJson.AppendText(temp);
                    count++;
                    //if (count >= 10) break;

                    var height = -canvas.ActualHeight / Convert.ToDouble(fromJsonFloat) * (-canvas.ActualHeight / 100) + 100;
                    topHeight = downHeight;
                    stepWidth = stepWidth + width;
                    if (height < downHeight)
                    {
                        downHeight = height;
                    }
                    else
                    {
                        downHeight += height;
                    }

                    Line line = new Line();
                    line.X1 = stepWidth - width;
                    line.Y1 = topHeight;
                    line.X2 = stepWidth;
                    line.Y2 = downHeight;

                    line.StrokeThickness = 1.5;
                    line.Stroke = Brushes.Black;
                    canvas.Children.Add(line);
                }

                //Debug.WriteLine(SpindleFromJson);
                //TextBoxReadJson.Text = SpindleFromJson.payload.events[].id.ToString();

            }
            catch { }
            
        }
        public class Event
        {
            public string command { get; set; }
            public int id { get; set; }
            public int event_type_id { get; set; }
            public int workshop_id { get; set; }
            public int machine_id { get; set; }
            public long time1 { get; set; }
            public object time2 { get; set; }
            public object data { get; set; }
            public double @float { get; set; }
            public object enum_value { get; set; }
            public object uint32 { get; set; }
        }

        public class Payload
        {
            public List<Event> events { get; set; }
            public string _rk { get; set; }
        }

        public class Root
        {
            public string command { get; set; }
            public Payload payload { get; set; }
        }

    }
}
