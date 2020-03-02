using MyOnlineStore.Application.Data.Models.Presenters;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Entities.Models.Users;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Models.Purchases;
using MyOnlineStore.Shared.Interfaces.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MyOnlineStore.Application.Presentation.Views.StoresScenarios;
using MyOnlineStore.Application.Presentation.Views.OrdersScenarios;
using System.Collections.ObjectModel;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Data.Presenters;

namespace MyOnlineStore.Application.Presentation.Managers
{
    public class ShoppingManager : ExtendedBindableObject
    {
        private Store currentShoppingStore;
        private uint cartItemsQuantity = uint.MinValue;
        private double cartTotalPrice = 0.0;
        private ObservableCollection<ProductItemPresenter> cart = new ObservableCollection<ProductItemPresenter>();

        private readonly int PlusPlus = 1;
        private readonly int MinusMinus = -1;

        protected IOrderFactory OrderFactory { get; private set; }
        protected IOrdersProductItemsFactory OrdersProductItemsFactory { get; private set; }
        protected IStoreDataStore StoreDataStore { get; private set; }
        protected IOrdersDataStore OrdersDataStore { get; private set; }
        protected IProductItemBuyPresenterFactory ProductItemBuyPresenterFactory { get; private set; }
        public ICommand NavigateToCartCommand { get; set; }
        public ICommand AddProductItemCommand { get; set; }
        public ICommand RemoveProductItemCommand { get; set; }
        /**
         * User's store id are Keys
         */
        public static List<Order> MyStoresOrders { get; private set; }

        /// <summary>
        /// Counter of the Cart
        /// </summary>
        public uint CartItemsQuantity
        {
            get => cartItemsQuantity;
            set { cartItemsQuantity = value; RaisePropertyChanged(() => CartItemsQuantity); }
        }

        /// <summary>
        /// Total Price of the Cart
        /// </summary>
        public double CartTotalPrice
        {
            get => cartTotalPrice;
            set { cartTotalPrice = value; RaisePropertyChanged(() => CartTotalPrice); }
        }

        /// <summary>
        /// Store shopping for
        /// </summary>
        public Store CurrentShoppingStore
        {
            get => currentShoppingStore;
            set
            {
                currentShoppingStore = value;
                AvailableItems = CurrentShoppingStore.ProductItems;
                RaisePropertyChanged(() => CurrentShoppingStore);
            }
        }

        /// <summary>
        /// Order that is being used
        /// </summary>
        public Order CurrentOrder { get; set; }

        // TODO: Connect to recieve signal from backend
        public List<ProductItem> AvailableItems { get; set; }

        /// <summary>
        /// Client's Information to pay
        /// </summary>
        public User ClientUser { get; set; }

        /// <summary>
        /// Cart Items as Presenters
        /// </summary>
        public ObservableCollection<ProductItemPresenter> MyCart
        {
            get => cart;
            set { cart = value; RaisePropertyChanged(() => MyCart); }
        }

        public ShoppingManager(IOrderFactory orderFactory, IOrdersProductItemsFactory ordersProductItemsFactory, IStoreDataStore storeDataStore,IOrdersDataStore ordersDataStore, IProductItemBuyPresenterFactory productItemBuyPresenterFactory)
        {
            OrderFactory = orderFactory;
            OrdersProductItemsFactory = ordersProductItemsFactory;
            StoreDataStore = storeDataStore;
            OrdersDataStore = ordersDataStore;
            ProductItemBuyPresenterFactory = productItemBuyPresenterFactory;

            if(App.ApplicationManager.IsLogged())
            {
                ClientUser = App.ApplicationManager.CurrentUser;
            }

            AddProductItemCommand = new Command<ProductItemPresenter>((item) => AddProductItemToCart(item));
            RemoveProductItemCommand = new Command<ProductItemPresenter>((item) => RemoveProductItemToCart(item));
            NavigateToCartCommand = new Command(async () => await NavigateToCartHandler());
        }

        /// <summary>
        /// Download User's Orders that are pending
        /// </summary>
        /// <returns></returns>
        private async Task DownloadUserCurrentOrders()
        {
            var result = await Task.Run(async ()
                    => await OrdersDataStore.GetClientPendingOrders(ClientUser.Id.ToString())
                );
            MyStoresOrders = new List<Order>(result);
        }

        /// <summary>
        /// Check if has a pending order for this Current Store
        /// </summary>
        /// <returns></returns>
        private bool HasOrderForThisStore()
        {
            return MyStoresOrders.Where(storeId => storeId.MyStoreId == CurrentShoppingStore.Id).ToList().Count >= 1;
        }

