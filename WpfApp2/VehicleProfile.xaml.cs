using BingMapsRESTToolkit;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

    public partial class VehicleProfile : Window
    {
        public int Vid;
        public VehicleProfile(int VehicleId)
        {
            this.Vid = VehicleId;   
            InitializeComponent();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            Vehicle dir=new Vehicle();
            try
            {
                HttpClient client= new HttpClient();
                var respons = await client.GetAsync("https://localhost:7082/api/Vehicle/FindVehicles?VehicleId=" + Vid);
                respons.EnsureSuccessStatusCode();                                
                var jsonString = await respons.Content.ReadAsStringAsync();
                dir = JsonConvert.DeserializeObject<Vehicle>(jsonString);               
                
                
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (dir!=null)
            {
                DriverName.Content = dir.VehicleRegNo;
                Age.Content = dir.VehicleType;
                ExpYear.Content = dir.Capacity;
                LicenseClasss.Content= dir.LastMaintainance;

            }
        }

    }
}
