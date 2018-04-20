using System;
using System.Collections.Generic;
using System.Text;

namespace CoreX.Models
{
    public class Property : ModelItem, ICloneable
    {
        public virtual PropertyType Type { get; set; }
        public virtual bool Required { get; set; } = false;
        public virtual IEnumerable<IValidation> Validations { get; set; } = new List<IValidation>();

        public Property()
        {
            base.ItemType = ModelItemType.Property;
        }

        public object Clone()
        {
            var clone = new Property();
            clone.Name = this.Name;
            clone.ItemType = this.ItemType;
            clone.Type = this.Type;
            clone.Required = this.Required;

            return clone;
        }
    }
}
