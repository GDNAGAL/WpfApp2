using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
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
using System.Windows.Shapes;
using WpfApp2.Model;
using WpfApp2.UserControls;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for AddWorkOrder.xaml
    /// </summary>
    public partial class AddWorkOrder : Window
    {
        ObservableCollection<DestinationDetails> members = new ObservableCollection<DestinationDetails>();

        public AddWorkOrder()
        {
            InitializeComponent();
            LoadData();

            // Disable dates prior to today
            WorkOrderDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));

            // Enable the next 30 days including today
            DateTime endDate = DateTime.Today.AddDays(29);
            WorkOrderDatePicker.DisplayDateEnd = endDate;
        }

        private void LoadData()
        {
            // Create a DataTable with static data
            DataTable dataTable = new DataTable("SampleData");
            dataTable.Columns.Add("RouteID", typeof(int));
            dataTable.Columns.Add("Route Name", typeof(string));
            dataTable.Columns.Add("Description", typeof(string));

            // Add some rows
            dataTable.Rows.Add(2121211, "Bikaner-Delhi", "via - Panjab,Haryana");
            dataTable.Rows.Add(5411552, "Delhi-Karnatka", "via - Rajasthan,Gujrat");
            dataTable.Rows.Add(1541663, "Rajasthan-Goa", "via - Gujrat");
            dataTable.Rows.Add(2121211, "Bikaner-Delhi", "via - Panjab,Haryana");
            dataTable.Rows.Add(5411552, "Delhi-Karnatka", "via - Rajasthan,Gujrat");
            dataTable.Rows.Add(1541663, "Rajasthan-Goa", "via - Gujrat");

            // Set the DataTable as the DataGrid's ItemsSource
            dummydataGrid.ItemsSource = dataTable.DefaultView;
        }
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            
            using (HttpClient client = new HttpClient())
            {
                
                var respons = await client.GetAsync("https://localhost:7082/api/DestinationManage/GetAllDestinationDetails");
                try{
                    respons.EnsureSuccessStatusCode(); }
                catch(Exception ex)
                {
                    MessageBox.Show($"{ex.Message}","Error",MessageBoxButton.OK,MessageBoxImage.Error);
                }

                if (respons.IsSuccessStatusCode)
                {
                    var jsonString = await respons.Content.ReadAsStringAsync();
                    members = JsonConvert.DeserializeObject<ObservableCollection<DestinationDetails>>(jsonString);
                    int j = 1;
                    PickupPoint.Items.Clear();
                    DropPoint.Items.Clear();
                    foreach (var item in members)
                    {
                        ComboBoxItem comboBoxItem = new();
                        comboBoxItem.Content=item.DestinationName;
                        comboBoxItem.Tag = item.DestinationID;
                        PickupPoint.Items.Add(comboBoxItem);
                        comboBoxItem = new();
                        comboBoxItem.Content = item.DestinationName;
                        comboBoxItem.Tag = item.DestinationID;
                        DropPoint.Items.Add(comboBoxItem);
                    }

                }
                
            }
        }
        private void AddDestination_Button_Click(object sender, RoutedEventArgs e)
        {
            AddDestination add_destination = new AddDestination();
            add_destination.ShowDialog();
        }

        private void PickupPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
     
            if (((ComboBoxItem)DropPoint.SelectedItem)!=null && ((ComboBoxItem)PickupPoint.SelectedItem) != null && ((ComboBoxItem)PickupPoint.SelectedItem).Tag.ToString() == ((ComboBoxItem)DropPoint.SelectedItem).Tag.ToString())
            {
                MessageBox.Show("Can Not Set Both Point Same.","Warning",MessageBoxButton.OK,MessageBoxImage.Warning);
                PickupPoint.SelectedItem = null;
            }
             
        }

        private void DropPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)PickupPoint.SelectedItem)!=null && ((ComboBoxItem)DropPoint.SelectedItem) != null && ((ComboBoxItem)PickupPoint.SelectedItem).Tag.ToString() == ((ComboBoxItem)DropPoint.SelectedItem).Tag.ToString())
            {
                MessageBox.Show("Can Not Set Both Point Same.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                DropPoint.SelectedItem=null;
            }
        }
    }
}
