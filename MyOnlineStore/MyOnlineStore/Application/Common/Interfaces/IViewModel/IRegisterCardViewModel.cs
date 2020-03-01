using MyOnlineStore.Application.Data.Models.Entries;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.Validations;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Entities.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using MyOnlineStore.Shared.Interfaces.Factories;

namespace MyOnlineStore.Application.Common.Interfaces.IViewModel
{
    public interface IRegisterCardViewModel
    {
        IUserCardDataStore _UserCardDataStore { get; }
        IValidatableObjectFactory _ValidatableObjectFactory { get; }
        RegisterEntry UserRegistrationInfo { get; set; }
        CardAccount CardAccount { get; }
        List<string> TypeOfCards { get; }
        public bool IsCreditCard { get; }

        IValidateableObject<string> CardNumber { get; }
        IValidateableObject<string> TypeOfCard { get; }
        IValidateableObject<string> SecurityCode { get;  }
        IValidateableObject<DateTime> ExpirationDate { get;  }
        IValidateableObject<string> CardHolderName { get;  }

        ICommand RegisterCardCommand { get; }
        ICommand TypeOfCardChangedCommand { get; }
        ICommand SkipToHomeCommand { get; }

        Task<bool> RegisterCard(Guid? storeId);
        bool Validate();
        void TypeOfCardSelectionChangedHandler(CommandEventData data);

        public void LoadFirstCard(User user);

        ICardFactory CardFactory { get; }
    }
}
