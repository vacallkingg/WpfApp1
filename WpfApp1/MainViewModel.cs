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
    public class MainViewModel
    {
        private readonly string __path = $"C:\\Users\\Anton\\source\\repos\\WpfApp1\\spindleSpeedData.json";
        public PlotModel MyModel { get; private set; }
        public  MainViewModel()
        {

            try
            {
                string jsonFromFile;
                using (StreamReader reader = new StreamReader(__path))
                {
                    jsonFromFile = reader.ReadToEnd();
                }
                Root SpindleFromJson = JsonSerializer.Deserialize<Root>(jsonFromFile);


                var line1 = new LineSeries()
                {
                    Title = $"Series 1",
                    Color = OxyColors.Blue,
                    StrokeThickness = 1,
                };

                var count = 0;
                //var width = canvas.ActualWidth / (Convert.ToDouble(SpindleFromJson.payload.events.Count) * 0.1);
                //double stepWidth = 0;
                //double topHeight = canvas.ActualHeight;
                //double downHeight = canvas.ActualHeight;
                string temp = "";
                foreach (var item in SpindleFromJson.payload.events)
                {
                    
                    string fromJsonID = Convert.ToString(item.id);
                    string fromJsonEventTypeID = Convert.ToString(item.event_type_id);
                    string fromJsonWorkshopID = Convert.ToString(item.workshop_id);
                    string fromJsonMachineID = Convert.ToString(item.machine_id);
                    string fromJsonTime1 = Convert.ToString(item.time1);
                    DateTime dateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
                    dateTime = dateTime.AddMilliseconds(Convert.ToDouble(fromJsonTime1));
                    string fromJsonFloat = Convert.ToString(item.@float);
                    temp += "\n" + "Идентификатор события: " + fromJsonID + "\n" +
                            "ID скорости шпинделя со станка: " + fromJsonEventTypeID + "\n" +
                            "Идентификатор цеха: " + fromJsonWorkshopID + "\n" +
                            "Идентификатор станка: " + fromJsonMachineID + "\n" +
                            "Время выхода: " + dateTime.ToString() + "\n" +
                            "Скорость шпинделя во время выхода: " + fromJsonFloat + "\n";
                  // TextBoxReadJson.AppendText(temp);

                    line1.Points.Add(new DataPoint(item.time1, item.@float));

                    count++;
                    if (count >= 10) break;

                    /* var height = -canvas.ActualHeight / Convert.ToDouble(fromJsonFloat) * (-canvas.ActualHeight / 100) + 100;
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
                     canvas.Children.Add(line); */
                }
                MyModel.Series.Add(line1);
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
