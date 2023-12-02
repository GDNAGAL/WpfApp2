using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Model
{
    internal class RoutesDetails
    {
        [Key] public string? RouteID { get; set; }
        public string? StartPoint { get; set; }
        public string? EndPoint { get; set; }
        public string? MidCity1 { get; set; }
        public string? MidCity2 { get; set; }
        public string? MidCity3 { get; set; }
    }
}
