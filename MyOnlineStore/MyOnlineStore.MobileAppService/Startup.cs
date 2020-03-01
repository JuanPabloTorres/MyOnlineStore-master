using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using MyOnlineStore.MobileAppService.Context;
using MyOnlineStore.MobileAppService.Interfaces.Repositories;
using MyOnlineStore.MobileAppService.Repositories;
using MyOnlineStore.Shared.Interfaces.Factories;
using MyOnlineStore.Shared.Factories.OrderFactory;
using MyOnlineStore.Shared.Factories.RolesFactory;
using MyOnlineStore.MobileAppService.Hubs;
using Microsoft.AspNetCore.SignalR;
using MyOnlineStore.MobileAppService.Interfaces.Hub.HubManegement;
using MyOnlineStore.MobileAppService.Interfaces.Hub;
using MyOnlineStore.MobileAppService.Hubs.Mapping;
using Microsoft.AspNet.SignalR;

namespace MyOnlineStore.MobileAppService
{
    public class Startup
    {
        private static readonly string _ActiveEnviroment = "ActiveEnviromentStage";
        public static bool Mode { get; private set; }
        public IConfiguration Configuration;
        public string _ConnectionString { get; private set; } = string.Empty;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            Console.WriteLine($"App Root Path: {env.ContentRootPath}");           
            
            var builder = new ConfigurationBuilder()
           .SetBasePath(env.ContentRootPath)
           .AddJsonFile($"appsettings.json", optional: false)
           .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
           .AddEnvironmentVariables();
            
            Configuration = builder.Build();
            
            env.EnvironmentName = Configuration.GetSection(_ActiveEnviroment).Value;

            Mode = env.IsDevelopment();
            Console.WriteLine($"Enviroment: { env.EnvironmentName}");
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().ConfigureApplicationPartManager(p => 
            //    p.FeatureProviders.Add(new GenericControllerFeatureProvider())
            //).AddNewtonsoftJson();

            services.AddMvc().AddNewtonsoftJson();

            //var idProvider = new SignalRUserIdProvider();
            //Microsoft.AspNet.SignalR.GlobalHost.DependencyResolver.Register(typeof(IUserIdProvider), () => idProvider);

            services.AddSignalR();
            services.AddControllers().AddNewtonsoftJson();
            
            #region Repositories
            services.AddScoped<IUsersRepository,UsersRepository>();
            services.AddScoped<IUserCardRepository, UserCardRepository>();
            services.AddScoped<IStoresRepository, StoresRepository>();
            services.AddScoped<IProductItemRepository, ProductItemRepository>();
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IFavoriteStoreClientRepository, FavoriteStoreClientRepository>();
            services.AddScoped<IEmployeeRepository, EmployeesRepository>();
            services.AddScoped<INotificationRepository, NotificationRepository>();

            services.AddSingleton<IUserConnectionManager, UserConnectionManager>();

            #endregion

            #region Factories
            services.AddScoped<IOrderFactory, OrderFactory>();
            services.AddScoped<IRoleFactory, RoleFactory>();
            #endregion

            if (Mode)
            {
                _ConnectionString = Configuration.GetConnectionString("DevelopmentDBLocal");
                Console.WriteLine(_ConnectionString);
                Console.WriteLine("################################");
                services.AddDbContext<MyContext>(options => options.UseSqlServer(_ConnectionString),ServiceLifetime.Transient);
            }
            else
            {
                Console.WriteLine("Not in Dev Mode".ToUpper());
                // Production DB
                //_ConnectionString = Configuration.GetConnectionString("DevelopmentDB");
                //services.AddDbContext<MyContext>(options => options.UseSqlServer(_ConnectionString));
            }
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            ////app.UseHttpsRedirection();
            //var idProvider = new UserIdProvider();

            //GlobalHost.DependencyResolver.Register(typeof(Microsoft.AspNet.SignalR.IUserIdProvider), () => idProvider);

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
               endpoints.MapHub<NotificationHub>("/hubs/notification");
            });

          

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            { 
                ForwardedHeaders = Microsoft.AspNetCore.HttpOverrides.ForwardedHeaders.All 
            });

            Console.WriteLine($"=============== Listening ===============");
        }
    }
}