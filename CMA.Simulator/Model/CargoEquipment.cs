using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CMA_Simulator.Model
{
    public class CargoEquipment
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }
        public string AssignedContainerNumber { get; set; }
        public string EquipmentSizeType { get; set; }
        public string Grade { get; set; }
        public string Commodity { get; set; }
        public string ShipmentId { get; set; }
        public virtual Shipment Shipment { get; set; }

    }

    internal class CargoEquipmentEntityConfiguration : IEntityTypeConfiguration<CargoEquipment>
    {
        public void Configure(EntityTypeBuilder<CargoEquipment> builder)
        {
            builder.HasKey(d => d.Id);
            builder.HasOne(x => x.Shipment).WithMany(p => p.CargoEquipments)
                .HasForeignKey(x => x.ShipmentId);
        }
    }
}
