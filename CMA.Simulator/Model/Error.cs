using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMA_Simulator.Model
{
    public class ErrorDetails
    {
        public string Reason { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public override string ToString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
