using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Validations;

namespace MyOnlineStore.Application.Data.Factories.ValidatableObjectsFactories
{
    public class ValidatableObjectsFactory : IValidatableObjectFactory
    {
        public ValidateableObject<T> CreateSimpleValidatebleObject<T>()
        {
            return new ValidateableObject<T>();
        }
        public ValidateableObject<T> CreateSimpleValidatebleObject<T>(T value)
        {
            return new ValidateableObject<T>(value);
        }
    }
}
