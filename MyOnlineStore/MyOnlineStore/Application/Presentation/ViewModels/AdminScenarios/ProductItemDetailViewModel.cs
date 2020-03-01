using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Application.Common.Global.Constants;
using MyOnlineStore.Application.Data.DataStore;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Entities.Models.Purchases;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Application.Presentation.Views.Templates.Popups;
using Rg.Plugins.Popup.Services;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using MyOnlineStore.Application.Common.Interfaces.Validations;
using MyOnlineStore.Shared.Interfaces.Factories;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios;

namespace MyOnlineStore.Application.Presentaion.ViewModels.AdminScenarios
{
    //==================================================================================
    //
    // TODO: Take photo isn't implemented. 
    //          -> Implemented in Juan's project
    //
    //==================================================================================

    /// <summary>
    /// Query Properties
    /// StoreId -> Owner of this product [Requiered]
    /// ProductId -> Use for fetching & updating a product [Requiered]
    ///         Configuration: Value:[New] for a new product entry
    ///                        Value:[Guid.Value] for a update of item
    /// IsEditable -> Determines if is editable [Required]
    /// </summary>
    [QueryProperty("StoreId", "storeId")]
    [QueryProperty("ProductId", "productId")]
    [QueryProperty("IsEditable", "isEditable")]
    public class ProductItemDetailViewModel : BaseViewModel, IProductItemDetailViewModel
    {

        #region Properties
        protected readonly IProductItemDataStore _ProductItemDataStore;
        protected readonly IProductItemFactory _ProductFactory;
        protected readonly IValidatableObjectFactory _ValidatableObjectFactory;

        public ICommand GetProductPhotoCommand { get; set; }
        public ICommand TakeProductPhotCommand { get; set; }
        public ICommand AddToInventoryCommand { get; set; }
        public ICommand ScanBarcodeCommand { get; set; }

        IValidateableObject<string> itemName = null!;
        public IValidateableObject<string> ItemName
        {
            get => itemName;
            set { itemName = value; RaisePropertyChanged(() => ItemName); }
        }

        public IValidateableObject<string> Price { get; set; }

        IValidateableObject<string> description = null!;
        public IValidateableObject<string> Description
        {
            get => description;
            set { description = value; RaisePropertyChanged(() => Description); }
        }
        public IValidateableObject<string> Quantity { get; set; }
        public IValidateableObject<string> TypeOfItem { get; set; }

        byte[] _productImageSource = null!;
        public byte[] ProductImageSource
        {
            get { return _productImageSource; }
            set { _productImageSource = value; RaisePropertyChanged(() => ProductImageSource); }
        }

        private string storeId = string.Empty;
        public string StoreId
        {
            get => storeId;
            set { storeId = value; }
        }

        private string productId = string.Empty;
        public string ProductId
        {
            get => productId;
            set
            {
                ProductItem productItem;
                byte[] image;
               
                productId = value;

                if (value != ViewType.DetailView.New.ToString())
                {
                    productItem = _ProductItemDataStore.GetItem(productId);
                }
                else
                {
                    // Get Image Placeholder
                   // Utils.GetByteArrayFromPath(out image, LocalStorage.ImagePlaceHolderPath);
                    productItem = _ProductFactory.CreateSimpleProducItem();
                    //productItem.Image = image;
                }

                ItemName = _ValidatableObjectFactory.CreateSimpleValidatebleObject(productItem.Name);
                Price = _ValidatableObjectFactory.CreateSimpleValidatebleObject(productItem.Price.ToString());
                Description = _ValidatableObjectFactory.CreateSimpleValidatebleObject(productItem.Description);
                Quantity = _ValidatableObjectFactory.CreateSimpleValidatebleObject(productItem.Quantity.ToString());
                TypeOfItem = _ValidatableObjectFactory.CreateSimpleValidatebleObject(productItem.Category);
                ProductImageSource = productItem.Image??null!;
            }
        }

        private string isEditable = bool.FalseString;
        public string IsEditable
        {
            get => isEditable;
            set 
            {
                isEditable = (!bool.Parse(value)).ToString(); 
                RaisePropertyChanged(() => IsEditable); 
            }
        }

        #endregion

        public ProductItemDetailViewModel(IProductItemDataStore productItemDataStore, IProductItemFactory productItemFactory, IValidatableObjectFactory validatableObjectFactory)
        {
            _ProductItemDataStore = productItemDataStore;
            _ProductFactory = productItemFactory;
            _ValidatableObjectFactory = validatableObjectFactory;

            ScanBarcodeCommand = new Command(() => ScanBarcode());
            AddToInventoryCommand = new Command(() => InsertOrUpdateProductItem());
            GetProductPhotoCommand = new Command(async () => ProductImageSource = await Utils.PickPhoto());
            TakeProductPhotCommand = new Command(() => { });

            Init();
        }


