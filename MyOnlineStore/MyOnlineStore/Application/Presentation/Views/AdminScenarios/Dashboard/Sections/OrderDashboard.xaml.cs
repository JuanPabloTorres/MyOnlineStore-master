using MyOnlineStore.Application.Common.Interfaces.IViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios.Dashboard.Sections
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class OrderDashboard : ContentPage
    {
        public OrderDashboard()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IReportViewModel>();
        }
    }
}