using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2.Entities
{
    public class Bill : Id
    {
        public FuelItem FuelItem { get; set; }
        public List<FoodItem> FoodItems { get; set; }

        public double TotalCost { get; set; }
        public Bill()
        {
            FoodItems = new List<FoodItem>();
        }
    }
}
