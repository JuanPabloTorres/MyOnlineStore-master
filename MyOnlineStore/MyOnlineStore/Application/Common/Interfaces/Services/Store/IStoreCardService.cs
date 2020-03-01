using System.Windows.Input;

namespace MyOnlineStore.Application.Common.Interfaces.Services.Store
{
    public interface IStoreCardService
    {
        ICommand NavigateToStoreDetailCommand { get; }
        ICommand MakeStoreFavoriteClickCommand { get; }
    }
}
