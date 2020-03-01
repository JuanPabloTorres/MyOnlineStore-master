using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Application.Presentation.PresentationHelpers.Converters;
using System.Globalization;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.Templates.StepperView
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StepperView : ContentView
    {
        //-----------------------------------
        // Plus Click
        //
        public static readonly BindableProperty PlusClickCommandProperty = BindableProperty.Create(
            propertyName: nameof(PlusClickCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(StepperView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);
        public ICommand PlusClickCommand
        {
            get => (ICommand)GetValue(PlusClickCommandProperty);
            set => SetValue(PlusClickCommandProperty, value);
        }

        //-----------------------------------
        // Plus Click Parameter
        //
        private static readonly BindableProperty PlusClickCommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(PlusClickCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(StepperView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);
        public object PlusClickCommandParameter
        {
            get => GetValue(PlusClickCommandParameterProperty);
            set => SetValue(PlusClickCommandParameterProperty, value);
        }

        //-----------------------------------
        // Minus Click
        //
        public static readonly BindableProperty MinusClickCommandProperty = BindableProperty.Create(
            propertyName: nameof(MinusClickCommand),
            returnType: typeof(ICommand),
            declaringType: typeof(StepperView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);
        public ICommand MinusClickCommand
        {
            get => (ICommand)GetValue(MinusClickCommandProperty);
            set => SetValue(MinusClickCommandProperty, value);
        }

        //-----------------------------------
        // Minus Click Parameter
        //
        private static readonly BindableProperty MinusClickCommandParameterProperty = BindableProperty.Create(
            propertyName: nameof(MinusClickCommandParameter),
            returnType: typeof(object),
            declaringType: typeof(StepperView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);
        public object MinusClickCommandParameter
        {
            get => GetValue(MinusClickCommandParameterProperty);
            set => SetValue(MinusClickCommandParameterProperty, value);
        }

        //-----------------------------------
        // Counter
        //
        public static readonly BindableProperty CounterProperty = BindableProperty.Create(
            propertyName: nameof(Counter),
            returnType: typeof(uint),
            declaringType: typeof(StepperView),
            defaultValue: uint.MinValue,
            defaultBindingMode: BindingMode.TwoWay);
        public uint Counter
        {
            get => (uint)GetValue(CounterProperty);
            set => SetValue(CounterProperty, value);
        }

        //-----------------------------------
        // Converter
        //
        private static readonly BindableProperty ParameterConverterProperty = BindableProperty.Create(
            propertyName: nameof(ParameterConverter),
            returnType: typeof(IValueConverter),
            declaringType: typeof(StepperView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay);
        public IValueConverter ParameterConverter
        {
            get => (IValueConverter)GetValue(ParameterConverterProperty);
            set => SetValue(ParameterConverterProperty, value);
        }

        public StepperView()
        {
            InitializeComponent();
        }

        private void ImageButton_Clicked_Plus(object sender, System.EventArgs e)
        {
            if (PlusClickCommand.CanExecute(BindingContext))
            {
                if (ParameterConverter !=null)
                {
                    var parameter = ParameterConverter.Convert(BindingContext, typeof(object), null, CultureInfo.CurrentUICulture);
                    
                    PlusClickCommand.Execute(parameter);
                }
            }
        }

        private void ImageButton_Clicked_Minus(object sender, System.EventArgs e)
        {
            if (MinusClickCommand.CanExecute(BindingContext))
            {
                if (Counter < 1)
                    return;

                if (ParameterConverter != null)
                {
                    var parameter = ParameterConverter.Convert(BindingContext, typeof(object), null, CultureInfo.CurrentUICulture);
                    MinusClickCommand.Execute(parameter);
                }
            }
        }
    }
}