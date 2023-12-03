using GoogleMapsApi.Entities.PlacesDetails.Response;
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
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Page
    {
        public List<Person> Data { get; set; }
        public Dashboard()
        {
            InitializeComponent();

            List<YourDataClass> dummyData = GenerateDummyData();

            // Set dummy data as the ItemsSource for the DataGrid
            myDataGrid.ItemsSource = dummyData;

            Data = new List<Person>()
            {
                new Person { Name = "Total Work Order", Height = 444 },
                new Person { Name = "Pending Work Order", Height = 107 },
                new Person { Name = "Vehicle", Height = 250 },
                new Person { Name = "Drivers", Height = 320 }
            };
            DataContext = this; 
        }

        public class Person
        {
            public string Name { get; set; }

            public double Height { get; set; }
        }

        public class YourDataClass
        {
            public string TrackingId { get; set; }  
            public string Category { get; set; }
            public string Destination { get; set; }
            public string Date { get; set; }
            public string Status { get; set; }
        }
        private List<YourDataClass> GenerateDummyData()
        {
            List<YourDataClass> data = new List<YourDataClass>();

            // Generate some dummy records
            data.Add(new YourDataClass { TrackingId = "#23658", Category = "Electronics", Destination = "Mumbai, India", Date ="12/11/2023", Status="Processing" });
            data.Add(new YourDataClass { TrackingId = "#23658", Category = "Petrol", Destination = "Delhi, India", Date ="10/11/2023", Status="Completed" });
            data.Add(new YourDataClass { TrackingId = "#23658", Category = "Febrics", Destination = "Gujrat, India", Date ="07/11/2023", Status="Completed" });
           
            // Add more dummy data as needed...

            return data;
        }
    }
}