        /// <summary>
        /// Creates or uses a existing order and creates User's Cart
        /// </summary>
        /// <returns></returns>
        public async Task PrepareOrder()
        {
            DownloadUserCurrentOrders().Wait();

            if (!HasOrderForThisStore())
            {
                CurrentOrder = OrderFactory.CreateOrderForStore(CurrentShoppingStore, ClientUser);
                MyStoresOrders.Add(CurrentOrder);
                await OrdersDataStore.CreateOrder(OrderFactory.CreatNewOrder(CurrentOrder));
            }
            else
            {
                CurrentOrder = MyStoresOrders.Find(order => order.MyStoreId == CurrentShoppingStore.Id);

                if (CurrentOrder.OrderItems is object)
                {
                    await FillCart(CurrentOrder.OrderItems);
                }
                else
                {
                    CurrentOrder.OrderItems = OrdersProductItemsFactory.CreateEmptyCart();
                }

            }
        }

        /// <summary>
        /// Create Cart List for the current Store
        /// </summary>
        /// <param name="orderItems">List of items in order </param>
        /// <returns></returns>
        private async Task FillCart(IEnumerable<OrdersProductItems> orderItems)
        {
            MyCart = new ObservableCollection<ProductItemPresenter>();
            await Task.Run(() =>
            {
                if (orderItems is object)
                {
                    foreach (var item in orderItems)
                    {
                        var itemPresenter = ProductItemBuyPresenterFactory.CreateProductBuyPresenterWithOffer(
                                product: item.ProductItem,
                                selectQuantity: item.QuantityOfItem,
                                totalprice: item.ProductItem.Price * item.QuantityOfItem
                            );
                        MyCart.Add(itemPresenter);
                    }
                }

                CartTotalPrice = MyCart.Sum(ordersItems => ordersItems.TotalPriceOfSelectedItems);
            });

        }

        /// <summary>
        /// Update Cart Counter and Total Price of the Order
        /// </summary>
        public void UpdateCartNumbers()
        {
            //Update Cart Quantity
            CartItemsQuantity = (uint)CurrentOrder.OrderItems.Sum(orderItem => orderItem.QuantityOfItem);

            //Update Total Price of Cart
            //CartTotalPrice = CurrentOrder.OrderItems.Sum(ordersitems =>
            //    ordersitems.ProductItem.Price * ordersitems.QuantityOfItem
            //);

            CartTotalPrice = CurrentOrder.OrderItems.Sum(orderitems => {
                double sum = double.NaN;

                if (orderitems.ProductItem.HasOffer())
                {
                    sum = orderitems.ProductItem.ProductOffer!.OfferPrice * orderitems.QuantityOfItem;
                }
                else
                {
                    sum = orderitems.ProductItem.Price * orderitems.QuantityOfItem;
                }

                return sum;
            });

        }

        /// <summary>
        /// Update the quantity of an item
        /// </summary>
        /// <param name="orderProductItem"></param>
        /// <param name="sum"></param>
        private void UpdateOrderProductItem(OrdersProductItems orderProductItem, int sum)
        {
            //Update Quantity of this specific item
            orderProductItem.QuantityOfItem += (uint)sum;
        }
       
        /// <summary>
        /// Update ProductPresenter/Item model's 
        /// - inventory quantity
        /// - selected quantity for item
        /// - total price for the item
        /// </summary>
        /// <param name="productItemPresenter"></param>
        /// <param name="sum"></param>
        private void UpdateProductPresenter(ProductItemPresenter productItemPresenter,int sum)
        {
            //Update Inventory Quantity
            productItemPresenter.Quantity+= (uint)(-Math.Abs(sum));

            //Update selected item quantity stepper
            productItemPresenter.SelectedItemCount+=(uint)sum;

            // TODO: Calculate price with offers price
            //Update Product Item Total Price
            productItemPresenter.TotalPriceOfSelectedItems = (float)productItemPresenter.Price * productItemPresenter.SelectedItemCount;
        }

        /// <summary>
        /// Update order in DB, Order quantities, Product Presenter & Cart
        /// </summary>
        /// <param name="orderProductItem"></param>
        /// <param name="productItemPresenter"></param>
        /// <param name="sum"></param>
        public void Update(OrdersProductItems orderProductItem, ProductItemPresenter productItemPresenter, int sum)
        {
            Task.Run(async () => await OrdersDataStore.UpdateOrder(CurrentOrder));
            
            UpdateOrderProductItem(orderProductItem, sum);
            UpdateProductPresenter(productItemPresenter, sum);
            UpdateCartNumbers();

        }

        /// <summary>
        /// Remove item from current order
        /// Occurs when selected item count is 0 or the complete deletion of items from order
        /// </summary>
        /// <param name="orderProductItem"></param>
        /// <param name="productItemPresenter"></param>
        private void RemoveItemFromOrder(OrdersProductItems orderProductItem, ProductItemPresenter productItemPresenter)
        {
            if (CurrentOrder.OrderItems is object)
            {
                // Remove from Order Items
                CurrentOrder.OrderItems.RemoveAll(op => op.ProductItemId == orderProductItem.ProductItemId);
                // Remove from
                MyCart.Remove(productItemPresenter);

                // Remove from DB
                Task.Run(async () =>
                    await OrdersDataStore.RemoveItemFromOrder(
                        orderId: orderProductItem.OrderId.ToString(),
                        productId: orderProductItem.ProductItemId.ToString()
                    )
                );
            }
        }

