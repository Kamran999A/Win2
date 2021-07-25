using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2
{
    public class Applier : Human
    {
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public IList<string> Languages { get; set; }

        public Applier()
        {
            Languages = new List<string>();
        }
    }
}
