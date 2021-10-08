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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Newtonsoft;
using System.Text.Json;

namespace WpfApp1
{
  
    public partial class MainWindow : Window
    {

        private readonly string __path = $"C:\\Users\\Anton\\source\\repos\\WpfApp1\\spindleSpeedData.json";
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Event
        {
            public int id { get; set; }
            public int event_type_id { get; set; }
            public int workshop_id { get; set; }
            public int machine_id { get; set; }
            public object time1 { get; set; }
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


        public MainWindow()
        {
            InitializeComponent();
        }

        public static List<Event> Convert(string json)
        {
            return JsonSerializer.Deserialize<List<Event>>(json);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            this.Close();
        } 

        private void RichTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //https://www.youtube.com/watch?v=SholKTNGdHk
                string jsonFromFile;
                using (var reader = new Reader(__path))
                {
                    jsonFromFile = reader.ReadToEnd();
                }
            }
        }
    }
}
