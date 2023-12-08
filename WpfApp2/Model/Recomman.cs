using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Model
{
    internal class Recomman
    {
        [Key] public string? RouteID { get; set; }
        public string? Summary { get; set; }
        public string? Vehicle { get; set; }
        public string? Driver { get; set; }
        public string? Distance { get; set; }
    }
}
