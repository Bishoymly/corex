using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CoreX.Models
{
    public class Part : ModelItem
    {
        public virtual IEnumerable<ModelItem> Items { get; set; } = new List<ModelItem>();
        public virtual IEnumerable<EntityAction> Actions { get; set; } = new List<EntityAction>();
        
        public virtual IEnumerable<Property> Properties
        {
            get
            {
                return Items.Where(item => item.ItemType == ModelItemType.Property).Cast<Property>();
            }
        }

        public virtual IEnumerable<Part> Parts
        {
            get
            {
                return Items.Where(item => item.ItemType == ModelItemType.Part).Cast<Part>();
            }
        }

        public virtual IEnumerable<Property> AllProperties
        {
            get
            {
                var result = new List<Property>();

                foreach (var item in Items)
                {
                    if(item.ItemType == ModelItemType.Property)
                    {
                        result.Add(item as Property);
                    }

                    if(item.ItemType == ModelItemType.Part)
                    {
                        foreach (var subItem in (item as Part).AllProperties)
                        {
                            var subProperty = subItem.Clone() as Property;
                            subProperty.Name = subItem.Name + subProperty.Name;
                            result.Add(subProperty);
                        }
                    }
                }

                return result;
            }
        }

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
            base.ItemType = ModelItemType.Part;
        }
    }
}
