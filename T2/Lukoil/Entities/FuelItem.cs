using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2.Entities
{
    public class FuelItem : Abstracts.Item
    {
        public Fuel Fuel { get; set; }
        public double Liter { get; set; } 
        public double Totalcost { get; set; }
    }
}
