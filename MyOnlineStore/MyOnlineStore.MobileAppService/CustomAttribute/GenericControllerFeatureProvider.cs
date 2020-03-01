using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using MyOnlineStore.MobileAppService.Controllers;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static MyOnlineStore.MobileAppService.CustomAttribute.GenericControllerNameAttribute;

namespace MyOnlineStore.MobileAppService.CustomAttribute
{
    public class GenericControllerFeatureProvider : IApplicationFeatureProvider<ControllerFeature>
    {
        public void PopulateFeature(IEnumerable<ApplicationPart> parts, ControllerFeature feature)
        {
            // Get the list of entities that we want to support for the generic controller
            foreach (var entityType in IncludedEntities.Types)
            {
                var typeName = entityType.Name + "Controller";

                // Check to see if there is a "real" controller for this class
                if (!feature.Controllers.Any(t => t.Name == typeName))
                {
                    // Create a generic controller for this type
                    var type = entityType;
                    var astype = type.AsType();
                    var info = typeof(UsersController<>).MakeGenericType(astype);
                    var controllerType = info.GetTypeInfo();
                    
                    feature.Controllers.Add(controllerType);
                }
            }
        }
    }
}
