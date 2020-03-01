using Xamarin.Forms;
using System.Windows.Input;

namespace MyOnlineStore.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class Page1 : ContentPage
    {
        public ICommand PopCommand { get; set; }
        public Page1()
        {
            InitializeComponent();
            BindingContext = this;
        }
        
    }
}