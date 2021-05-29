using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CMA_Simulator.Dto
{
    public class CreateReuseDto
    {
        public string ShipmentReference { get; init; }
        public DateTime? RequestDate { get; init; }
        public string Destination { get; init; }
    }
}
