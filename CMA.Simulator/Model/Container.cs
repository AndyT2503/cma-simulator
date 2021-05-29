using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMA_Simulator.Model
{
    public class Container
    {
        [Key]
        public string ContainerNumber { get; set; }
        public string LeaseType { get; set; }
        public string BuildYearAndCountry { get; set; }
        public string EquipmentSizeType { get; set; }
        public string LastEvent { get; set; }
        public DateTime LasEventDateTime { get; set; }
        public bool? CreditTermsRule { get; set; }
    }

    internal class ContainerEntityConfiguration : IEntityTypeConfiguration<Container>
    {
        public void Configure(EntityTypeBuilder<Container> builder)
        {
            builder.HasKey(d => d.ContainerNumber);
        }
    }
}
