using MyOnlineStore.Entities.Models.Stores;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyOnlineStore.Application.Presentation.Views.StoresScenarios
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StoreContentCarouselView : ContentView
    {
        public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
            propertyName: nameof(Items),
            returnType: typeof(IEnumerable<StoreFeaturedItem>),
            declaringType: typeof(StoreContentCarouselView),
            defaultValue: null,
            defaultBindingMode: BindingMode.TwoWay,
            propertyChanged: OnItemsChanged);
        static void OnItemsChanged(BindableObject bindableObject, object newValue, object oldValue)
        {
            if (true)
            {
                var x = bindableObject;
            }
        }
        public IEnumerable<StoreFeaturedItem>  Items
        {
            get => (IEnumerable<StoreFeaturedItem>)GetValue(ItemsProperty);
            set => SetValue(ItemsProperty, value);
        }
        public StoreContentCarouselView()
        {
            InitializeComponent();
        }
    }
}