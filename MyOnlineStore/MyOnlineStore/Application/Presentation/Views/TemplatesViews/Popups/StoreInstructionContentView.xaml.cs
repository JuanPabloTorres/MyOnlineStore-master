using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.TemplatesViews.Popups
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreInstructionContentView : ContentView
    {
        public static readonly BindableProperty ShowCommandProperty = BindableProperty.Create(
              propertyName: nameof(ShowCommand),
              returnType: typeof(ICommand),
              declaringType: typeof(StoreInstructionContentView),
              defaultValue: null,
              defaultBindingMode: BindingMode.Default
          );
        public ICommand ShowCommand
        {
            get => (ICommand)GetValue(ShowCommandProperty);
            set
            {
                SetValue(ShowCommandProperty, value);
            }
        }
        public StoreInstructionContentView()
        {
            InitializeComponent();
            ShowCommand = new Command(() => ShowCommandHandler());
            ShowCommand.Execute(null);
        }

        private void ShowCommandHandler()
        {
            popup.Show();
        }
    }
}