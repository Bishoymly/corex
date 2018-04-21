using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace CoreX.Models
{
    public class Part
    {
        public virtual string Name { get; set; }
        public virtual IList<Property> Properties { get; set; } = new List<Property>();
        public virtual IList<EntityAction> Actions { get; set; } = new List<EntityAction>();

        [JsonIgnore]
        public virtual IEnumerable<Part> Parts
        {
            get
            {
                return Properties.Where(item => item.Type == PropertyType.Part).Select(p => (Part)p.Reference);
            }
        }

        [JsonIgnore]
        public virtual IEnumerable<Property> AllProperties
        {
            get
            {
                var result = new List<Property>();

                foreach (var property in Properties)
                {
                    if (property.Type == PropertyType.Part)
                    {
                        foreach (var subProperty in (property.Reference as Part).AllProperties)
                        {
                            subProperty.Name = property.Name + subProperty.Name;
                            result.Add(subProperty);
                        }
                    }
                    else
                    {
                        result.Add(property as Property);
                    }
                }

                return result;
            }
        }

        [JsonIgnore]
        public virtual IEnumerable<EntityAction> AllActions
        {
            get
            {
                var result = new List<EntityAction>(Actions);

                foreach (var part in Parts)
                {
                    result.AddRange(part.Actions);
                }

                return result.OrderBy(a => a.Name);
            }
        }

        public Part()
        {
            
        }

        public Part(string name)
        {
            Name = name;
        }
    }
}
