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
using Newtonsoft.Json;
using System.Collections.ObjectModel;
using WpfApp2.Model;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Routes.xaml
    /// </summary>
    public partial class Routes : Page
    {
        ObservableCollection<RoutesDetails> members = new ObservableCollection<RoutesDetails>();

        public Routes()
        {
            InitializeComponent();
            // Create dummy data
            

            // Set dummy data as the ItemsSource for the DataGrid
            //myDataGrid.ItemsSource = dummyData;
        }       
       
        private void AddRoute_Click(object sender, RoutedEventArgs e)
        {
            AddRoute addRoute = new AddRoute();
            addRoute.ShowDialog();
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //var converter = new BrushConverter();
            //string[] color = { "#1098AD", "#1E88E5", "#FF8F00", "#FF5252", "#0CA678", "#6741D9", "#FF6D00", "#FF5252", "#1E88E5", "#0CA678" };
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    var respons = await client.GetAsync("https://localhost:7082/api/RouteDetailsManage/GetAllRouteDetails");
                    respons.EnsureSuccessStatusCode();
                    if (respons.IsSuccessStatusCode)
                    {
                        var jsonString = await respons.Content.ReadAsStringAsync();
                        members = JsonConvert.DeserializeObject<ObservableCollection<RoutesDetails>>(jsonString);
                        
                        
                        myDataGrid.ItemsSource = members;

                    }
                    else
                    {
                        MessageBox.Show($"Server Error Code{respons.StatusCode}");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
    
}
