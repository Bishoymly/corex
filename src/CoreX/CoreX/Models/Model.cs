using System;
using System.Collections.Generic;
using System.Text;

namespace CoreX.Models
{
    public class Model
    {
        public virtual IList<Entity> Entities { get; set; } = new List<Entity>();
        public virtual IList<Part> Parts { get; set; } = new List<Part>();
    }
}
