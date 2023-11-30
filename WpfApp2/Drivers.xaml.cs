using System;
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
using System.Collections.ObjectModel;
using System.Net.Http;
using Newtonsoft.Json;

namespace WpfApp2
{
    /// <summary>
    /// Interaction logic for Drivers.xaml
    /// </summary>
    public partial class Drivers : Page
    {
        ObservableCollection<DriverDetails> members = new ObservableCollection<DriverDetails>();
        //ObservableCollection<Member> members = new ObservableCollection<Member>();
        public Drivers()
        {
            InitializeComponent();

            //var converter = new BrushConverter();

            //members.Add(new Member { Number = "1", Character = "J", BgColor = (Brush)converter.ConvertFromString("#1098AD"), Name = "John Doe", Position = "Coach", Email = "john.doe@gmail.com", Phone = "415-954-1475" });
            //members.Add(new Member { Number = "2", Character = "R", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Reza Alavi", Position = "Administrator", Email = "reza110@hotmail.com", Phone = "254-451-7893" });
            //members.Add(new Member { Number = "3", Character = "D", BgColor = (Brush)converter.ConvertFromString("#FF8F00"), Name = "Dennis Castillo", Position = "Coach", Email = "deny.cast@gmail.com", Phone = "125-520-0141" });
            //members.Add(new Member { Number = "4", Character = "G", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Gabriel Cox", Position = "Coach", Email = "coxcox@gmail.com", Phone = "808-635-1221" });
            //members.Add(new Member { Number = "5", Character = "L", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Lena Jones", Position = "Manager", Email = "lena.offi@hotmail.com", Phone = "320-658-9174" });
            //members.Add(new Member { Number = "6", Character = "B", BgColor = (Brush)converter.ConvertFromString("#6741D9"), Name = "Benjamin Caliword", Position = "Administrator", Email = "beni12@hotmail.com", Phone = "114-203-6258" });
            //members.Add(new Member { Number = "7", Character = "S", BgColor = (Brush)converter.ConvertFromString("#FF6D00"), Name = "Sophia Muris", Position = "Coach", Email = "sophi.muri@gmail.com", Phone = "852-233-6854" });
            //members.Add(new Member { Number = "8", Character = "A", BgColor = (Brush)converter.ConvertFromString("#FF5252"), Name = "Ali Pormand", Position = "Manager", Email = "alipor@yahoo.com", Phone = "968-378-4849" });
            //members.Add(new Member { Number = "9", Character = "F", BgColor = (Brush)converter.ConvertFromString("#1E88E5"), Name = "Frank Underwood", Position = "Manager", Email = "frank@yahoo.com", Phone = "301-584-6966" });
            //members.Add(new Member { Number = "10", Character = "S", BgColor = (Brush)converter.ConvertFromString("#0CA678"), Name = "Saeed Dasman", Position = "Coach", Email = "saeed.dasi@hotmail.com", Phone = "817-320-5052" });

            //membersDataGrid.ItemsSource = members;

        }

        private void AddDriver_Click(object sender, RoutedEventArgs e)
        {
            AddDriver add_driver = new AddDriver();
            add_driver.ShowDialog();
        }
        public async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var converter = new BrushConverter();
            string[] color = { "#1098AD", "#1E88E5", "#FF8F00", "#FF5252", "#0CA678", "#6741D9", "#FF6D00", "#FF5252", "#1E88E5", "#0CA678" };
            using (HttpClient client = new HttpClient())
            {
                var respons = await client.GetAsync("https://localhost:7082/api/DriverDetails/GetAllDrivers");
                respons.EnsureSuccessStatusCode();
                if (respons.IsSuccessStatusCode)
                {
                    var jsonString = await respons.Content.ReadAsStringAsync();
                    members = JsonConvert.DeserializeObject<ObservableCollection<DriverDetails>>(jsonString);
                    int i = 0, j = 1;
                    foreach (var item in members)
                    {
                        if (i == color.Length)
                        {
                            i = 0;
                        }
                        item.BgColor = (Brush)converter.ConvertFromInvariantString(color[i]);
                        item.Number = (j).ToString();
                        item.Character = item.FullName[0].ToString();
                        i++;
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
    public class DriverDetails
    {

        public string? DriverID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public DateTime DOB { get; set; }

        public int LicenseType { get; set; }

        public string? LicenseNo { get; set; }

        public DateTime LicenseExpiryDate { get; set; }

        public string? ContactNo { get; set; }

        public string? AltContactNo { get; set; }
        public string? Email { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public int ZipCode { get; set; }

        public string? Country { get; set; }
        public string? Character { get; set; }
        public string? Number { get; set; }
        public Brush? BgColor { get; set; }

    }


    //public class Member
    //{
    //    public string? FullName { get; set; }
    //    public string ContactNo { get; set; }
    //    public string City { get; set; }
    //    public string Position { get; set; }
    //    public string Email { get; set; }
    //    public string Phone { get; set; }
    //}

}