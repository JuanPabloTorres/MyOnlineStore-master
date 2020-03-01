using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Views.AdminScenarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreSetting : ContentPage
    {
        public static string Route = App.Current.Resources[$"{nameof(StoreSetting) + nameof(Route)}"].ToString();
        public StoreSetting()
        {
            InitializeComponent();
        }
    }
}