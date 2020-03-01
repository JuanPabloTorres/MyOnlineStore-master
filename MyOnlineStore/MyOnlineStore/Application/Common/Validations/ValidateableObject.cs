using MyOnlineStore.Application.Common.Interfaces.Validations;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Common.Validations
{
    public class ValidateableObject<T> : ExtendedBindableObject, IValidateableObject<T>
    {
        private List<IValidationRule<T>> _validations;
        private List<string> _errors;
        private T _value;
        private bool _isValid = false;
        private ICommand _validateCommmand;

        public List<IValidationRule<T>> ValidationsRules
        {
            get { return _validations; }
            protected set { _validations = value; }
        }

        public List<string> Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
                RaisePropertyChanged(() => Errors);
            }
        }

        public T Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged(() => Value);
            }
        }

        public bool IsValid
        {
            get
            {
                return _isValid;
            }
            set
            {
                _isValid = value;
                RaisePropertyChanged(() => IsValid);
            }
        }

        public ICommand ValidateCommand
        {
            get { return _validateCommmand; }
            set { _validateCommmand = value;}
        }

        public ValidateableObject()
        {
            Init();
        }
        public ValidateableObject(T value)
        {
            _value = value;
            Init();
        }

        private void Init()
        {
            _isValid = true;
            _errors = new List<string>();
            _validations = new List<IValidationRule<T>>();
            ValidateCommand = new Command(() => Validate());
        }

        public bool Validate()
        {
            Errors.Clear();

            IEnumerable<string> errors = _validations.Where(v => !v.Check(Value))
                .Select(v => v.ValidationMessage);

            Errors = errors.ToList();
            IsValid = !Errors.Any();

            return this.IsValid;
        }

        public void ValidationsClear()
        {
            ValidationsRules = new List<IValidationRule<T>>();
        }

        public void Set(T value)
        {
            Value = value;
        }
    }
}
