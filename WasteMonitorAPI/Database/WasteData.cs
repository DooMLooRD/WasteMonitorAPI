using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WasteMonitorAPI.Database
{
    public class WasteData
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public double Weight { get; set; }
        public double FillingLevel { get; set; }

    }
}
