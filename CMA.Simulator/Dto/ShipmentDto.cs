using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMA_Simulator.Dto
{
    public class ShipmentDto
    {
        public string ShipmentReference { get; init; }
        public string CargoShipmetId { get; init; }
        public DateTime ClosingDate { get; init; }
        public string ContainerHandlingReference { get; init; }
        public string Destination { get; init; }
        public DateTime SalingDate { get; init; }
        public bool ShipperOwnedBooking { get; init; }
        public string PolWaypoint { get; init; }
        public string Voyage { get; init; }
        public virtual ICollection<CargoEquipmentDto> CargoEquipments { get; set; }
    }
}
