using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Model
{
    internal class DestinationDetails
    {
        [Key]
        public int DestinationID { get; set; }
        public string? DestinationName { get; set; }
        public string? Latitude { get; set; }
        public string? Longitude { get; set; }
    }
}
