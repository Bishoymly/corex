using System;
using System.Collections.Generic;
using System.Text;

namespace CoreX.Models
{
    public class EntityAction
    {
        public virtual string Name { get; set; }
        public virtual ActionType Input { get; set; }
        public virtual ActionType Output { get; set; }

        public EntityAction(string name, ActionType input, ActionType output)
        {
            Name = name;
            Input = input;
            Output = output;
        }
    }
}
