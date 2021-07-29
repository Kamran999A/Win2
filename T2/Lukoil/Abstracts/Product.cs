using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2.Abstracts
{
    public abstract class Product : Id
    {
        public string Name { get; set; }
        public double Price { get; set; }
    }
}
