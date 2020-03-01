using MyOnlineStore.Application.Common.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Application.Common.Validations.Rules
{
    public class CompareValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; } = string.Empty;
        public object CompareToString { get; set; } = string.Empty;
        public bool Check(T value)
        {
            bool isEqual;

            if (CompareToString != null)
            {
                isEqual = CompareToString.Equals(value);
            }
            else
                isEqual = false;

            return isEqual;
        }
    }
}
