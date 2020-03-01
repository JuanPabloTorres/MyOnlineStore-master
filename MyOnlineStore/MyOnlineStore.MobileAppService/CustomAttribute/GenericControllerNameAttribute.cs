using Microsoft.AspNetCore.Mvc.ApplicationModels;
using MyOnlineStore.MobileAppService.Controllers;
using MyOnlineStore.Models.CustomAttribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MyOnlineStore.MobileAppService.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class GenericControllerNameAttribute : Attribute, IControllerModelConvention
    {
        List<Type> results = new List<Type>();
        public void Apply(ControllerModel controller)
        {
            ListAllDerviedTypes();

            foreach (Type type in results)
            {
                if (controller.ControllerType.GetGenericTypeDefinition() == type)
                {
                    var entityType = controller.ControllerType.GenericTypeArguments[0];
                    controller.ControllerName = entityType.Name;
                }
            }
        }

        public static class IncludedEntities
        {
            public static IReadOnlyList<TypeInfo> Types;

            static IncludedEntities()
            {
                var assembly = typeof(ApiGenericEntity).GetTypeInfo().Assembly;
                var typeList = new List<TypeInfo>();
                var candidates = assembly.GetExportedTypes().Where(x => x.GetCustomAttributes<ApiGenericEntity>().Any());

                foreach (Type candidate in candidates)
                {
                    if (candidate.GetCustomAttributes(typeof(ApiGenericEntity), true).Length > 0)
                    {
                        var info = candidate.GetTypeInfo();
                        typeList.Add(candidate.GetTypeInfo());
                    }
                }

                Types = typeList;
            }
        }

        private void ListAllDerviedTypes()
        {
            Type entityType = typeof(GenericController<>);
            Assembly assembly = Assembly.LoadFrom(entityType.Assembly.Location);
            Type[] types = assembly.GetTypes();

           
            GetAllDerivedTypesRecursively(types, typeof(GenericController<>), ref results);

            //foreach (var type in results)
            //{
            //    Console.WriteLine(type.Name);
            //}
        }

        private static void GetAllDerivedTypesRecursively(Type[] types, Type type1, ref List<Type> results)
        {
            if (type1.IsGenericType)
            {
                GetDerivedFromGeneric(types, type1, ref results);
            }
            else
            {
                GetDerivedFromNonGeneric(types, type1, ref results);
            }
        }

        private static void GetDerivedFromGeneric(Type[] types, Type type, ref List<Type> results)
        {
            var derivedTypes = types
                .Where(t => t.BaseType != null && t.BaseType.IsGenericType &&
                            t.BaseType.GetGenericTypeDefinition() == type).ToList();
            results.AddRange(derivedTypes);
            foreach (Type derivedType in derivedTypes)
            {
                GetAllDerivedTypesRecursively(types, derivedType, ref results);
            }
        }


        public static void GetDerivedFromNonGeneric(Type[] types, Type type, ref List<Type> results)
        {
            var derivedTypes = types.Where(t => t != type && type.IsAssignableFrom(t)).ToList();

            results.AddRange(derivedTypes);
            foreach (Type derivedType in derivedTypes)
            {
                GetAllDerivedTypesRecursively(types, derivedType, ref results);
            }
        }
    }
}