        /// <summary>
        /// Validation for adding to cart
        /// </summary>
        /// <param name="productItemPresenter"></param>
        /// <returns></returns>
        private bool AddValidateProductPresenter(ProductItemPresenter productItemPresenter)
        {
            return productItemPresenter is object
                && productItemPresenter.Id != null
                && productItemPresenter.Id != Guid.Empty
                && productItemPresenter.Quantity > productItemPresenter.SelectedItemCount;
        }

        /// <summary>
        /// Validation for removing of cart
        /// </summary>
        /// <param name="productItemPresenter"></param>
        /// <returns></returns>
        private bool RemoveValidateProductPresenter(ProductItemPresenter productItemPresenter)
        {
            return productItemPresenter is object
                && productItemPresenter.Id != null
                && productItemPresenter.Id != Guid.Empty;
        }

        /// <summary>
        /// Check if item exist in the current order
        /// </summary>
        /// <param name="productItemPresenter"></param>
        /// <returns></returns>
        private bool OrderContainsItem(ProductItemPresenter productItemPresenter)
        {
            return CurrentOrder.OrderItems.Select(orderItem => orderItem.ProductItem.Id).Contains(productItemPresenter.Id);
        }

        /// <summary>
        /// Add or create an item to the current Order's Item
        /// </summary>
        /// <param name="product"></param>
        private void AddProductItemToCart(ProductItemPresenter product)
        {
            ProductItemPresenter productItemPresenter = product;
            OrdersProductItems orderProductItem;

            if (AddValidateProductPresenter(productItemPresenter) && CurrentOrder.OrderItems is object)
            {
                // Get OrderProductItem
                orderProductItem = CurrentOrder.OrderItems
                    .Where(orderItem => orderItem.ProductItemId == productItemPresenter.Id).FirstOrDefault();

                //Check Order has this product item if not create and add to order
                if (!OrderContainsItem(productItemPresenter))
                {
                    //Create OrderProductItem
                    orderProductItem = OrdersProductItemsFactory.CreateSimpleOrderProduct(
                        order: CurrentOrder, 
                        productItem: productItemPresenter.ToProductItem(),
                        quantity: productItemPresenter.SelectedItemCount);

                    //Add OrderProductItem
                    CurrentOrder.OrderItems.Add(orderProductItem);
                    MyCart.Add(productItemPresenter);
                }
                
                Update(orderProductItem, productItemPresenter,PlusPlus);
            }
        }

        /// <summary>
        /// Check if a item is ready to be remove from Order's Item List
        /// </summary>
        /// <param name="ordersProductItems"></param>
        /// <returns></returns>
        private bool IsRemoveable(OrdersProductItems ordersProductItems)
        {
            return ordersProductItems.QuantityOfItem < 2;
        }

        /// <summary>
        /// Remove or substract an item  from current order's item and cart
        /// </summary>
        /// <param name="product"></param>
        private void RemoveProductItemToCart(ProductItemPresenter product)
        {
            ProductItemPresenter productItemPresenter = product;
            OrdersProductItems orderProductItem;

            if (RemoveValidateProductPresenter(productItemPresenter) && OrderContainsItem(productItemPresenter)
                && CurrentOrder.OrderItems is object)
            {
                //Get OrderProductItem
                orderProductItem = CurrentOrder.OrderItems
                    .Where(o => o.ProductItemId == productItemPresenter.Id)
                    .FirstOrDefault();

                if (IsRemoveable(orderProductItem))
                {
                    RemoveItemFromOrder(orderProductItem, productItemPresenter);
                }

                Update(orderProductItem, productItemPresenter, MinusMinus);
            }
        }

        /// <summary>
        /// Remove an item from current Order's item
        /// </summary>
        /// <param name="item"></param>
        public void RemoveAllProductsFromOrder(ProductItemPresenter item)
        {
            ProductItemPresenter productItemPresenter = item;
            OrdersProductItems orderProductItem;

            if (RemoveValidateProductPresenter(productItemPresenter) && OrderContainsItem(productItemPresenter) 
                && CurrentOrder.OrderItems is object)
            {
                //Get OrderProductItem
                orderProductItem = CurrentOrder.OrderItems
                    .Where(o => o.ProductItemId == productItemPresenter.Id)
                    .FirstOrDefault();

                RemoveItemFromOrder(orderProductItem, productItemPresenter);

                Update(orderProductItem, productItemPresenter, MinusMinus);
            }
        }

        /// <summary>
        /// NAvigates to cart
        /// </summary>
        /// <returns></returns>
        private async Task NavigateToCartHandler()
        {
            await Device.InvokeOnMainThreadAsync(async () =>
            {
                await Shell.Current.GoToAsync($"{CheckoutListPage.Route}",
                       animate: true);
            });
        }
    }
}