        #region Methods
        private void Init()
        {
           
        }

        private async Task PickPhoto()
        {
            Plugin.Media.Abstractions.MediaFile mediaFile;
            System.IO.MemoryStream memoryStream;
            byte[] productImageSource;

            if (Plugin.Media.CrossMedia.Current.IsTakePhotoSupported )
            {
                mediaFile = await Plugin.Media.CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions
                {
                    Directory = "Test",
                    SaveToAlbum = true,
                    CompressionQuality = 75,
                    CustomPhotoSize = 50,
                    PhotoSize = Plugin.Media.Abstractions.PhotoSize.MaxWidthHeight,
                    MaxWidthHeight = 2000,
                    DefaultCamera = Plugin.Media.Abstractions.CameraDevice.Front
                });


                ////Get Root Documents Directory And Add Variable Directory
                //string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                //folderPath = Path.Combine(folderPath, directory);

                ////If Variable Directory Does Not Exist Create It
                //if (!File.Exists(folderPath))
                //    Directory.CreateDirectory(folderPath);

                ////Create New File Path
                //var newPath = Path.Combine(folderPath, Path.GetFileNameWithoutExtension(mediaFile.Path) + 
                //    Path.GetExtension(mediaFile.Path));

                ////If New File Path Exists Delete It  
                //if (File.Exists(newPath))
                //    File.Delete(newPath);

                ////Copy File to New File Path
                //File.Copy(mediaFile.Path, newPath);


                if (mediaFile != null)
                {
                    memoryStream = new System.IO.MemoryStream();
                    mediaFile.GetStream().CopyTo(memoryStream);
                    productImageSource = memoryStream.ToArray();
                    ProductImageSource = productImageSource;
                    mediaFile.Dispose();
                }

            }
            else
            {
                //--------------------------------------
                //
                // TODO: Inject PopupView and display 
                //       message of permission.
                //
                //--------------------------------------
            }

           // return productImageSource;
        }

        private void ClearEntries()
        {
            ItemName = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>();
            Price = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>();
            Description = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>();
            Quantity = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>();
            TypeOfItem = _ValidatableObjectFactory.CreateSimpleValidatebleObject<string>();
           // Utils.GetByteArrayFromPath(out _productImageSource, LocalStorage.ImagePlaceHolderPath);
        }
        private void SuccessPopup(bool success)
        {
            SuccessPopup successPopup;

            if (success)
            {
                successPopup = new SuccessPopup
                {
                    LottieAnimation = "animation_success.json",
                    Message = "Succesfully!"
                };
            }
            else
            {
                successPopup = new SuccessPopup
                {
                    LottieAnimation = "animation_failed.json",
                    Message = "Failed!"
                };
            }

            PopupNavigation.Instance.PushAsync(successPopup).GetAwaiter();
        }
        #endregion

        #region Services

        private void ScanBarcode()
        {
            BarcodeDataStore barcodeDataStore = new BarcodeDataStore();
            var product = barcodeDataStore.GetItem("047400664685");

            if (product.items is object)
            {
                foreach (var item in product.items)
                {
                    ItemName = _ValidatableObjectFactory.CreateSimpleValidatebleObject(item.title);
                    Price = _ValidatableObjectFactory.CreateSimpleValidatebleObject(item.description);
                }
            }
        }

        private async void InsertOrUpdateProductItem()
        {
            bool isUpdateable;
            bool isSuccessfull;
            ProductItem productItemFromDB;
            ProductItem currentInsertingItem;

            currentInsertingItem = _ProductFactory.CreateProductItem(itemName: ItemName.Value,
                price: Price.Value,
                description: Description.Value,
                quantity: Quantity.Value,
                category: TypeOfItem.Value,
                logo: ProductImageSource,
                
                storeId: StoreId);

            productItemFromDB = _ProductItemDataStore.GetItemByNameFromInventory(storeId: StoreId,
                productName: currentInsertingItem.Name);

            isUpdateable = productItemFromDB != null ? true : false;

            if (isUpdateable && productItemFromDB != null)
            {
                productItemFromDB.Quantity += currentInsertingItem.Quantity;
                productItemFromDB.Price = currentInsertingItem.Price;
                productItemFromDB.Category = currentInsertingItem.Category;
                productItemFromDB.Description = currentInsertingItem.Description;

                isSuccessfull = _ProductItemDataStore.UpdateItem(productItemFromDB);
            }
            else
            {
                isSuccessfull = _ProductItemDataStore.AddItem(currentInsertingItem);
            }

            SuccessPopup(isSuccessfull);

            if (isSuccessfull)
            {
                ClearEntries();
                await Shell.Current.GoToAsync($"{InventoryListPage.Route}?" +
                  $"owenerstoreid={App.ApplicationManager.CurrentStore!.Id.ToString()}");
            }
        }

        #endregion
    }
}
