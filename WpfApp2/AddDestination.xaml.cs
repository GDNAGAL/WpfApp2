using BingMapsRESTToolkit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp2.Model;

namespace WpfApp2
{

    public partial class AddDestination : Window
    {
        public AddDestination()
        {
            InitializeComponent();
        }

        private async void Add_Destination(object sender, RoutedEventArgs e)
        {
            DestinationDetails destination = new DestinationDetails();
            destination.DestinationName = DestinationName.TextValue;
            var dataAsString = JsonSerializer.Serialize(destination);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7082/api/DestinationManage/AddNewDestination");
            request.Headers.Add("accept", "text/plain");
            var content = new StringContent(dataAsString, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            Console.WriteLine(response.Content);
            this.Close();
            MessageBox.Show("Destinastion Added Successfully !!!", "Success ", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
