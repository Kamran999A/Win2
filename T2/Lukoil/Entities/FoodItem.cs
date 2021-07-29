using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2.Abstracts;

namespace T2.Entities
{
    public class FoodItem : Abstracts.Item
    {
        public Food Food { get; set; }
        public int Count { get; set; }
    }
}
