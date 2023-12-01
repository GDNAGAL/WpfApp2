using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Model
{
    internal class VehicleDetails
    {
        [Key] public string? VehicleID { get; set; }
        public int VehicleType { get; set; }
        public string? VehicleNumber { get; set; }

        public int VehicleClass { get; set; }

        public string? ChassisNo { get; set; }
        public string? Color { get; set; }
        public string? RegistrationNo { get; set; }
        public string? Make { get; set; }
        public string? Model { get; set; }
        public int Year { get; set; }
        public int Age { get; set; }
        public int Capacity { get; set; }
        public string? CapacityUnit { get; set; }
        public DateTime LastMaintenanceDate { get; set; }
        public string? Status { get; set; }
        public byte isAllDocumentValid { get; set; }
        public string? FuelType { get; set; }
        public int Mileage { get; set; }
        public string? DriverId { get; set; }
        public string? Remark { get; set; }
        public string? Number { get; set; }
    }
}
