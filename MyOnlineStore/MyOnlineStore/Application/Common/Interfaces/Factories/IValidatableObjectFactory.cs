using MyOnlineStore.Application.Common.Interfaces.Validations;
using MyOnlineStore.Application.Common.Validations;
using System;
using System.Collections.Generic;
using System.Text;

namespace MyOnlineStore.Application.Common.Interfaces.Factories
{
    public interface IValidatableObjectFactory
    {
        ValidateableObject<T> CreateSimpleValidatebleObject<T>();
        ValidateableObject<T> CreateSimpleValidatebleObject<T>(T value);
    }
}
