
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.AdminScenarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DashboardPage : ContentPage
    {
        public DashboardPage()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IReportViewModel>();
        }
    }
}