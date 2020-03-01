using MyOnlineStore.Application.Common.Interfaces.Validations;

namespace MyOnlineStore.Application.Common.Validations.Rules
{
    public class IsNotNullOrEmptyRule<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; } = string.Empty;

        public bool Check(T value)
        {
            if (value == null)
            {
                return false;
            }

            var str = value as string;

            return !string.IsNullOrWhiteSpace(str);
        }
    }
}
