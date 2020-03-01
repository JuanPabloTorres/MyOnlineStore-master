using ImageCircle.Forms.Plugin.Abstractions;
using MyOnlineStore.ViewModels;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Collections.ObjectModel;
using System.IO;
using Xamarin.Forms;
using Microsoft.Extensions.DependencyInjection;


namespace MyOnlineStore.Views.Administrator
{
    [Xamarin.Forms.Xaml.XamlCompilation(Xamarin.Forms.Xaml.XamlCompilationOptions.Compile)]
    public partial class AddItemPage : ContentPage
    {
        ObservableCollection<MediaFile> files = new ObservableCollection<MediaFile>();
        AdministratorViewModel aVM;
        public AddItemPage()
        {
            InitializeComponent();

            aVM = Startup.ServiceProvider.GetService<AdministratorViewModel>();

            BindingContext = aVM;
            files.CollectionChanged += Files_CollectionChanged;

            pickPhoto.Clicked += async (sender, args) =>
            {
                files.Clear();
                if (!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                    return;
                }
                var file = await CrossMedia.Current.PickPhotoAsync(new PickMediaOptions
                {
                    PhotoSize = PhotoSize.Medium,
                });
              

                if (file == null)
                    return;
                else
                {
                    byte[] imageByte;
                    Stream imageStream = null;
                    imageStream = file.GetStream();
                    BinaryReader br = new BinaryReader(imageStream);
                    imageByte = br.ReadBytes((int)imageStream.Length);
                    aVM.NewItem.Image = imageByte;
                }

                files.Add(file);
            };
        }
       

        private void Files_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            if (files.Count == 0)
            {
                ImageList.Children.Clear();
                return;
            }
            if (e.NewItems.Count == 0)
                return;

            var file = e.NewItems[0] as MediaFile;
            var image = new Image { WidthRequest = 300, HeightRequest = 300, Aspect = Aspect.AspectFit };
            image.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                return stream;
            });

            Image CImg = new Image()
            {
                HeightRequest = 150,
                WidthRequest = 150,
                Source = image.Source
            };

            ImageList.Children.Add(CImg);
        }


       

       
       
    }
}
