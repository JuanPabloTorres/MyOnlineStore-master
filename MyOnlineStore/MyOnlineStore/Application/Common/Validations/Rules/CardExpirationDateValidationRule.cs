using MyOnlineStore.Application.Common.Interfaces.Validations;
using System;

namespace MyOnlineStore.Application.Common.Validations.Rules
{
    public class CardExpirationDateValidationRule : IValidationRule<DateTime>
    {
        public string ValidationMessage { get; set; } = string.Empty;

        public bool Check(DateTime value)
        {
            bool isValid;
            if (value.Year > DateTime.Today.Year)
            {
                isValid = true;
            }
            else if (value.Year == DateTime.Today.Year)
            {
                if (value.Month > DateTime.Today.Month)
                {
                    isValid = true;
                }
                else
                    isValid = false;
            }
            else
                isValid = false;

            return isValid;
        }
    }
}
