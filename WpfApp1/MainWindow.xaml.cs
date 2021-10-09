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
                using (var reader = new StreamReader(__path)) //Читает путь который задал заранее
                {
                    jsonFromFile = reader.ReadToEnd();
                }
                Payload SpindleFromJson = JsonSerializer.Deserialize<Payload>(jsonFromFile); //Десереализация
                
                foreach(var item in SpindleFromJson.events) //Вот здесь залупа, какой-то костыль пытаюсь сделать
                {
                    string toJson = Convert.ToString(item.id);//Конвертирую например id в строку 
                    TextBoxReadJson.Text = toJson; //Пытаюсь вывести в текстбокс
                }

                
                //TextBoxReadJson.Text = jsonFromFile;

            }
            catch { }
        }

        private void TextBoxReadJson_Scroll(object sender, System.Windows.Controls.Primitives.ScrollEventArgs e)
        {

        }
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
    }
}
