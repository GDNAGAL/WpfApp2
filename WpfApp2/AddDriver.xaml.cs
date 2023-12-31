﻿using System;
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

namespace WpfApp2
{

    public partial class AddDriver : Window
    {
        private readonly TimeZoneInfo INDIAN_ZONE = TimeZoneInfo.FindSystemTimeZoneById("India Standard Time");

        public object MessageBoxIcon { get; private set; }

        public AddDriver()
        {
            InitializeComponent();
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

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void AddDriver_Button_Click(object sender, RoutedEventArgs e)
        {
            Model.DriverDetails details = new Model.DriverDetails();
            details.FullName = DriverNameTextBox.TextValue;
            details.LicenseExpiryDate = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(LicenceExpiryDateTextBox.TextValue), INDIAN_ZONE);
            details.LicenseNo = LicenceNumberTextBox.TextValue;
            details.Address = AddressTextBox.TextValue;
            details.LicenseType = Convert.ToInt32(LicenceTypeTextBox.TextValue);
            details.DOB = TimeZoneInfo.ConvertTimeFromUtc(Convert.ToDateTime(DOBTextBox.TextValue), INDIAN_ZONE);
            details.AltContactNo = AltContectNumberTextBox.TextValue;
            details.ContactNo = ContectNumberTextBox.TextValue;

            var dataAsString = JsonSerializer.Serialize(details);

            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, "https://localhost:7082/api/DriverDetails/AddDriver");
            request.Headers.Add("accept", "text/plain");
            var content = new StringContent(dataAsString, null, "application/json");
            request.Content = content;
            var response = await client.SendAsync(request);
            Console.WriteLine(response.Content);
            this.Hide();
            MessageBox.Show("Driver Added Successfully !!!", "Success ", MessageBoxButton.OK, MessageBoxImage.Information);

        }

    }
}
