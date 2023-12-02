using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;


namespace WpfApp2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
            Dashboard dashboard = new Dashboard();
            LftRender.Content = dashboard;
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Drivers_Click(object sender, RoutedEventArgs e)
        {
            LftRender.Content = new Drivers();
           
        }

        private void Vehicles_Click(object sender, RoutedEventArgs e)
        {
            LftRender.Content = new Vehicles();
        }

        private void WorkOrder_Click(object sender, RoutedEventArgs e)
        {
            LftRender.Content = new WorkOrders();
        }

        private void Routes_Click(object sender, RoutedEventArgs e)
        {
            LftRender.Content = new Routes();
        }
    }
}