using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Data.Presenters;
using MyOnlineStore.Application.Presentation.Managers;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MyOnlineStore.Application.Presentation.ViewModels.StoresScenarios
{
    public class StoreShopViewModel : BaseViewModel, IStoreShopViewModel
    {
        private ObservableCollection<ProductItemPresenter> productItemPresenters = new ObservableCollection<ProductItemPresenter>();

        protected IProductItemBuyPresenterFactory ProductsPresenterFactory { get; private set; }

        public ObservableCollection<ProductItemPresenter> Products
        {
            get => productItemPresenters;
            set
            {
                productItemPresenters = value;
                RaisePropertyChanged(() => Products);
            }
        }

        public ShoppingManager ShoppingManager { get; set; }

        public ICommand AddProductItemCommand { get; set; }

        public ICommand RemoveProductItemCommand { get; set; }
        
        public StoreShopViewModel(ShoppingManager shoppingManager, IProductItemBuyPresenterFactory itemBuyPresenterFactory)
        {
            ShoppingManager = shoppingManager;
            ProductsPresenterFactory = itemBuyPresenterFactory;
            Init();
        }

        private void Init()
        {
            //TODO: Recieve a signal of quantity
            //API will send a signal with an updated products quantity
            //ShoppingManager.AvailableItems.ForEach((product) => 
            //{
            //    Products.Add(ProductsPresenterFactory.CreateProductBuyPresenterWithOffer(product));
            //});

            foreach (var item in ShoppingManager.AvailableItems)
            {
                Products.Add(ProductsPresenterFactory.CreateProductBuyPresenterWithOffer(item));
            }
            ShoppingManager.MyCart = new ObservableCollection<ProductItemPresenter>(Products);
            ShoppingManager.UpdateCartNumbers();
        }
    }
}
