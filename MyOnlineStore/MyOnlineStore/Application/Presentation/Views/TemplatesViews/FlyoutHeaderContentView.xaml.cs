using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.Views.Templates
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class FlyoutHeaderContentView : ContentView
    {
        public static BindableProperty HeaderContentViewProperty = BindableProperty.Create(nameof(HeaderContentView),typeof(View),typeof(FlyoutHeaderContentView),null,propertyChanged: OnHeaderContentChanged);
        public View HeaderContentView
        {
            get { return (View)GetValue(HeaderContentViewProperty); }
            set { SetValue(HeaderContentViewProperty, value); }
        }

        public FlyoutHeaderContentView()
        {
            InitializeComponent();
        }
        static void OnHeaderContentChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is FlyoutHeaderContentView instance)
            {
                if (newValue is StackLayout value)
                {
                    instance.SetValue(HeaderContentViewProperty, value);
                    instance.HeaderContentView = value;
                }
            }
        }
    }
}