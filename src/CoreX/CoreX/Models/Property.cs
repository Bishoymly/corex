using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace CoreX.Models
{
    public class Property : ICloneable
    {
        public virtual string Name { get; set; }
        public virtual PropertyType Type { get; set; }
        public virtual bool Required { get; set; } = false;
        public virtual IList<IValidation> Validations { get; set; }
        public virtual string ReferenceName { get; set; }
        
        [JsonIgnore]
        public virtual object Reference { get; set; }

        public Property()
        {
            
        }

        public Property(string name, PropertyType type, bool required)
        {
            Name = name;
            Type = type;
            Required = required;
        }

        public Property(string name, string partName)
        {
            Name = name;
            Type = PropertyType.Part;
            ReferenceName = partName;
        }

        public object Clone()
        {
            var clone = new Property();
            clone.Name = this.Name;
            clone.Type = this.Type;
            clone.Required = this.Required;
            
            return clone;
        }
    }
}
