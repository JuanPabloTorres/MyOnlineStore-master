using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.Templates.Badges
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class BadgeView : ContentView
    {
        //---------------------------------------------------------------------------------------------------------
        // BadgeColor
        //
        public static readonly BindableProperty BadgeColorProperty = BindableProperty.Create(
                propertyName: nameof(BadgeColor),
                returnType: typeof(Color),
                declaringType: typeof(BadgeView),
                defaultValue: Color.Red,
                defaultBindingMode: BindingMode.Default
            );
        public Color BadgeColor
        {
            get => (Color)GetValue(BadgeColorProperty);
            set
            {
                SetValue(BadgeColorProperty, value);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // TextColor
        //
        private static readonly BindableProperty TextColorProperty = BindableProperty.Create(
                propertyName: nameof(TextColor), 
                returnType: typeof(Color),
                declaringType: typeof(BadgeView),
                defaultValue: Color.Black,
                defaultBindingMode: BindingMode.Default
            );
        public Color TextColor
        {
            get => (Color)GetValue(TextColorProperty);
            set
            {
                SetValue(TextColorProperty, value);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // CounterText
        //
        public static readonly BindableProperty CounterTextProperty = BindableProperty.Create(
                propertyName: nameof(CounterText),
                returnType: typeof(string),
                declaringType: typeof(BadgeView),
                defaultValue: "0",
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnCounterTextChanged
            );
        public string CounterText
        {
            get => (string)GetValue(CounterTextProperty);
            set
            {
                uint counter = uint.Parse(value);

                if (counter > 0)
                    HasCounter = true;
                else
                    HasCounter = false;

                if (counter.ToString().Length == CounterSizes.ThreeDigitLength)
                {
                    CounterSize = CounterSizes.ThreeDigitCollumnSpanSize;
                    StartingColumn = CounterSizes.StartingColumnOneTwoThreeDigitLength;
                }
                else if (counter.ToString().Length == CounterSizes.FourDigitLength)
                {
                    CounterSize = CounterSizes.FourDigitCollumnSpanSize;
                    StartingColumn = CounterSizes.StartingColumnFourDigitLength;
                }
                else
                {
                    CounterSize = CounterSizes.OneTwoDigitCollumnSpanSize;
                    StartingColumn = CounterSizes.StartingColumnOneTwoThreeDigitLength;
                }

                SetValue(CounterTextProperty, value);
            }
        }
        private static void OnCounterTextChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is BadgeView instance)
            {
                instance.CounterText = newValue.ToString();
                instance.SetValue(CounterSizeProperty, instance.CounterSize);
            }  
        }

        //---------------------------------------------------------------------------------------------------------
        // HasCounter
        //
        private static readonly BindableProperty HasCounterProperty = BindableProperty.Create(
              propertyName: nameof(HasCounter),
              returnType: typeof(bool),
              declaringType: typeof(BadgeView),
              defaultValue: false,
              defaultBindingMode: BindingMode.Default,
              propertyChanged: OnHasCounterChanged
          );
        public bool HasCounter
        {
            get => (bool)GetValue(HasCounterProperty);
            protected set
            {
                SetValue(HasCounterProperty, value);
            }
        }
        private static void OnHasCounterChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is BadgeView instance)
            {
                instance.SetValue(HasCounterProperty, instance.HasCounter);
            }
        }

        //---------------------------------------------------------------------------------------------------------
        // CounterSize
        //
        protected static class CounterSizes
        {
            public static int OneTwoDigitCollumnSpanSize = 1;
            public static int ThreeDigitCollumnSpanSize = 2;
            public static int FourDigitCollumnSpanSize = 3;

            public static int ThreeDigitLength = 3;
            public static int FourDigitLength = 4;

            public static int StartingColumnOneTwoThreeDigitLength = 3;
            public static int StartingColumnFourDigitLength = 2;
        }

        private static readonly BindableProperty CounterSizeProperty = BindableProperty.Create(
             propertyName: nameof(CounterSize),
             returnType: typeof(int),
             declaringType: typeof(BadgeView),
             defaultValue: 1,
             defaultBindingMode: BindingMode.Default
         );
        public int CounterSize
        {
            get
            {
                return (int)GetValue(CounterSizeProperty);
            }
            protected set
            {
                SetValue(CounterSizeProperty, value);
            }
        }

        private static readonly BindableProperty StartingColumnProperty = BindableProperty.Create(
             propertyName: nameof(StartingColumn),
             returnType: typeof(int),
             declaringType: typeof(BadgeView),
             defaultValue: CounterSizes.StartingColumnOneTwoThreeDigitLength,
             defaultBindingMode: BindingMode.TwoWay
         );
        public int StartingColumn
        {
            get => (int)GetValue(StartingColumnProperty);
            protected set => SetValue(StartingColumnProperty, value);
        }
        //---------------------------------------------------------------------------------------------------------

        public BadgeView()
        {
            InitializeComponent();
        }
    }
}