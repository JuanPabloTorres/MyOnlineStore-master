namespace MyOnlineStore.ViewModels.Admin
{
    using MyOnlineStore.Application.Common.Interfaces.DataStore;
    using MyOnlineStore.Application.Common.Interfaces.Validations;
    using MyOnlineStore.Application.Common.Utilities;
    using MyOnlineStore.Application.Common.Validations;
    using MyOnlineStore.Application.Common.Validations.Rules;
    using MyOnlineStore.Application.Domain.Interfaces.IViewModel;
    using MyOnlineStore.Application.Presentation.ViewModels.Base;
    using MyOnlineStore.Application.Presentation.Views.AdminScenarios;
    using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.OfferStepView;
    using MyOnlineStore.Entities.Models.Purchases;
    using MyOnlineStore.Shared.Models.Purchases;
    using Syncfusion.SfCalendar.XForms;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Xamarin.Forms;

    /*
     Todo:
         
         */
    /// <summary>
    /// Defines the <see cref="OfferContentViewModel" />
    /// </summary>
    /// 
    [QueryProperty("StoreID", "storeID")]
    public class OfferContentViewModel : BaseViewModel, IOfferViewModel
    {

        private string storeId;

        public string StoreID
        {
            get { return storeId; }
            set 
            {
                storeId = value; 

                if(!string.IsNullOrEmpty(storeId))
                {
                    var datastore = this.productItemDataStore.GetOfferOfStore(storeId);
                    Offers = new ObservableCollection<Offer>(datastore);

                    var storeInventory = this.productItemDataStore.GetInventoryForStore(storeId);
                    StoreItems = new ObservableCollection<ProductItem>(storeInventory);
                }

            }
        }


        private ObservableCollection<Offer> offers;

        public ObservableCollection<Offer> Offers
        {
            get { return offers; }
            set { offers = value;
                RaisePropertyChanged(() => Offers);
            }
        }

        public ICommand StartMakeOffertCommand { get; set; }

        public ICommand GoTitleAndDescriptionCommand { get; set; }

        public ICommand NextCommand { get; set; }

        public ICommand SetOffertCommand { get; set; }

        public ICommand DoneCommand { get; set; }

        public ICommand DeleteCommand { get; set; }

        public static Offer OfferInformation { get; set; }
      
        public ObservableCollection<ProductItem> StoreItems { get; set; }

      
        internal IProductItemDataStore productItemDataStore { get; set; }

      
        private IValidateableObject<string> validationtitle;

     
        public IValidateableObject<string> ValidationOfferTitle
        {
            get => validationtitle;
            set
            {
                validationtitle = value;

                RaisePropertyChanged(() => ValidationOfferTitle);
            }
        }

     
        private string titleerror;

       
        public string TitleError
        {
            get { return titleerror; }
            set
            {
                titleerror = value;
                RaisePropertyChanged(() => TitleError);
            }
        }

     
        private bool isShowerror;

     
        public bool IsShowError
        {
            get { return isShowerror; }
            set
            {
                isShowerror = value;
                RaisePropertyChanged(() => IsShowError);
            }
        }

      
        private IValidateableObject<string> validationdescription;

      
        public IValidateableObject<string> ValidationDesciption
        {
            get { return validationdescription; }
            set
            {
                validationdescription = value;
                RaisePropertyChanged(() => ValidationDesciption);
            }
        }

     
        private string descriptionerror;

      
        public string DescriptionError
        {
            get { return descriptionerror; }
            set
            {
                descriptionerror = value;
                RaisePropertyChanged(() => DescriptionError);
            }
        }

     
        private bool isShowerrordescription;

     
        public bool IsShowErrorDescription
        {
            get { return isShowerrordescription; }
            set
            {
                isShowerrordescription = value;
                RaisePropertyChanged(() => IsShowErrorDescription);
            }
        }

     
        internal ValidateableObject<double> buyquantityvalidation;

      
        public ValidateableObject<double> BuyQuantityValidation
        {
            get => buyquantityvalidation;

            set
            {
                buyquantityvalidation = value;
                RaisePropertyChanged(() => BuyQuantityValidation);
            }
        }
      
        private string buyquantityerrormsg;

     
        public string BuyQuantityErrorMsg
        {
            get { return buyquantityerrormsg; }
            set
            {
                buyquantityerrormsg = value;
                RaisePropertyChanged(() => BuyQuantityErrorMsg);
            }
        }

      
        private bool showbuyquantityerror;

       
        public bool ShowBuyQuantityError
        {
            get { return showbuyquantityerror; }
            set
            {
                showbuyquantityerror = value;
                RaisePropertyChanged(() => ShowBuyQuantityError);
            }
        }

       
        internal ValidateableObject<double> totalpricevalidation;

     
        public ValidateableObject<double> TotalPriceValidation
        {
            get => totalpricevalidation;

            set
            {
                totalpricevalidation = value;
                RaisePropertyChanged(() => TotalPriceValidation);
            }
        }

      
        private string totalpriceerrormsg;

     
        public string TotalPriceErrorMsg
        {
            get { return totalpriceerrormsg; }
            set
            {
                totalpriceerrormsg = value;
                RaisePropertyChanged(() => TotalPriceErrorMsg);
            }
        }

       
        private bool showerrortotalprice;

       
        public bool ShowErrorTotalPrice
        {
            get { return showerrortotalprice; }
            set
            {
                showerrortotalprice = value;
                RaisePropertyChanged(() => ShowErrorTotalPrice);
            }
        }
      
        internal ValidateableObject<DateTime> datetimevalidation;

     
        public ValidateableObject<DateTime> DateTimeValidation
        {
            get => datetimevalidation;

            set
            {
                datetimevalidation = value;
                RaisePropertyChanged(() => DateTimeValidation);
            }
        }

      
        private string dateerrormsg;

      
        public string DateErrorMsg
        {
            get { return dateerrormsg; }
            set
            {
                dateerrormsg = value;
                RaisePropertyChanged(() => DateErrorMsg);
            }
        }     
        private bool showdateerror;
      
        public bool ShowDateError
        {
            get { return showdateerror; }
            set
            {
                showdateerror = value;
                RaisePropertyChanged(() => ShowDateError);
            }
        }
     
        public ICommand CreateOfferCommand { get; set; }

      
        private ProductItem selectedproduct;

       
        public ProductItem SelectedProduct
        {
            get { return selectedproduct; }
            set
            {
              

                selectedproduct = value;

                ItemImage = ImageSource.FromStream(() => new MemoryStream(SelectedProduct.Image));

                RaisePropertyChanged(() => SelectedProduct);

                  
              
            }
        }

      
        private Offer offer;

      
        public Offer AOffer
        {
            get { return offer; }
            set
            {
                offer = value;
                OnPropertyChanged("AOffer");
            }
        }

       
        private double itemvalue;
      
        public double ItemValue
        {
            get { return itemvalue; }
            set
            {
                itemvalue = value;
                RaisePropertyChanged(() => ItemValue);
            }
        }

      
        internal SelectionRange offerdate;

     
        public SelectionRange OfferDate
        {
            get => offerdate;
            set
            {
                offerdate = value;
                RaisePropertyChanged(() => OfferDate);
            }
        }

      
        private ImageSource itemiimage;

      
        public ImageSource ItemImage
        {
            get { return itemiimage; }
            set
            {
                itemiimage = value;
                RaisePropertyChanged(() => ItemImage);
            }
        }

        private double pricebyone;

        public double PriceByOne
        {
            get { return pricebyone; }
            set 
            {
                pricebyone = value;
                RaisePropertyChanged(() => PriceByOne);
            }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set 
            { 
                title = value;
                RaisePropertyChanged(() => Title);
            }
        }

        private string description;

        public string Description
        {
            get { return description; }
            set 
            { 
                description = value;
                RaisePropertyChanged(() => Description);
            }
        }

        private int buyquantiy;

        public int BuyQuantity
        {
            get { return buyquantiy; }
            set 
            {
                buyquantiy = value;
                RaisePropertyChanged(() => BuyQuantity);
            }
        }

        private double totalprice;

        public double TotalPrice
        {
            get { return totalprice; }
            set { 
                totalprice = value;
                RaisePropertyChanged(() => TotalPrice);

                PriceByOne = TotalPrice / BuyQuantity;


                var percentvalue= ((TotalPrice - PriceByOne) / TotalPrice) * 100;
                ItemPercent = (double)System.Math.Round(percentvalue, 2);

            }
        }

        public double ItemPercent { get; set; }

        public OfferContentViewModel(IProductItemDataStore productItemDataStore)
        {
            this.productItemDataStore = productItemDataStore;

            OfferDate = new SelectionRange();

            OfferDate.StartDate = DateTime.Today;
            OfferDate.EndDate = DateTime.Today;

            SelectedProduct = new ProductItem();
            SelectedProduct.Price = 0.00f;

            ValidationOfferTitle = new ValidateableObject<string>();
            ValidationDesciption = new ValidateableObject<string>();
            BuyQuantityValidation = new ValidateableObject<double>();
            TotalPriceValidation = new ValidateableObject<double>();
            DateTimeValidation = new ValidateableObject<DateTime>();

            AddValidations();

            StartMakeOffertCommand = new Command<CommandEventData>(async (data) => 
            {


                var eventArgs = data.Args as Syncfusion.ListView.XForms.SwipeEndedEventArgs;

                var item = eventArgs.ItemData as ProductItem;


                if (eventArgs.SwipeDirection == Syncfusion.ListView.XForms.SwipeDirection.Right)
                {
                    if (eventArgs.SwipeOffset >= 100)
                    {
                        if (item != null)
                        {
                            SelectedProduct = item;

                            var producthasoffer = productItemDataStore.ProductHasOffer(SelectedProduct.Id.ToString(), StoreID);

                            if (!producthasoffer)
                            {
                                await Shell.Current.Navigation.PushAsync(new SelectDateOfferPage());
                            }
                            else
                            {
                                await App.Current.MainPage.DisplayAlert("Error", "Product has offer, Delete offer before insert.", "OK");
                            }

                        }
                    }

                }
               

            });

            CreateOfferCommand = new Command(async() =>
            {
                await Shell.Current.Navigation.PushAsync(new SelectProductToSetOfferPage());             
            });

            DoneCommand = new Command(async () => 
            {
                AOffer = new Offer()
                {
                    Title = Title,
                    Description = Description,
                    Id = Guid.NewGuid(),
                    MyProductId = SelectedProduct.Id,
                    BuyQuantity = BuyQuantity,
                    TotalPrice = TotalPrice,
                    StartDate = OfferDate.StartDate,
                    EndDate = OfferDate.EndDate,
                    BuyOne = PriceByOne,
                    Percent = ItemPercent,
                    StoreId=Guid.Parse(StoreID)
                    
                };

                InsertOffert(AOffer);

            });

            NextCommand = new Command(async ()=>
            {
                if(OfferDate.StartDate >= DateTime.Today)
                {

                await Shell.Current.Navigation.PushAsync(new OfferTitleAndDescriptionPage());
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Date need to be equal o greater than today. Check your data", "OK");
                }
            });

            SetOffertCommand = new Command(() => { });

            DeleteCommand = new Command<CommandEventData>(async (data) =>
            {


                var eventArgs = data.Args as Syncfusion.ListView.XForms.SwipeEndedEventArgs;

                var item = eventArgs.ItemData as Offer;


                if (eventArgs.SwipeDirection == Syncfusion.ListView.XForms.SwipeDirection.Right)
                {
                    if (eventArgs.SwipeOffset >= 100)
                    {
                        if (item != null)
                        {

                           bool result= productItemDataStore.RemoveOffert(item);

                            if(result)
                            {
                                await App.Current.MainPage.DisplayAlert("Notification", "Succefully Removed", "OK");
                                Offers.Remove(item);

                            }

                        }
                    }

                }


            });

            //GoTitleAndDescriptionCommand = new Command(async () => await Shell.Current.GoToAsync(TitleAnDescription.Route));
        }

    
        internal async  void InsertOffert(Offer offer)
        {         
                bool result = this.productItemDataStore.InsertOffer(offer);

                if (result)
                {
                    await Shell.Current.GoToAsync(OffersHomePage.Route);
                    var datastore = this.productItemDataStore.GetOfferOfStore(storeId);
                    Offers = new ObservableCollection<Offer>(datastore);
                    Reset();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Insert was not succesfully.", "OK");
                }
        }

   
        internal double ApplyDiscount(double price, double discountpercent)
        {
            double tosubtract = price * (discountpercent / 100);

            double total = price - tosubtract;

            return total;
        }

     
        internal double ApplyDiscountByQuantity(int priceof, double price)
        {

            double total = priceof * price;

            return total;
        }

     
        private void AddValidations()
        {


            ValidationOfferTitle.ValidationsRules.AddRange(
                 new List<IValidationRule<string>>
                 {
                     new IsNotNullOrEmptyRule<string> { ValidationMessage = "Is required." },

                 });
            ValidationDesciption.ValidationsRules.AddRange(
                new List<IValidationRule<string>>
                {
                     new IsNotNullOrEmptyRule<string> { ValidationMessage = "Is required." },

                });
            //BuyQuantityValidation.ValidationsRules.AddRange(
            //    new List<IValidationRule<double>>
            //    {
            //        new IsDifferentZero<double>{ValidationMessage = "Greater than zero."}

            //    });
            //TotalPriceValidation.ValidationsRules.AddRange(
            //  new List<IValidationRule<double>>
            //  {
            //        new IsDifferentZero<double>{ValidationMessage = "Greater than zero."}

            //  });

            //DateTimeValidation.ValidationsRules.AddRange(
            //new List<IValidationRule<DateTime>>
            //{
            //        new DateGreaterTodayRule<DateTime>{ValidationMessage = "Invalid Date."}

            //});
        }

     
        internal void Reset()
        {

            Title = string.Empty;
            Description = string.Empty;
            BuyQuantity = 0;
            TotalPrice = 0.00;
            OfferDate.StartDate = DateTime.Today;
            
        }

      
        internal void TitleValidation(bool isvalid)
        {
            if (!isvalid)
            {

                TitleError = ValidationOfferTitle.Errors[0];
                IsShowError = true;
            }
            else
            {
                IsShowError = false;
            }
        }

      
        internal void DescriptionValidation(bool isvalid)
        {
            if (!isvalid)
            {

                DescriptionError = ValidationDesciption.Errors[0];
                IsShowErrorDescription = true;
            }
            else
            {
                IsShowErrorDescription = false;
            }
        }

     
        internal void BuyQtyValidation(bool isvalid)
        {
            if (!isvalid)
            {

                BuyQuantityErrorMsg = BuyQuantityValidation.Errors[0];
                ShowBuyQuantityError = true;
            }
            else
            {
                ShowBuyQuantityError = false;
            }
        }

    
        internal void ValidationTotalPrice(bool isvalid)
        {
            if (!isvalid)
            {

                TotalPriceErrorMsg = TotalPriceValidation.Errors[0];
                ShowErrorTotalPrice = true;
            }
            else
            {
                ShowErrorTotalPrice = false;
            }
        }

      
        internal void ValidationDate(bool isvalid)
        {
            if (!isvalid)
            {

                DateErrorMsg = DateTimeValidation.Errors[0];
                ShowDateError = true;
                App.Current.MainPage.DisplayAlert("Error", DateErrorMsg, "OK");
            }
            else
            {
                ShowDateError = false;
            }
        }

      
    }
}
