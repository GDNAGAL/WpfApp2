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
        private readonly HttpClient _httpClient;
        public Routes()
        {
            InitializeComponent();
            _httpClient = new HttpClient();
            ShowMap();
        }
        private async void ShowMap()
        {
            try
            {
                string bingMapsKey = "FqPVzbC12lz0XsfWFJ7G~tEGUro4BW1iB5iiyvgDqgA~AhRJ26UqNyj6WWKjAvmJD42KAZxc03K_DMwN-_nx8N5V1ZHN-wvqrxujRzz5ZU5z";
                string mapArea = "47.5151,-122.1774,47.6723,-121.2853"; // Latitude, Longitude, Zoom Level
                string imageUrl = $"https://dev.virtualearth.net/REST/v1/Imagery/Map/Road?mapArea={mapArea}&key={bingMapsKey}";
                HttpResponseMessage response = await _httpClient.GetAsync(imageUrl);

                if (response.IsSuccessStatusCode)
                {
                    byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.StreamSource = new System.IO.MemoryStream(imageBytes);
                    bitmap.EndInit();

                    mapImage.Source = bitmap;
                }
                else
                {
                    MessageBox.Show("Error downloading map image: " + response.ReasonPhrase);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading map: " + ex.Message);
            }
        }
    }
    
}
