using System;
using System.Collections.Generic;
using System.Text;

namespace CoreX.Models
{
    public class ModelItem
    {
        public virtual string Name { get; set; }
        public virtual ModelItemType ItemType { get; set; }
    }
}
