using MyOnlineStore.Application.Common.Interfaces.IViewModel;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.AdminScenarios.Dashboard.Sections
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GoalDashboard : ContentPage
    {
        public GoalDashboard()
        {
            InitializeComponent();
            BindingContext = Startup.ServiceProvider.GetService<IReportViewModel>();
        }
    }
}