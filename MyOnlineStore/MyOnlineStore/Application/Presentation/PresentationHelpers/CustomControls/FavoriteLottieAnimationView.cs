using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentation.CustomControls
{

    //########################################################################################
    //
    // 
    //
    //#######################################################################################
    public class FavoriteLottieAnimationView : Lottie.Forms.AnimationView
    {

        public FavoriteLottieAnimationView()
        {
            Animation = "favorite_icon.json";
            Speed = 1f;
        }

        public static readonly BindableProperty StartOfAnimationProperty = 
            BindableProperty.Create(nameof(StartOfAnimation), typeof(int), typeof(FavoriteLottieAnimationView), 0, BindingMode.TwoWay);
        public static readonly BindableProperty EndOfAnimationProperty = 
            BindableProperty.Create(nameof(EndOfAnimation), typeof(int), typeof(FavoriteLottieAnimationView), 0, BindingMode.TwoWay);
        public static readonly BindableProperty IsFavoriteProperty = 
            BindableProperty.Create(nameof(IsFavorite), typeof(bool), typeof(FavoriteLottieAnimationView), false, BindingMode.TwoWay,propertyChanged: OnIsFavoriteChanged);


        public int StartOfAnimation
        {
            get { return (int)GetValue(StartOfAnimationProperty); }
            set { SetValue(StartOfAnimationProperty, value); }
        }
        public int EndOfAnimation
        {
            get { return (int)GetValue(EndOfAnimationProperty); }
            set { SetValue(EndOfAnimationProperty, value); }
        }


        //------------------------------------------------------------------
        //
        //  Property is only called when use in code. Bindable property uses 
        //  Set/Get Value for change.
        //
        //------------------------------------------------------------------
        private bool _isFavorite;
        public bool IsFavorite
        {
            get
            {
                _isFavorite = (bool)GetValue(IsFavoriteProperty);
                
                return _isFavorite;
            }
            set
            {
                _isFavorite = value;
                if (_isFavorite)
                    PlayProgressSegment(0, 1);
                else
                    PlayProgressSegment(0, 0);
                SetValue(IsFavoriteProperty, value);
            }
        }

        //static void OnEndOfAnimationChanged(BindableObject bindable , object oldValue, object newValue)
        //{
        //    if (oldValue != newValue)
        //    {
        //        bindable.SetValue(StartOfAnimationProperty,(int)newValue);

        //    }
        //}

        static void OnIsFavoriteChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                var instance = ((FavoriteLottieAnimationView)bindable);

                instance.SetValue(IsFavoriteProperty, (bool)newValue);
                instance.IsFavorite = (bool)newValue;
            }
        }
    }
}
