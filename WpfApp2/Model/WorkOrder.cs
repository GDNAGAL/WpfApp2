using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp2.Model
{
    class WorkOrder
    {
        [Key]
        public string? WorkOrderID { get; set; }
        public DateTime Date { get; set; }
        public int TypeOfPackage { get; set; }
        public string? Package { get; set; }
        public int Quantity { get; set; }
        public string? Unit { get; set; }
        public string? DriverId { get; set; }
        public string? VehicleId { get; set; }
        public string? RouteId { get; set; }
        public string? DestinationID { get; set; }
        public int EstLoadingTime { get; set; }
        public DateTime DepatureTime { get; set; }
        public DateTime EstArrivalTime { get; set; }
        public int EstUnloadingTime { get; set; }
        public int DelayTime { get; set; }
    }
}
