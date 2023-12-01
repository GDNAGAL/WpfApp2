using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace WpfApp2.Model
{
    public class DriverDetails
    {

        public string? DriverID { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? FullName { get; set; }

        public DateTime DOB { get; set; }

        public string? DOBs {  get; set; }

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
}
