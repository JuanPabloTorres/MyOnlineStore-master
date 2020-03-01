using MyOnlineStore.Application.Common.Interfaces.Validations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyOnlineStore.Application.Common.Interfaces.IViewModel
{
    public interface IRegisterStoreViewModel
    {
        bool SaveStore();
        ICommand RegisterStoreCommand { get; }
        ICommand GetLocationCommand { get; }
        ICommand StoreNameValidationCommand { get; }
        ICommand LongitudeValidationCommand { get; }
        ICommand LatitudeValidationCommand { get; }
        ICommand MoreInfoCommand { get; }
        IValidateableObject<string> StoreName { get; }
        IValidateableObject<string> CardNumber { get; }
        IValidateableObject<string> Longitude { get; }
        IValidateableObject<string> Latitude { get; }
        bool Validate();
        Task GetLocation();
        bool VerifiedCard { get; }
    }
}
