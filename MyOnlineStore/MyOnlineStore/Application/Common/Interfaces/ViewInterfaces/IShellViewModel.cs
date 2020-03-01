using MyOnlineStore.Entities.Models.Stores;
using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Common.Interfaces.ViewInterfaces
{
    public interface IShellViewModel
    {
        void CreateHeader();
        ObservableCollection<Store>? MyStores { get; set; }
        View? HeaderContent { get; set; }
    }
}
