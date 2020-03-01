using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using MyOnlineStore.Application;
using MyOnlineStore.Application.Common.Global.Constants;
using MyOnlineStore.Application.Common.Global.HubManger;
using MyOnlineStore.Application.Common.Interfaces.DataStore;
using MyOnlineStore.Application.Common.Interfaces.Factories;
using MyOnlineStore.Application.Common.Interfaces.IViewModel;
using MyOnlineStore.Application.Common.Interfaces.Services.Store;
using MyOnlineStore.Application.Common.Interfaces.ViewInterfaces;
using MyOnlineStore.Application.Common.Utilities;
using MyOnlineStore.Application.Data.DataStore;
using MyOnlineStore.Application.Data.Factories.FavoriteStoreClientFactory;
using MyOnlineStore.Application.Data.Factories.ProductItemFactories;
using MyOnlineStore.Application.Data.Factories.Store;
using MyOnlineStore.Application.Data.Factories.UsersFactories;
using MyOnlineStore.Application.Data.Factories.ValidatableObjectsFactories;
using MyOnlineStore.Application.Data.Models.Entries;
using MyOnlineStore.Application.Domain.Interfaces.IViewModel;
using MyOnlineStore.Application.Presentaion.ViewModels.AdminScenarios;
using MyOnlineStore.Application.Presentation.Managers;
using MyOnlineStore.Application.Presentation.Services.Stores;
using MyOnlineStore.Application.Presentation.ViewModels;
using MyOnlineStore.Application.Presentation.ViewModels.AdminScenarios;
using MyOnlineStore.Application.Presentation.ViewModels.Home;
using MyOnlineStore.Application.Presentation.ViewModels.LoginScenarios;
using MyOnlineStore.Application.Presentation.ViewModels.MyAccount;
using MyOnlineStore.Application.Presentation.ViewModels.Notification;
using MyOnlineStore.Application.Presentation.ViewModels.OrdersScenarios;
using MyOnlineStore.Application.Presentation.ViewModels.StoresScenarios;
using MyOnlineStore.Application.Presentation.ViewModels.WorkSpaceScenarios;
using MyOnlineStore.Application.Presentation.Views.LoginScenarios;
using MyOnlineStore.DataStore;
using MyOnlineStore.Interfaces.DataStore;
using MyOnlineStore.Shared.Factories.CardFactories;
using MyOnlineStore.Shared.Factories.OrderFactory;
using MyOnlineStore.Shared.Factories.ProductItemFactories;
using MyOnlineStore.Shared.Factories.StoreFactories;
using MyOnlineStore.Shared.Factories.StoreFeaturedItemFactories;
using MyOnlineStore.Shared.Interfaces.Factories;
using MyOnlineStore.Shared.Models.Entries;
using MyOnlineStore.Testing.MockData.Entries;
using MyOnlineStore.ViewModels.Admin;
using System;
using System.IO;
using Xamarin.Essentials;

namespace MyOnlineStore
{
    public static class Startup
    {
        #region Properties

        public static IServiceProvider ServiceProvider { get; set; } = null!;

        #endregion Properties

        #region Methods

        public static App Init(Action<HostBuilderContext, IServiceCollection> nativeConfigureServices)
        {
            var systemDir = FileSystem.CacheDirectory;
            Utils.ExtractSaveResource(@"MyOnlineStore.appsettings.json", systemDir);
            var fullConfig = Path.Combine(systemDir, "MyOnlineStore.appsettings.json");

            //LoggingConfiguration
            var host = new HostBuilder()
                .ConfigureHostConfiguration(c =>
                {
                    c.AddCommandLine(new string[] { $"ContentRoot={FileSystem.AppDataDirectory}" });
                    c.AddJsonFile(fullConfig);
                })
                .ConfigureServices((context, services) =>
                {
                    nativeConfigureServices(context, services);
                    ConfigureServices(context, services);
                })
                .ConfigureLogging(l => l.AddConsole(o =>
                {
                    o.DisableColors = false;
                    o.IncludeScopes = true;
                    o.LogToStandardErrorThreshold = LogLevel.Information;
                }))
                .Build();

            ServiceProvider = host.Services;

            //Register Syncfusion license
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(@"MTg4NzUwQDMxMzcyZTM0MmUzMEZqaUw4QktNMUU5aXgxWVhCT3VJeldmUGhMWGplTi9MU0JlSEJBOTNaTkk9");

            return ServiceProvider.GetService<App>();
        }

