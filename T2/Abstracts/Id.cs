using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace T2
{
    public abstract class Id
    {
        public Guid Guid { get; set; }
        protected Id()
        {
            Guid = Guid.NewGuid();
        }
    }
}
