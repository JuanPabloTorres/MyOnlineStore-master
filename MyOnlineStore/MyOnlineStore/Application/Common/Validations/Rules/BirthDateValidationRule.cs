using MyOnlineStore.Application.Common.Interfaces.Validations;
using System;

namespace MyOnlineStore.Application.Common.Validations.Rules
{
    public class BirthDateValidationRule : IValidationRule<DateTime>
    {
        public string ValidationMessage { get; set; } = string.Empty;

        public bool Check(DateTime value)
        {
            bool isValid ;

            

            return true;
        }
    }
}
