using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using WpfApp2.Model;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Vehicles.xaml
    /// </summary>
    public partial class Vehicles : Page
    {
        ObservableCollection<VehicleDetails> members = new ObservableCollection<VehicleDetails>();

        public Vehicles()
        {
            InitializeComponent();
        }

        private void addvehicle_Click(object sender, RoutedEventArgs e)
        {
            AddVehicle add_vehicle = new AddVehicle();
            add_vehicle.ShowDialog();
        }
        private async void Data_Delete(object sender, RoutedEventArgs e)
        {
            string VehileId = (membersDataGrid.SelectedItem as VehicleDetails).VehicleID;
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://localhost:7082/api/Vehicle/DeleteVehicle?VehicleId={VehileId}");
            request.Headers.Add("accept", "text/plain");
            var response = await client.SendAsync(request);
            try
            {

            response.EnsureSuccessStatusCode();
            }
            catch (Exception ex) { 
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);  
            }
            if (response.IsSuccessStatusCode)
            {

                MessageBox.Show("Vehile Deleted Successfully", "Data Deleted", MessageBoxButton.OK);

            }
            else
            {
                MessageBox.Show("Vefile Deleted", "Data Deleted", MessageBoxButton.OK, MessageBoxImage.Hand);

            }


        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //var converter = new BrushConverter();
            //string[] color = { "#1098AD", "#1E88E5", "#FF8F00", "#FF5252", "#0CA678", "#6741D9", "#FF6D00", "#FF5252", "#1E88E5", "#0CA678" };
            using (HttpClient client = new HttpClient())
            {
                var respons = await client.GetAsync("https://localhost:7082/api/Vehicle/GetAllVehicleDetails");
                respons.EnsureSuccessStatusCode();
                if (respons.IsSuccessStatusCode)
                {
                    var jsonString = await respons.Content.ReadAsStringAsync();
                    members = JsonConvert.DeserializeObject<ObservableCollection<VehicleDetails>>(jsonString);
                    int j = 1;
                    foreach (var item in members)
                    {

                        //item.BgColor = (Brush)converter.ConvertFromInvariantString(color[i]);
                        item.Number = (j).ToString();
                        //item.Character = item.FullName[0].ToString().ToUpper();

                        j++;
                    }
                    membersDataGrid.ItemsSource = members;

                }
                else
                {
                    MessageBox.Show($"Server Error Code{respons.StatusCode}");
                }
            }
        }
    }
}
