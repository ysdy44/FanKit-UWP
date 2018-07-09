using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FanKit.Library
{
    /// <summary>
    /// 页面与名称
    /// </summary>
  public  class PageType
    {
        public Type Page;
        public string Name;

        public PageType(Type page, string name)
        {
            this.Page = page;
            this.Name = name;
        }
    }
}
