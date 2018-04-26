using System;
using System.Collections.Generic;
using System.Text;

namespace CoreX.Models
{
    public class Entity : Part
    {
        public virtual PropertyType PrimaryKeyType { get; set; } = PropertyType.UniqueIdentifier;

        public override IEnumerable<Property> AllProperties
        {
            get
            {
                var result = new List<Property>();
                result.Add(new Property("Id", PrimaryKeyType, true));
                result.AddRange(base.AllProperties);
                return result;
            }
        }

        public Entity()
        {

        }

        public Entity(string name) : base(name)
        {
        }
    }
}
