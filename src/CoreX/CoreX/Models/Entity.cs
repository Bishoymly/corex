using System;
using System.Collections.Generic;
using System.Text;

namespace CoreX.Models
{
    public class Entity : Part
    {
        public virtual PropertyType PrimaryKeyType { get; set; } = PropertyType.UniqueIdentifier;

        public Entity()
        {

        }

        public Entity(string name) : base(name)
        {
        }
    }
}
