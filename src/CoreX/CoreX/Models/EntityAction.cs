using System;
using System.Collections.Generic;
using System.Text;

namespace CoreX.Models
{
    public class EntityAction
    {
        public virtual string Name { get; set; }
        public virtual ActionSchema Input { get; set; }
        public virtual ActionSchema Output { get; set; }

        public EntityAction(string name, ActionSchema input, ActionSchema output)
        {
            Name = name;
            Input = input;
            Output = output;
        }
    }
}
