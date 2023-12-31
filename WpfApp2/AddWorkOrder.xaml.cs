﻿using Newtonsoft.Json;
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
using System.Text.Json;

using WpfApp2.UserControls;
using System.Reflection.Metadata;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for AddWorkOrder.xaml
    /// </summary>
    public partial class AddWorkOrder : Window
    {
        private readonly TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");
        ObservableCollection<DestinationDetails> members = new ObservableCollection<DestinationDetails>();
        ObservableCollection<RoutesDetails> routes = new ObservableCollection<RoutesDetails>();
        ObservableCollection<Recommandation> recomm = new ObservableCollection<Recommandation>();
        ObservableCollection<VehicleDetails> vehicles = new ObservableCollection<VehicleDetails>();

        public AddWorkOrder()
        {
            InitializeComponent();
            loadDestinations();

            //recomm.Add(new Recommandation { RouteID = "1", Vehicle = "RJ07UA5258", Driver = "Pawan", Summary = "NH 44", Distance = "200 KM" });
            //recomm.Add(new Recommandation { RouteID = "1", Vehicle = "RJ07UA5258", Driver = "Pawan", Summary = "NH 44", Distance = "220 KM" });
            //recomm.Add(new Recommandation { RouteID = "1", Vehicle = "RJ07UA5258", Driver = "Pawan", Summary = "NH 44", Distance = "240 KM" });



            // Disable dates prior to today
            WorkOrderDatePicker.BlackoutDates.Add(new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-1)));

            // Enable the next 30 days including today
            DateTime endDate = DateTime.Today.AddDays(29);
            WorkOrderDatePicker.DisplayDateEnd = endDate;
        }

        private async void loadDestinations()
        {

            using (HttpClient client = new HttpClient())
            {
                var respons = new HttpResponseMessage();
                try
                {
                    respons = await client.GetAsync("https://localhost:7082/api/DestinationManage/GetAllDestinationDetails");
                    respons.EnsureSuccessStatusCode();
                    if (respons.IsSuccessStatusCode)
                    {
                        var jsonString = await respons.Content.ReadAsStringAsync();
                        members = JsonConvert.DeserializeObject<ObservableCollection<DestinationDetails>>(jsonString);
                        PickupPoint.Items.Clear();
                        DropPoint.Items.Clear();
                        foreach (var item in members)
                        {
                            ComboBoxItem comboBoxItem = new();
                            comboBoxItem.Content = item.DestinationName;
                            comboBoxItem.Tag = item.DestinationID;
                            PickupPoint.Items.Add(comboBoxItem);
                            comboBoxItem = new();
                            comboBoxItem.Content = item.DestinationName;
                            comboBoxItem.Tag = item.DestinationID;
                            DropPoint.Items.Add(comboBoxItem);
                        }

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void AddDestination_Button_Click(object sender, RoutedEventArgs e)
        {
            AddDestination add_destination = new AddDestination();
            add_destination.Closed += addDestinationDialog_Closed;
            add_destination.ShowDialog();
        }
        private void addDestinationDialog_Closed(object sender, EventArgs e)
        {
            // The dialog is closed and Destination Refreshed
            //MessageBox.Show("Dialog is closed.");

            loadDestinations();

        }
        private void PickupPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (((ComboBoxItem)DropPoint.SelectedItem) != null && ((ComboBoxItem)PickupPoint.SelectedItem) != null && ((ComboBoxItem)PickupPoint.SelectedItem).Tag.ToString() == ((ComboBoxItem)DropPoint.SelectedItem).Tag.ToString())
            {
                MessageBox.Show("Can Not Set Both Point Same.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                PickupPoint.SelectedItem = null;
            }

        }

        private void DropPoint_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)PickupPoint.SelectedItem) != null && ((ComboBoxItem)DropPoint.SelectedItem) != null && ((ComboBoxItem)PickupPoint.SelectedItem).Tag.ToString() == ((ComboBoxItem)DropPoint.SelectedItem).Tag.ToString())
            {
                MessageBox.Show("Can Not Set Both Point Same.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                DropPoint.SelectedItem = null;
            }
        }

        private void Get_Route_Button_Click(object sender, RoutedEventArgs e)
        {
            if (((ComboBoxItem)PickupPoint.SelectedItem) != null && ((ComboBoxItem)DropPoint.SelectedItem) != null)
            {

            }
            else
            {
                MessageBox.Show("Please Select Pickup and Drop Point", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }


        private void CreateOrder(object sender, RoutedEventArgs e)
        {
            WorkOrder workOrder = new WorkOrder();
            workOrder.Date = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(WorkOrderDatePicker.Text), INDIAN_ZONE);
            workOrder.Package = Package.TextValue;
            workOrder.Quantity = Convert.ToInt32(Quantity.TextValue);
            if (((ComboBoxItem)Unit.SelectedItem) != null)
            {
                workOrder.Unit = ((ComboBoxItem)Unit.SelectedItem).Content.ToString();
            }


        }

        private async void Recommendations_Button_Click(object sender, RoutedEventArgs e)
        {
            var workorderdate = WorkOrderDatePicker.Text;
            var packageQty = Quantity.TextValue;

            if (((ComboBoxItem)PackageType.SelectedItem) != null && workorderdate != "" && packageQty != "" && ((ComboBoxItem)PickupPoint.SelectedItem) != null && ((ComboBoxItem)DropPoint.SelectedItem) != null)
            {
                Package obj = new Package();

                var packageType = ((ComboBoxItem)PackageType.SelectedItem).Content.ToString();
                var unit = ((ComboBoxItem)Unit.SelectedItem).Content.ToString();
                var pickuppoint = ((ComboBoxItem)PickupPoint.SelectedItem).Content.ToString();
                var dropPoint = ((ComboBoxItem)DropPoint.SelectedItem).Content.ToString();
                //call api
                obj.packageType = packageType;
                obj.PackageDate = Convert.ToDateTime(workorderdate);
                obj.unit = unit;
                obj.packages = Package.Content.ToString()+" ";
                obj.quantity =Convert.ToInt32(packageQty);
                var data = System.Text.Json.JsonSerializer.Serialize(obj);
                var client = new HttpClient();
                try
                {
                    var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7082/api/GetRecommendation/GetRecommendations?origin=" + pickuppoint + "&destination=" + dropPoint);
                    var content = new StringContent(data, null, "application/json");
                    request.Content = content;
                    var response = await client.SendAsync(request);
                    response.EnsureSuccessStatusCode();
                    var result = await response.Content.ReadAsStringAsync();
                    //ObservableCollection<WpfApp2.Model.Recommandation> re = new ObservableCollection<Recommandation>();
                    recomm = JsonConvert.DeserializeObject<ObservableCollection<Recommandation>>(result);
                    lstCards.ItemsSource = recomm;
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }
            else
            {
                MessageBox.Show("Please Select All Required Field", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private async void Save_WorkOrder(object sender, RoutedEventArgs e)
        {
            Recommandation rec = lstCards.SelectedItem as Recommandation;
            WorkOrder order=new();
            if (rec!=null)
            {
                 order = new() {
                    PackageId = rec.packageID,
                    RouteId = rec.RouteID,
                    Package = Package.TextValue,
                    TypeOfPackage = ((ComboBoxItem)PackageType.SelectedItem).Content.ToString(),
                    Quantity = Convert.ToInt32(Quantity.TextValue),
                    Unit = ((ComboBoxItem)Unit.SelectedItem).Content.ToString(),
                    Date = Convert.ToDateTime(WorkOrderDatePicker.Text)
                };
            }
            var data = System.Text.Json.JsonSerializer.Serialize(order);
            HttpClient client = new HttpClient();
            try
            {
                var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7082/api/WorkOrder/AddNewWorkOrder\r\n");
                var content = new StringContent(data, null, "application/json");
                request.Content = content;
                var response = await client.SendAsync(request);
                response.EnsureSuccessStatusCode();
                var result = await response.Content.ReadAsStringAsync();
                this.Close();
                MessageBox.Show(result);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ViewDriver(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;

            // Find the ListBoxItem that contains the clicked button
            ListBoxItem clickedListBoxItem = FindVisualParent<ListBoxItem>(clickedButton);

            // Check if the ListBoxItem is not null
            if (clickedListBoxItem != null)
            {
                // Get the data context of the ListBoxItem
                Recommandation itemData = clickedListBoxItem.DataContext as Recommandation;

                // Check if the data context is not null
                if (itemData != null)
                {
                    // Access the DriverName property
                    string driverName = itemData.DriverName;
                    int driverid = itemData.DriverID;

                    // Now you can use the 'driverName' as needed
                    //MessageBox.Show($"DriverName: {driverName}");
                    DriverProfile driverp = new DriverProfile(driverid);
                    driverp.ShowDialog();
                }
            }
        }



        private void ViewDriverProfile(object sender, RoutedEventArgs e)
        {
            TextBlock clickedButton = sender as TextBlock;

            // Find the ListBoxItem that contains the clicked button
            ListBoxItem clickedListBoxItem = FindVisualParent<ListBoxItem>(clickedButton);

            // Check if the ListBoxItem is not null
            if (clickedListBoxItem != null)
            {
                // Get the data context of the ListBoxItem
                Recommandation itemData = clickedListBoxItem.DataContext as Recommandation;

                // Check if the data context is not null
                if (itemData != null)
                {
                    // Access the DriverName property
                    string driverName = itemData.DriverName;

                    // Now you can use the 'driverName' as needed
                    //MessageBox.Show($"DriverName: {driverName}");
                    DriverProfile driverp = new DriverProfile(itemData.DriverID);
                    driverp.ShowDialog();
                }
            }
        }

        private void ViewVehicleProfile(object sender, RoutedEventArgs e)
        {
            TextBlock clickedButton = sender as TextBlock;

            // Find the ListBoxItem that contains the clicked button
            ListBoxItem clickedListBoxItem = FindVisualParent<ListBoxItem>(clickedButton);

            // Check if the ListBoxItem is not null
            if (clickedListBoxItem != null)
            {
                // Get the data context of the ListBoxItem
                Recommandation itemData = clickedListBoxItem.DataContext as Recommandation;

                // Check if the data context is not null
                if (itemData != null)
                {
                    // Access the DriverName property


                    // Now you can use the 'driverName' as needed
                    //MessageBox.Show($"DriverName: {driverName}");



                    VehicleProfile vehicle = new VehicleProfile(itemData.VehicleID);
                    vehicle.ShowDialog();
                }
            }
        }


        private T FindVisualParent<T>(DependencyObject obj) where T : DependencyObject
        {
            while (obj != null)
            {
                if (obj is T parent)
                {
                    return parent;
                }
                obj = VisualTreeHelper.GetParent(obj);
            }
            return null;
        }


    }
}
