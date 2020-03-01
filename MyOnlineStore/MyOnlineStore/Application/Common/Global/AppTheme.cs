using MyOnlineStore.Application;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Xamarin.Forms;

namespace MyOnlineStore.Global
{
    public enum XappyTheme
    {
        Default,
        Clancey
    }

    public class AppTheme : INotifyPropertyChanged
    {
        public bool UseFlyout { get; set; }

        public bool UseTabs
        {
            get
            {
                return !UseFlyout;
            }
        }

        public XappyTheme CurrentTheme { get; set; } = XappyTheme.Default;

        public AppTheme()
        {
        }

        public void InitTheme()
        {
            App.Current.Resources["pageBackgroundColor"] = Color.White; //Application.Current.Resources["black"];
        }

        public void SetTheme(XappyTheme theme)
        {

            switch (theme)
            {
                case XappyTheme.Clancey:
                    App.Current.Resources["pageBackgroundColor"] = LookupColor("almost_black");
                    App.Current.Resources["flyoutGradientStart"] = LookupColor("dark_peach");
                    App.Current.Resources["flyoutGradientEnd"] = LookupColor("light_peach");
                    break;
                default:
                    App.Current.Resources["Primary"] = LookupColor("Primary");
                    App.Current.Resources["PrimaryLight"] = LookupColor("PrimaryLight");
                    //App.Current.Resources["Secondary"] = LookupColor("Secondary");
                    break;
            }

        }

        public Color LookupColor(string key)
        {
            if(App.Current.Resources.TryGetValue(key, out var newColor))
            {
                return (Color)newColor;
            }
            return Color.White;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void SetAndRaisePropertyChanged<TRef>(
            ref TRef field, TRef value, [CallerMemberName] string propertyName = null!)
        {
            field = value;
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected void SetAndRaisePropertyChangedIfDifferentValues<TRef>(
            ref TRef? field, TRef value, [CallerMemberName] string propertyName = null!)
            where TRef : class
        {
            if (field == null || !field.Equals(value))
            {
                SetAndRaisePropertyChanged(ref field, value, propertyName);
            }
        }
    }
}
