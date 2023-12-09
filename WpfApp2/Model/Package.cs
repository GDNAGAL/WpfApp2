using System;
using System.ComponentModel.DataAnnotations;

namespace WpfApp2.Model
{
    public class Package
    {
        [Key] public int packageId { get; set; }
        public string? packageType { get; set; }
        public string? packages { get; set; }
        public int quantity { get; set; }
        public string? unit { get; set; }
        public DateTime PackageDate { get; set; }
        

    }
}
