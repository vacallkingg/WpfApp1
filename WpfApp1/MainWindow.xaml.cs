using System;
using System.IO;
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
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Windows.Automation;
using System.Diagnostics;

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


                foreach(var item in SpindleFromJson.payload.events) 
                {
                    string temp = "";
                    string fromJsonID = Convert.ToString(item.id);
                    string fromJsonEventTypeID = Convert.ToString(item.event_type_id);
                    string fromJsonWorkshopID = Convert.ToString(item.workshop_id);
                    string fromJsonMachineID = Convert.ToString(item.machine_id);
                    string fromJsonTime1 = Convert.ToString(item.time1);
                    string fromJsonFloat = Convert.ToString(item.@float);
                    temp += "\n" + "Идентификатор события: " + fromJsonID + "\n" +
                            "Скорость шпинделя со станка: " + fromJsonEventTypeID + "\n" +
                            "Идентификатор цеха: " + fromJsonWorkshopID + "\n" +
                            "Идентификатор станка: " + fromJsonMachineID + "\n" +
                            "Время выхода: " + fromJsonTime1 + "\n" +
                            "Скорость шпинделя во время выхода: " + fromJsonFloat + "\n" 
                        ;
                    TextBoxReadJson.Text += temp; 
                }

                //Debug.WriteLine(SpindleFromJson);
                //TextBoxReadJson.Text = SpindleFromJson.payload.events[].id.ToString();

            }
            catch { }
            
        }

        private void TextBoxReadJson_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

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
