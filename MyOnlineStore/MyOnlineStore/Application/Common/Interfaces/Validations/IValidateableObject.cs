using System.Collections.Generic;
using System.Windows.Input;

namespace MyOnlineStore.Application.Common.Interfaces.Validations
{
    public interface IValidateableObject<T> : IValidity
    {
        List<string> Errors { get; }
        T Value { get; }
        List<IValidationRule<T>> ValidationsRules { get; }
        bool Validate();
        void ValidationsClear();
        ICommand ValidateCommand { get; }
        void Set(T value);
    }
}
