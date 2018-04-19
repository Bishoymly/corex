using System;
using System.Collections.Generic;
using System.Text;

namespace CoreX.Models
{
    public class Property : ModelItem, ICloneable
    {
        public object Clone()
        {
            var clone = new Property();
            clone.Name = this.Name;
            clone.Type = this.Type;

            return clone;
        }
    }
}
