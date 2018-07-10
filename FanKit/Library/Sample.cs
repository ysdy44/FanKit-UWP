using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanKit.Library
{
 public   class Sample
    {
        public Type Page;
        public string Name;
        public string Summary;

        public Sample(Type page, string name)
        {
            this.Page = page;
            this.Name = name;
        }
    }
}
