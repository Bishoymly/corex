using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreX.Models
{
    public class Model
    {
        public virtual IList<Entity> Entities { get; set; } = new List<Entity>();
        public virtual IList<Part> Parts { get; set; } = new List<Part>();

        public void Initialize()
        {
            foreach (var entity in Entities)
            {
                foreach (var property in entity.Properties)
                {
                    if(property.Type == PropertyType.Part)
                    {
                        property.Reference = Parts.FirstOrDefault(p => p.Name == property.ReferenceName);
                    }
                }
            }
        }
    }
}
