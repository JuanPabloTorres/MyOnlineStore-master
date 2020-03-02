using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Presentation.ViewModels.StoresScenarios;
using MyOnlineStore.Entities.Models.Purchases;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;
using MyOnlineStore.Application.Presentation.Managers;
using MyOnlineStore.Entities.Models.Users;
using System.Windows.Input;
using System.Threading.Tasks;
using MyOnlineStore.Application.Data.Presenters;

namespace MyOnlineStore.Application.Presentation.ViewModels.OrdersScenarios
{
    [QueryProperty("Order", "order")]
    public class CheckoutViewModel : Base.BaseViewModel, ICheckoutViewModel
    {
        private ObservableCollection<ProductItemPresenter> myCartProducts = 
            new ObservableCollection<ProductItemPresenter>();

        public ShoppingManager ShoppingManager { get; set; }
        public float Total { get; set; } = 0.0f;
        public ObservableCollection<ProductItemPresenter> MyCartProducts
        {
            get => myCartProducts;
            set { myCartProducts = value; RaisePropertyChanged(() => MyCartProducts); }
        }
        public User CurrentUser { get; set; }
        public ICommand PayCommand { get; set; }
        public ICommand DeleteItemFromOrderCommand { get; set; }
        public CheckoutViewModel(ShoppingManager shoppingManager)
        {
            ShoppingManager = shoppingManager;

            //Init();

            PayCommand = new Command(async () => await PayHandler());
            DeleteItemFromOrderCommand = new Command<ProductItemPresenter>((item) 
                => DeleteItemFromOrderHandler(item)
            );

            if (App.ApplicationManager.IsLogged(logoutProcedure: true)
                && App.ApplicationManager.CurrentUser is User client)
            {
                CurrentUser = client;
            }
            
        }

        private void Init()
        {
            var order = ShoppingManager.MyStoresOrders.Find(order => order.MyStoreId == ShoppingManager.CurrentShoppingStore.Id);

            MyCartProducts = new ObservableCollection<ProductItemPresenter>();

            if (order.OrderItems is object)
            {
                foreach (var item in order.OrderItems)
                {
                    var itemPresenter = Startup.ServiceProvider
                        .GetService<IProductItemBuyPresenterFactory>()
                        .CreateProductBuyPresenterWithOffer(
                            product: item.ProductItem,
                            selectQuantity: item.QuantityOfItem,
                            totalprice: item.ProductItem.Price * item.QuantityOfItem
                        );
                    MyCartProducts.Add(itemPresenter);
                }
            }

            Total = MyCartProducts.Sum(ordersItems => ordersItems.TotalPriceOfSelectedItems);
        }

        private void DeleteItemFromOrderHandler(ProductItemPresenter item)
        {
            ShoppingManager.RemoveAllProductsFromOrder(item);
            Init();
        }

        private async Task PayHandler()
        {
            await Task.Run(async()=>{ });
        }
    }
}
