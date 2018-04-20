using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoreX.Models;

namespace CoreX.Host
{
    public static class Samples
    {
        public static Model CreateSampleModel()
        {
            var model = new Model();

            var audited = new Part
            {
                Name = "Audited",
                Items = new List<ModelItem>
                {
                    new Property{Name = "DateCreated", Type = PropertyType.DateTime, Required = true },
                    new Property{Name = "DateModified", Type = PropertyType.DateTime, Required = false },
                    new Property{Name = "CreatedBy", Type = PropertyType.UniqueIdentifier, Required = true },
                    new Property{Name = "ModifiedBy", Type = PropertyType.UniqueIdentifier, Required = false }
                },
                Actions = new List<EntityAction>
                {
                    new EntityAction{Name = "Get" },
                    new EntityAction{Name = "Create" },
                    new EntityAction{Name = "Update" },
                    new EntityAction{Name = "Delete" }                    
                }
            };
            model.Parts.Add(audited);

            var log = new Part
            {
                Name = "LogItem",
                Items = new List<ModelItem>
                {
                    new Property{Name = "DateCreated", Type = PropertyType.DateTime, Required = true },
                    new Property{Name = "CreatedBy", Type = PropertyType.String, Required = true },
                },
                Actions = new List<EntityAction>
                {
                    new EntityAction{Name = "Get" },
                    new EntityAction{Name = "Create" },
                    new EntityAction{Name = "Archive" }
                }
            };
            model.Parts.Add(log);

            model.Entities.Add(new Entity
            {
                Name = "Product",
                Items = new List<ModelItem>
                {
                    new Property{Name = "Name", Type = PropertyType.String, Required = true },
                    new Property{Name = "Image", Type = PropertyType.String, Required = false },
                    new Property{Name = "Price", Type = PropertyType.Decimal, Required = true },
                    new Property{Name = "Quantity", Type = PropertyType.Integer, Required = false },
                    audited
                }
            });

            model.Entities.Add(new Entity
            {
                Name = "AuditLog",
                Items = new List<ModelItem>
                {
                    new Property{Name = "Action", Type = PropertyType.String, Required = true },
                    new Property{Name = "Comment", Type = PropertyType.String, Required = false },
                    log
                }
            });

            return model;
        }
    }
}
