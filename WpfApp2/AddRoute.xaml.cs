using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
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
using System.Text.Json;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for AddRoute.xaml
    /// </summary>
    public partial class AddRoute : Window
    {
        public AddRoute()
        {
            InitializeComponent();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void Add_Route(object sender, RoutedEventArgs e)
        {
            RoutesDetails route = new RoutesDetails();
            route.StartPoint = StartPoint.TextValue;
            route.EndPoint = EndPoint.TextValue;
            route.MidCity1 = MidCity1.TextValue;
            route.MidCity2 = MidCity2.TextValue;
            route.MidCity3 = MidCity3.TextValue;
            var dataAsString = JsonSerializer.Serialize(route);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7082/api/RouteDetailsManage/AddNewRoute");
            request.Headers.Add("accept", "text/plain");
            var content = new StringContent(dataAsString, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            Console.WriteLine(response.Content);
            this.Hide();
            MessageBox.Show("Route Added Successfully !!!", "Success ", MessageBoxButton.OK, MessageBoxImage.Information);
        }


    }
}