        private static void ConfigureServices(HostBuilderContext ctx, IServiceCollection services)
        {
            if(ctx.HostingEnvironment.IsDevelopment())
            {
                var world = ctx.Configuration["Hello"];
            }
            else
            {
            }

            services.AddHttpClient("MyHttpClient", client =>
            {
                client.BaseAddress = new Uri($"{APIKeys.LocalBackendUrl}/");
            });

            services.AddSingleton<AppShell>();
            services.AddSingleton<App>();
            services.AddSingleton<RegisterAccountPage>();
            services.AddSingleton<LoginPage>();
            services.AddSingleton<Global.AppTheme>();
            services.AddSingleton<LoginEntry>();
            services.AddSingleton<RegisterEntry>();
            services.AddSingleton<MockRegisterEntry>();
            services.AddSingleton<UserCardEntry>();
            services.AddTransient<RegisterPage>();

            #region FactoriesServices

            services.AddSingleton<ICardFactory, CardFactory>();
            services.AddSingleton<IFavoriteStoreClientFactory, FavoriteStoreClientFactory>();
            services.AddSingleton<ILoggerFactory, LoggerFactory>();
            services.AddSingleton<IOrderFactory, OrderFactory>();
            services.AddSingleton<IOrdersProductItemsFactory, OrdersProductItemsFactory>();
            services.AddSingleton<IProductItemBuyPresenterFactory, ProductItemBuyPresenterFactory>();
            services.AddSingleton<IProductItemFactory, ProductItemFactory>();
            services.AddSingleton<IStoreFactory, StoreFactory>();
            services.AddSingleton<IStoreFeaturedItemFactory, StoreFeaturedItemFactory>();
            services.AddSingleton<IStorePresenterFactory, StorePresenterFactory>();
            services.AddSingleton<IUserFactory, UserFactory>();
            services.AddSingleton<IValidatableObjectFactory, ValidatableObjectsFactory>();

            #endregion FactoriesServices

            #region DataStoreServices

            services.AddSingleton<IUserDataStore, UserDataStore>();
            services.AddSingleton<IFavoritesDataStore, FavoritesDataStore>();
            services.AddSingleton<IOrdersDataStore, OrderDataStore>();
            services.AddSingleton<IProductItemDataStore, ProductItemDataStore>();
            services.AddSingleton<IRoleDataStore, RoleDataStore>();
            services.AddSingleton<IStoreDataStore, StoreDataStore>();
            services.AddSingleton<IUserCardDataStore, UserCardDataStore>();
            services.AddSingleton<IEmployeeDataStore, EmployeeDataStore>();
            services.AddSingleton<INotificationDataStore, NotificationDataStore>();
            services.AddSingleton<IStoresEmployeesDataStore, StoreEmployeeDataStore>();

            #endregion DataStoreServices

            #region ViewModelsServices

            services.AddScoped<IProductItemDetailViewModel, ProductItemDetailViewModel>();
            services.AddSingleton<IExploreStoresViewModel, ExploreStoresViewModel>();
            services.AddSingleton<IFeaturedItemUploadViewModel, FeaturedItemUploadViewModel>();
            services.AddSingleton<IHomeViewModel, HomeViewModel>();
            services.AddSingleton<IInventoryViewModel, InventoryViewModel>();
            services.AddSingleton<ILoginViewModel, LoginViewModel>();
            services.AddSingleton<IRegisterStoreViewModel, RegisterStoreViewModel>();
            services.AddSingleton<IReportViewModel, ReportViewModel>();
            services.AddSingleton<IShellViewModel, ShellViewModel>();
            services.AddTransient<ICheckoutViewModel, CheckoutViewModel>();
            services.AddTransient<IMyAccountSettingsViewModel, MyAccountViewModel>();
            services.AddTransient<IRegisterCardViewModel, RegisterCardViewModel>();
            services.AddTransient<IRegisterViewModel, RegisterViewModel>();
            services.AddTransient<IShellViewModel, ShellViewModel>();
            services.AddTransient<IStoreHomeViewModel, StoreHomeViewModel>();
            services.AddTransient<IStoreShopViewModel, StoreShopViewModel>();
            services.AddSingleton<IStoreRegistrationEntry, StoreRegistrationEntry>();
            services.AddSingleton<IMyStoresViewModel, MyStoresViewModel>();
            services.AddSingleton<IOfferViewModel, OfferContentViewModel>();
            services.AddSingleton<IEmployeeViewModel, EmployeeViewModel>();
            services.AddSingleton<INotificationViewModel, NotificationViewModel>();
            services.AddSingleton<IWorkSpaceViewModel, WorkSpaceViewModel>();
            services.AddSingleton<IOfferContentViewModel, ContentOfferViewModel>();

            #endregion ViewModelsServices

            #region ServiceServices

            services.AddSingleton<IStoreCardService, StoreCardService>();
            services.AddSingleton<ShoppingManager>();
            services.AddSingleton<ApplicationManager>();
            services.AddSingleton<HubManager>();

            #endregion ServiceServices
        }

        #endregion Methods
    }
}