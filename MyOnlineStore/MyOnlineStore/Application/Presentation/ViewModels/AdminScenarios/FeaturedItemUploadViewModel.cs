using MyOnlineStore.Application.Common.Global.Constants;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Application.Presentation.ViewModels;
using MyOnlineStore.Application.Presentation.ViewModels.Base;
using MyOnlineStore.Application.Presentation.Views.AdminScenarios.Stores.ContentStepView;
using MyOnlineStore.Application.Presentation.Views.Templates.Popups;
using MyOnlineStore.Application.Presentation.Views.TemplatesViews.Popups;
using MyOnlineStore.Entities.Models.Stores;
using MyOnlineStore.Shared.Interfaces.Factories;
using Rg.Plugins.Popup.Services;
using Syncfusion.ListView.XForms;
using Syncfusion.SfCalendar.XForms;
using Syncfusion.XForms.PopupLayout;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyOnlineStore.Application.Presentaion.ViewModels.AdminScenarios
{
    /*
     Todo: Make Update to selected item.

         */
    [QueryProperty("StoreID", "storeID")]
    public class FeaturedItemUploadViewModel : BaseViewModel, IFeaturedItemUploadViewModel
    {
        protected readonly IStoreDataStore _StoreDataStore;
        protected readonly IStoreFeaturedItemFactory _FeaturedItemFactory;
        public ICommand UploadImageContentCommand { get; set; }
        public ICommand InsertContentImageCommand { get; set; }
        public ICommand SuccessPopupCommand { get; set; }
        public ICommand RemoveContentImageCommand { get; set; }
        public ICommand SaveContentToCurrentStore { get; set; }
        public ICommand StartCreateContent { get; set; }

        public ICommand Clickevent { get; set; }

        public ICommand GoUpdateCommand { get; set; }
        public ICommand UpdateContentCommand { get; set; }

        public ICommand MovePosition { get; set; }

        public ICommand ExecutePopupCommand { get; set; }

        public ICommand ItemDraggincommand { get; set; }
        public ICommand SwipeDeleteCommand { get; set; }
        public ICommand SaveFeatureItem { get; set; }

        public ICommand NextCommand { get; set; }
        public ICommand NextUpdateCommand { get; set; }
        public Store? CurrentStore { get; set; } 

        private ObservableCollection<StoreFeaturedItem> _featuredItems = new ObservableCollection<StoreFeaturedItem>();
        public ObservableCollection<StoreFeaturedItem> FeaturedItems
        {
            get => _featuredItems;
            set
            {
                if (value != null)
                {
                    _featuredItems = value;
                    RaisePropertyChanged(() => FeaturedItems);
                }

            }
        }

        //Este property adquiere valor del query property.
        // Este filtrara para escojer los featuresitems de la tienda seleccionada.
        private string storeID;
       
        public string StoreID
        {
            get { return storeID; }
            set
            { 
                storeID = value;

                if (!string.IsNullOrEmpty(storeID))
                {
                    Guid guidresult;
                    bool result = Guid.TryParse(StoreID, out guidresult);
                    var featureitemdata = _StoreDataStore.GetFeaturedItemsOfStore(guidresult);
                    CurrentStore.FeaturedItems = new List<StoreFeaturedItem>(featureitemdata);
                    FeaturedItems =new ObservableCollection<StoreFeaturedItem>( CurrentStore.FeaturedItems);
                }
                RaisePropertyChanged(() => StoreID);
            }
        }


        private StoreFeaturedItem temp = new StoreFeaturedItem();
        public StoreFeaturedItem Temp
        {
            get => temp;
            set
            {
                if (value != null)
                {
                    temp = value;
                    RaisePropertyChanged(() => Temp);
                }

            }
        }

        private int _currentIndex = 0;
        public int CurrentIndex
        {
            get => _currentIndex;
            set
            {
                _currentIndex = value;
                RaisePropertyChanged(() => CurrentIndex);
            }
        }

        internal SelectionRange contentdate;

        /// <summary>
        /// Gets or sets the OfferDate
        /// </summary>
        public SelectionRange ContentDate
        {
            get => contentdate;
            set
            {
                contentdate = value;
                RaisePropertyChanged(() => ContentDate);
            }
        }

        private ImageSource itemiimage;

        /// <summary>
        /// Gets or sets the ItemImage
        /// </summary>
        public ImageSource ItemImage
        {
            get { return itemiimage; }
            set
            {
                itemiimage = value;
                RaisePropertyChanged(() => ItemImage);
            }
        }

        private StoreFeaturedItem selectedItem;

        public StoreFeaturedItem SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                RaisePropertyChanged(() => SelectedItem);

            }
        }

        private int selectedvalue;

        public int SelectedValue
        {
            get { return selectedvalue; }
            set
            {
                selectedvalue = value;
                RaisePropertyChanged(() => SelectedValue);

            }
        }


        List<StoreFeaturedItem> ItemsToChange;


        private int index;

        public int Index
        {
            get { return index; }
            set
            {
                index = value;
                RaisePropertyChanged(() => Index);
            }
        }

        public FeaturedItemUploadViewModel(IStoreDataStore storeDataStore, IStoreFeaturedItemFactory featuredItemFactory)
        {

            CurrentStore = App.ApplicationManager.CurrentStore;
            _StoreDataStore = storeDataStore;
            _FeaturedItemFactory = featuredItemFactory;

            //Guid guid;
            //bool result = Guid.TryParse(StoreID ,out guid );
            //var featureitemdata = _StoreDataStore.GetFeaturedItemsOfStore(guid);
            //CurrentStore.FeaturedItems = new List<StoreFeaturedItem>(featureitemdata);

            ContentDate = new SelectionRange();

            ContentDate.StartDate = DateTime.Today;
            ContentDate.EndDate = DateTime.Today;

            ItemsToChange = new List<StoreFeaturedItem>();
            SelectedItem = new StoreFeaturedItem();

            StartCreateContent = new Command(async () => { await StartCreate(); });
            UploadImageContentCommand = new Command(async () => await UploadContentImage());

            SuccessPopupCommand = new Command<string>(async (message) => await SuccessPopup(message));
            RemoveContentImageCommand = new Command(async () => await Remove());


            SaveContentToCurrentStore = new Command(async () =>
            {

                var items = new ObservableCollection<StoreFeaturedItem>(FeaturedItems);
                await UpdateContentPosition(items);
            });

            SaveFeatureItem = new Command(async () =>
            {

                await SaveContent();
            });

            Clickevent = new Command(async () => { await PopupNavigation.Instance.PushAsync(new HelpContentPopUp()); });

            Clickevent.Execute(null);

            NextCommand = new Command(async () => await Next());

            // GoUpdateCommand = new Command(async () => { await GoUpdateDate(); });
            NextUpdateCommand = new Command(async () => 
            {
                await NextUpdatePicture();
            });
            UpdateContentCommand = new Command(async () => await UpdateContent());

            ItemDraggincommand = new Command<CommandEventData>(async (data) =>
            {

                await ItemDraggingCommandHandler(data);

                //var eventArgs = data.Args as ItemDraggingEventArgs;

                // StoreFeaturedItem? currentDraggingItem = eventArgs.ItemData as StoreFeaturedItem;

                // var tempItem = FeaturedItems[eventArgs.NewIndex];


                // FeaturedItems[eventArgs.NewIndex] = currentDraggingItem;

                // FeaturedItems[eventArgs.OldIndex] = tempItem;

            });

            SwipeDeleteCommand = new Command<CommandEventData>(async (data) => {


                var eventArgs = data.Args as Syncfusion.ListView.XForms.SwipeEndedEventArgs;

                var item = eventArgs.ItemData as StoreFeaturedItem;


                if (eventArgs.SwipeDirection == Syncfusion.ListView.XForms.SwipeDirection.Right)
                {
                    if (eventArgs.SwipeOffset >= 100)
                    {
                        if (item != null)
                        {
                            await RemoveOnSwipe(item);

                        }
                    }

                }
                else if (eventArgs.SwipeDirection == Syncfusion.ListView.XForms.SwipeDirection.Left)
                {
                    if (eventArgs.SwipeOffset >= 100)
                    {
                        if (item != null)
                        {
                            await GoUpdateDate(item);
                        }
                    }

                }

            });

            Init();
        }

        private object popupcontrol;
        private bool displayPopup;

        public object PopupControl
        {
            get { return popupcontrol; }
            set
            {

                popupcontrol = value;
                RaisePropertyChanged(() => PopupControl);
            }
        }
        public bool DisplayPopup
        {

            get
            {
                return displayPopup;
            }
            set
            {
                displayPopup = value;
                RaisePropertyChanged(() => DisplayPopup);


            }
        }

        public void PopUpClosed()
        {

            this.DisplayPopup = false;
        }
        private void ExecutePopupCommandHandler(SfPopupLayout popup)
        {
            popup.Show();
        }

        private void Init()
        {
            byte[] image;
           // Utils.GetByteArrayFromPath(out image, LocalStorage.ImagePlaceHolderPath);

            if (CurrentStore != null && CurrentStore.FeaturedItems != null)
            {
                FeaturedItems = new ObservableCollection<StoreFeaturedItem>(CurrentStore.FeaturedItems.OrderBy(i => i.DisplayPosition));
             
            }

        }

        byte[] images { get; set; }



        private async Task UploadContentImage()
        {
            if (CurrentStore != null)
            {
                MemoryStream stream = new MemoryStream(await Utils.PickPhoto());
                images = stream.ToArray();


                ItemImage = ItemImage = ImageSource.FromStream(() => stream);

            }
        }

        //Actualiza un objecto de contenido
        private async Task GoUpdateDate(StoreFeaturedItem featuredItem)
        {

            if (featuredItem.Id.ToString() != Guid.Empty.ToString())
            {

                App.ContentTempStore.Id = featuredItem.Id;
                await App.Current.MainPage.Navigation.PushAsync(new UpdateDatePage());

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "You have not selected an item,Select one item.", "OK");
            }
        }
        private async Task SuccessPopup(string message)
        {
            var popup = new SuccessPopup
            {
                LottieAnimation = "animation_success.json",
                Message = message
            };

            await PopupNavigation.Instance.PushAsync(popup);
        }

        private async Task SaveContent()
        {
            bool saved = false;
            if (CurrentStore != null)
            {
                App.ContentTempStore.Id = Guid.NewGuid();
                App.ContentTempStore.MyStoreId = CurrentStore.Id;
                App.ContentTempStore.Image = images;

                if (App.ContentTempStore.Image.Length > 1)
                {

                    Temp = App.ContentTempStore;
                    FeaturedItems.Add(Temp);

                    int position = FeaturedItems.IndexOf(FeaturedItems.Where(i => i.Id == App.ContentTempStore.Id).FirstOrDefault());

                    FeaturedItems[position].DisplayPosition = position;

                    CurrentStore.FeaturedItems = new List<StoreFeaturedItem>(FeaturedItems);
                    saved = await Task.Run(() => _StoreDataStore.AddFeaturedItem(CurrentStore.FeaturedItems));
                    if (saved)
                    {
                        ContentDate.StartDate = DateTime.Today;
                        ContentDate.EndDate = DateTime.Today;

                        FeaturedItems = new ObservableCollection<StoreFeaturedItem>(FeaturedItems.OrderBy(i => i.DisplayPosition));

                        SuccessPopupCommand.Execute("Successfully Saved Content to Store");
                        await App.Current.MainPage.Navigation.PushAsync(new HomeContentPage());


                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Need to insert a image first.", "OK");
                }

            }

        }

        private async Task UpdateContent()
        {
            bool saved = false;
            if (CurrentStore != null)
            {

                StoreFeaturedItem updateditem = new StoreFeaturedItem()
                {
                    Id = App.ContentTempStore.Id,
                    MyStoreId = CurrentStore.Id,
                    Start = ContentDate.StartDate,
                    End = ContentDate.EndDate,
                    Image = images

                };
                if (updateditem.Image.Length > 1)
                {

                    FeaturedItems[FeaturedItems.IndexOf(FeaturedItems.Where(i => i.Id == updateditem.Id).FirstOrDefault())] = updateditem;
                    FeaturedItems[FeaturedItems.IndexOf(FeaturedItems.Where(i => i.Id == updateditem.Id).FirstOrDefault())].DisplayPosition = FeaturedItems.IndexOf(FeaturedItems.Where(i => i.Id == updateditem.Id).FirstOrDefault());
                    //FeaturedItems.Add(updateditem);
                    CurrentStore.FeaturedItems = new List<StoreFeaturedItem>(FeaturedItems);
                    saved = await Task.Run(() => _StoreDataStore.UpdateFeatureItem(updateditem));
                    if (saved)
                    {
                        ContentDate.StartDate = DateTime.Today;
                        ContentDate.EndDate = DateTime.Today;
                        FeaturedItems = new ObservableCollection<StoreFeaturedItem>(FeaturedItems.OrderBy(i => i.DisplayPosition));

                        SuccessPopupCommand.Execute("Successfully Updated Content to Store");
                        await App.Current.MainPage.Navigation.PushAsync(new HomeContentPage());

                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "Insert image", "OK");
                }

            }

        }

        private async Task UpdateContentPosition(ObservableCollection<StoreFeaturedItem> touptade)
        {
            bool saved = false;
            if (CurrentStore != null)
            {

                if (touptade.Count > 0)
                {
                    FeaturedItems.Clear();
                    FeaturedItems = new ObservableCollection<StoreFeaturedItem>(touptade);

                    for (int i = 0; i < FeaturedItems.Count; i++)
                    {
                        FeaturedItems[i].DisplayPosition = i;
                    }



                    CurrentStore.FeaturedItems = new List<StoreFeaturedItem>(FeaturedItems);
                    saved = await Task.Run(() => _StoreDataStore.UpdateFeatureItems(FeaturedItems));
                    if (saved)
                    {

                        FeaturedItems = new ObservableCollection<StoreFeaturedItem>(FeaturedItems.OrderBy(i => i.DisplayPosition));

                        SuccessPopupCommand.Execute("Successfully Updated Content Position.");
                    }
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Error", "You dont have nothing to save.", "OK");
                }

            }

        }

        private async Task Remove()
        {
            bool update = false;
            if (CurrentStore != null)
            {
                var toremoveitem = FeaturedItems.Where(i => i.Id == SelectedItem.Id).FirstOrDefault();
                update = await Task.Run(() => _StoreDataStore.RemoveFeatureItem(toremoveitem));

                if (update)
                {
                    SuccessPopupCommand.Execute("Successfully Removed.");
                    FeaturedItems.Remove(toremoveitem);
                    SelectedItem = null;
                }
            }



        }

        private async Task RemoveOnSwipe(StoreFeaturedItem item)
        {
            bool update = false;
            if (CurrentStore != null)
            {
                var toremoveitem = FeaturedItems.Where(i => i.Id == item.Id).FirstOrDefault();
                update = await Task.Run(() => _StoreDataStore.RemoveFeatureItem(toremoveitem));

                if (update)
                {
                    SuccessPopupCommand.Execute("Successfully Removed.");
                    FeaturedItems.Remove(toremoveitem);
                    SelectedItem = null;
                }
            }



        }

        private async Task ItemDraggingCommandHandler(CommandEventData data)
        {
            await Device.InvokeOnMainThreadAsync(() =>
            {
                if (data is object && data.Args is ItemDraggingEventArgs args && args.Action == DragAction.Drop && args.ItemData is StoreFeaturedItem featuredItem && data.Sender is SfListView listView)
                {
                    var tempItem = FeaturedItems[args.NewIndex];

                    FeaturedItems[args.NewIndex] = featuredItem;

                    FeaturedItems[args.OldIndex] = tempItem;

                    listView.RefreshListViewItem(args.OldIndex, args.NewIndex);

                    args.Handled = true;
                }
            });
        }

        public async Task Next()
        {
            App.ContentTempStore = new StoreFeaturedItem();

            App.ContentTempStore.Start = ContentDate.StartDate;
            App.ContentTempStore.End = ContentDate.EndDate;

            if (!String.IsNullOrEmpty(App.ContentTempStore.Start.ToString()) && !String.IsNullOrEmpty(App.ContentTempStore.End.ToString()))
            {

                await App.Current.MainPage.Navigation.PushAsync(new EditPicture());
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Fill Date before", "OK");
            }

        }

        public async Task NextUpdatePicture()
        {

            if (!string.IsNullOrEmpty(ContentDate.StartDate.ToString()) && !string.IsNullOrEmpty(ContentDate.EndDate.ToString()))
            {

                await Shell.Current.GoToAsync(UpdatePicturePage.Route);
            }

        }
        public async Task StartCreate()
        {
            if (FeaturedItems.Count < 5)
            {
                await App.Current.MainPage.Navigation.PushAsync(new SelectedDate());

            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Messages", "You have entered the content limit.", "OK");
            }

        }

    }
}