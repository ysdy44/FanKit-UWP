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
        public Uri Uri;
        public string Summary;

        public Sample(Type page, string name, Uri uri)
        {
            this.Page = page;
            this.Name = name;
            this.Uri = uri;
        }
    }
}
