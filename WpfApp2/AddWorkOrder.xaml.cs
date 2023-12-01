﻿using Newtonsoft.Json;
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
using System.Windows.Shapes;
using WpfApp2.Model;

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
    }
}
