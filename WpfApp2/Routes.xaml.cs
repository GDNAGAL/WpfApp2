using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
using Microsoft.Maps.MapControl.WPF;
using System.Net.Http;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Routes.xaml
    /// </summary>
    public partial class Routes : Page
    {
        public Routes()
        {
            InitializeComponent();
            // Create dummy data
            List<YourDataClass> dummyData = GenerateDummyData();

            // Set dummy data as the ItemsSource for the DataGrid
            myDataGrid.ItemsSource = dummyData;
        }

        public class YourDataClass
        {
            public string Name { get; set; }
            public int Age { get; set; }
            public string Email { get; set; }
        }
        private List<YourDataClass> GenerateDummyData()
        {
            List<YourDataClass> data = new List<YourDataClass>();

            // Generate some dummy records
            data.Add(new YourDataClass { Name = "John Doe", Age = 30, Email = "john@example.com" });
            data.Add(new YourDataClass { Name = "Jane Smith", Age = 25, Email = "jane@example.com" });
            data.Add(new YourDataClass { Name = "Alice Johnson", Age = 35, Email = "alice@example.com" });
            // Add more dummy data as needed...

            return data;
        }
        private void AddRoute_Click(object sender, RoutedEventArgs e)
        {
            AddRoute addRoute = new AddRoute();
            addRoute.ShowDialog();
        }
    }
    
}
