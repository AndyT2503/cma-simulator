using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMA_Simulator.Dto
{
    public class ContainerResponseDto
    {
        public string LeaseType { get; init; }
        public string BuildYearAndCountry { get; init; }
        public string EquipmentSizeType { get; init; }
        public string LastEvent { get; init; }
        public DateTime LasEventDateTime { get; init; }
        public bool? CreditTermsRule { get; init; }
    }
}
