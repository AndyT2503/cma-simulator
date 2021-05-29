using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMA_Simulator.Model
{
    public class Shipment
    {
        public string ShipmentId { get; set; }
        public string CargoShipmetId { get; set; }
        public DateTime ClosingDate { get; set; }
        public string ContainerHandlingReference { get; set; }
        public string Destination { get; set; }
        public DateTime SalingDate { get; set; }
        public bool ShipperOwnedBooking { get; set; }
        public string PolWaypoint { get; set; }
        public string Voyage { get; set; }
        public virtual ICollection<CargoEquipment> CargoEquipments { get; set; }
    }

    internal class ShipmentEntityConfiguration : IEntityTypeConfiguration<Shipment>
    {
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasKey(d => d.ShipmentId);
            builder.HasMany(d => d.CargoEquipments).WithOne(d => d.Shipment).HasForeignKey(d => d.ShipmentId);
        }
    }
}
