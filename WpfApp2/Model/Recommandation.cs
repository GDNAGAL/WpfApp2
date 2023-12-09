using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Model
{
    internal class Recommandation
    {
        [Key] public string? RouteID { get; set; }
        public string? RouteName { get; set; }
        public string? packageID { get; set; }
        public string? StartPoint { get; set; }
        public string? EndPoint { get; set; }
        public string? Distance { get; set; }
        public int VehicleID { get; set; }
        public string? VehicleRegNo { get; set; }
        public int DriverID { get; set; }
        public string? DriverName { get; set; }

    }
}
