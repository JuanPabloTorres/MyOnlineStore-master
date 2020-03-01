using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.CustomControls
{
    public partial class CustomShell : Xamarin.Forms.Shell
    {
        public CustomShell()
        {
            
        }

        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create(nameof(StartColor), typeof(Color), typeof(CustomShell), Color.White);

        public static readonly BindableProperty EndColorProperty =
           BindableProperty.Create(nameof(EndColor), typeof(Color), typeof(CustomShell), Color.Black);

        public Color StartColor
        {
            get { return (Color)GetValue(StartColorProperty); }
            set { SetValue(StartColorProperty, value); }
        }

        public Color EndColor
        {
            get { return (Color)GetValue(EndColorProperty); }
            set { SetValue(EndColorProperty, value); }
        }
    }
}
