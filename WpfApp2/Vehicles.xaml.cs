﻿using System;
using System.Collections.Generic;
using System.Linq;
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

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Vehicles.xaml
    /// </summary>
    public partial class Vehicles : Page
    {
        public Vehicles()
        {
            InitializeComponent();
        }

        private void addvehicle_Click(object sender, RoutedEventArgs e)
        {
            AddVehicle add_vehicle = new AddVehicle();
            add_vehicle.ShowDialog();
        }
    }
}
