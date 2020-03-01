using MyOnlineStore.ViewModels.Base;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.ViewModels
{
    public class Page1ViewModel : BaseViewModel
    {
        public ICommand GetProductPhotoCommand { get; set; }
        public ICommand PlusCommand { get; set; }
        public ICommand MinusCommand { get; set; }

        private uint counter = 0;
        public uint Counter
        {
            get => counter;
            set { counter = value; RaisePropertyChanged(() => Counter); }
        }
        byte[] _productImageSource = null!;
        public byte[] ProductImageSource
        {
            get { return _productImageSource; }
            set { _productImageSource = value; RaisePropertyChanged(() => ProductImageSource); }
        }

        public Page1ViewModel()
        {
            GetProductPhotoCommand = new Command(async () => ProductImageSource = await Utilities.Utils.PickPhoto());
            PlusCommand = new Command(PlusPlus);
            MinusCommand = new Command(MinusMinus);
        }
        private void PlusPlus()
        {
            ++Counter;
        }
        private void MinusMinus()
        {
            --Counter;
        }
    }
}
