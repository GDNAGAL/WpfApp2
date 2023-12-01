using GoogleMapsApi.Entities.Directions.Response;
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
    /// <summary>
    /// Interaction logic for AddVehicle.xaml
    /// </summary>
    public partial class AddVehicle : Window
    {
        public AddVehicle()
        {
            InitializeComponent();
        }
        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Grid_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                this.WindowState = WindowState.Normal;
            }
        }
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            VehicleDetails details = new VehicleDetails();
            details.VehicleNumber = VehicleNumber.TextValue;
            details.VehicleType = Convert.ToInt32(VehicleType.TextValue);
            details.Capacity = Convert.ToInt32(Capacity.TextValue);
            details.ChassisNo = ChassisNumber.TextValue;
            details.RegistrationNo = RegistrationNumber.TextValue;
            details.Model = Model.TextValue;
            details.Color = Color.TextValue;


            var dataAsString = JsonSerializer.Serialize(details);

            //using (var client = new HttpClient())
            //{
            //    var endpoint = new Uri("https://localhost:7082/api/DriverDetails/AddDriver");
            //    var dataAsString = JsonSerializer.Serialize(details);
            //    var content = new StringContent(dataAsString);
            //    //content.Headers.ContentType = contentType;
            //    client.PostAsync(endpoint, content);

            //}
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7082/api/Vehicle/AddVehile");
            request.Headers.Add("accept", "text/plain");
            var content = new StringContent(dataAsString, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            Console.WriteLine(response.Content);
            this.Hide();
            MessageBox.Show("Vehicle Added Successfully !!!", "Success ", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
