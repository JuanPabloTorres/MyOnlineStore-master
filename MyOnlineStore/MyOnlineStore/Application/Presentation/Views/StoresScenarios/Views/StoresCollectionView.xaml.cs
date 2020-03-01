using MyOnlineStore.Application.Common.Utilities;
using System.Collections.Generic;
using Xamarin.Forms;
using MyOnlineStore.Application.Data.Models.Presenters;

namespace MyOnlineStore.Application.Presentation.Views.StoresScenarios.Views
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class StoresCollectionView : ContentView
    {
        public static readonly BindableProperty StoreListProperty = BindableProperty.Create(
                propertyName: nameof(StoreList),
                returnType: typeof(IEnumerable<StorePresenter>),
                declaringType: typeof(StoresCollectionView),
                defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay,
                propertyChanged: OnStoreListChanged
            );

        private IEnumerable<StorePresenter>? _storelist = null;
        public IEnumerable<StorePresenter> StoreList
        {
            get
            {
                _storelist = (IEnumerable<StorePresenter>)GetValue(StoreListProperty);
                return _storelist;
            }
            set
            {
                _storelist = value;
                SetValue(StoreListProperty, value);
            }
        }

        public static readonly BindableProperty ToggleFavoriteCommandProperty = BindableProperty.Create(
                propertyName: nameof(ToggleFavoriteCommand),
                returnType: typeof(Command<CommandEventData>),
                declaringType: typeof(StoresCollectionView),
                defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay
            );

        private Command<CommandEventData>? _toggleFavoriteCommand = null;
        public Command<CommandEventData> ToggleFavoriteCommand
        {
            get
            {
                _toggleFavoriteCommand = (Command<CommandEventData>)GetValue(ToggleFavoriteCommandProperty) ;
                return _toggleFavoriteCommand;
            }
            set
            {
                _toggleFavoriteCommand = value;
                SetValue(ToggleFavoriteCommandProperty, value);
            }
        }

        public static readonly BindableProperty SectionTitleProperty = BindableProperty.Create(
                propertyName: nameof(SectionTitle), 
                returnType: typeof(string), 
                declaringType: typeof(StoresCollectionView), 
                defaultValue: string.Empty, 
                defaultBindingMode: BindingMode.TwoWay
            );

        public string SectionTitle
        {
            get { return (string)GetValue(SectionTitleProperty); }
            set { SetValue(SectionTitleProperty, value); }
        }

        public static readonly BindableProperty NavigateToStoreDetailCommandProperty = BindableProperty.Create(
                propertyName: nameof(NavigateToStoreDetailCommand),
                returnType: typeof(Command<CommandEventData>),
                declaringType: typeof(StoresCollectionView),
                defaultValue: null,
                defaultBindingMode: BindingMode.TwoWay
            );

        private Command<CommandEventData>? _navigateToStoreDetailCommand = null;
        public Command<CommandEventData> NavigateToStoreDetailCommand
        {
            get
            {
                _navigateToStoreDetailCommand =
                    (Command<CommandEventData>)GetValue(NavigateToStoreDetailCommandProperty);
                return _navigateToStoreDetailCommand;
            }
            set
            {
                _navigateToStoreDetailCommand = value;
                SetValue(NavigateToStoreDetailCommandProperty, value);
            }
        }

        public StoresCollectionView()
        {
            InitializeComponent();
        }

        static void OnStoreListChanged(BindableObject bindable , object oldValue, object newValue)
        {
            if (bindable is StoresCollectionView instance && newValue is IEnumerable<StorePresenter> value)
            {
                instance.SetValue(StoreListProperty, value);
                instance.StoreList = value;
            }
        }

        //private void listview_QueryItemSize(object sender, Syncfusion.ListView.XForms.QueryItemSizeEventArgs e)
        //{
        //    e.
        //}
    }
}