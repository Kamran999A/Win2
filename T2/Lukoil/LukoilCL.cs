using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using T2.Entities;

namespace T2
{
    public class Lukoilcl
    {
        public List<Fuel> Fuels { get; set; }
        public List<Food> Foods { get; set; }

        public List<Bill> Bills { get; set; }

        public Lukoilcl()
        {
            Fuels = new List<Fuel>();
            Foods = new List<Food>();
            Bills = new List<Bill>();
        }

        public double this[string name]
        {
            get => Foods.Single(f => f.Name == name).Price;
        }
    }

}
