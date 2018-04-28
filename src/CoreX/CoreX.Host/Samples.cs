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
                Properties = new List<Property>
                {
                    new Property("DateCreated", PropertyType.DateTime, true),
                    new Property("DateModified", PropertyType.DateTime, false),
                    new Property("CreatedBy", PropertyType.UniqueIdentifier, true),
                    new Property("ModifiedBy", PropertyType.UniqueIdentifier, false)
                },
                Actions = new List<EntityAction>
                {
                    new EntityAction("GetAll", ActionType.Nothing, ActionType.List),
                    new EntityAction("Get", ActionType.Id, ActionType.Entity),
                    new EntityAction("Create", ActionType.Entity, ActionType.Entity),
                    new EntityAction("Update", ActionType.Entity, ActionType.Entity),
                    new EntityAction("Delete", ActionType.Id, ActionType.Nothing)
                }
            };
            model.Parts.Add(audited);

            var log = new Part
            {
                Name = "LogItem",
                Properties = new List<Property>
                {
                    new Property("DateCreated", PropertyType.DateTime, true),
                    new Property("CreatedBy", PropertyType.String, true),
                },
                Actions = new List<EntityAction>
                {
                    new EntityAction("GetAll", ActionType.Nothing, ActionType.List),
                    new EntityAction("Create", ActionType.Entity, ActionType.Nothing),
                    new EntityAction("Archive", ActionType.Nothing, ActionType.Nothing)
                }
            };
            model.Parts.Add(log);

            model.Entities.Add(new Entity
            {
                Name = "Product",
                Properties = new List<Property>
                {
                    new Property("Name", PropertyType.String, true),
                    new Property("Image", PropertyType.String, false),
                    new Property("Price", PropertyType.Decimal, true),
                    new Property("Quantity", PropertyType.Integer, false),
                    new Property("", "Audited")
                }
            });

            model.Entities.Add(new Entity
            {
                Name = "AuditLog",
                Properties = new List<Property>
                {
                    new Property("Action", PropertyType.String, true),
                    new Property("Comment", PropertyType.String, false),
                    new Property("", "LogItem")
                }
            });

            model.Initialize();

            return model;
        }
    }
}
