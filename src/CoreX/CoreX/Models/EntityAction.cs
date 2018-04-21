using System;
using System.Collections.Generic;
using System.Text;

namespace CoreX.Models
{
    public class EntityAction
    {
        public virtual string Name { get; set; }

        public EntityAction(string name)
        {
            Name = name;
        }
    }
}
