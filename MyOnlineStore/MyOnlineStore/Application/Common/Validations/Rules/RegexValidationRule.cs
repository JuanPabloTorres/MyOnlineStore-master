using MyOnlineStore.Application.Common.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace MyOnlineStore.Application.Common.Validations.Rules
{
    public class RegexValidationRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; } = string.Empty;
        public string RegexString { get; set; } = string.Empty;
        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;
            bool isValid = Regex.IsMatch(str, RegexString);
            return isValid;
        }
    }
}
