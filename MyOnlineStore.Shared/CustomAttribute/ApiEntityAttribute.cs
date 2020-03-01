using System;

namespace MyOnlineStore.Models.CustomAttribute
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public class ApiGenericEntity : Attribute
    {
    }
}