using DataAccessLayer;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using DataAccessLayer.Repos.Invoices;
using DataAccessLayer.Repos.Products;
using LogicLayer.Global.Users;
using LogicLayer.Services;
using LogicLayer.Services.Invoices;
using LogicLayer.Services.Invoices.Helper_Service;
using LogicLayer.Services.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic.Logging;
using Serilog;
using Serilog.Extensions.Logging;
using Serilog.Filters;
using Serilog.Sinks.File;
using System.CodeDom;
using System.Globalization;
using System.Threading;

namespace InventorySalesManagementSystem
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            


            //Logger Creation
            Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()

            // ===== Application Logs =====
            .WriteTo.Logger(lc => lc
                .Filter.ByExcluding(
                    Matching.FromSource("Microsoft.EntityFrameworkCore"))
                .WriteTo.File(
                    path: "Logs\\app-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 14,
                    shared: true))

            // ===== EF Core Logs =====
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(
                    Matching.FromSource("Microsoft.EntityFrameworkCore"))
                .WriteTo.File(
                    path: "Logs\\ef-.log",
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 14,
                    shared: true))

            .CreateLogger();



            //App Setting Config
            var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                    .Build();

            //Conneciton String
            var connectionString =
                configuration.GetConnectionString("InventorySales");

            //Service Config
            var services = new ServiceCollection();

            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlServer(connectionString));


            services.AddLogging(logging =>
            {
                logging.ClearProviders();
                logging.AddSerilog();
            });

            //Basics
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            //Special Repos
            services.AddScoped<IProductPriceLogRepository, ProductPriceLogRepository>();
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IProductStockMovementLogRepository, ProductStockMovementLogRepository>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IWorkerRepository, WorkerRepository>();
            services.AddScoped<IProductTypeRepository, ProductTypeRepository>();
            services.AddScoped<ISoldProductRepository, SoldProductRepository>();
            services.AddScoped<IInvoiceRepository, InvoiceRepository>();
            services.AddScoped<ITakeBatchRepository, TakeBatchRepository>();

            //Generic Repos
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

            //Global
            services.AddSingleton<LogicLayer.Global.Users.UserSession>();

            //Services
            services.AddScoped<ProductPriceLogService>();
            services.AddScoped<ProductService>();
            services.AddScoped<ProductStockMovementLogService>();
            services.AddScoped<ProductTypeService>();
            services.AddScoped<CustomerService>();
            services.AddScoped<MasurementUnitService>();
            services.AddScoped<PersonService>();
            services.AddScoped<TownService>();
            services.AddScoped<UserService>();
            services.AddScoped<WorkerService>();
            services.AddScoped<SoldProductService>();
            services.AddScoped<TakeBatchService>();
            services.AddScoped<InvoiceService>();

            //Service Helpers
            services.AddScoped<InvoiceServiceHelper>();


            var serviceProvider = services.BuildServiceProvider();
            
            try
            {
                ApplicationConfiguration.Initialize();

                var loginForm = new frmLogin(serviceProvider);

                if(loginForm.ShowDialog()!= DialogResult.OK)
                    return;
                else
                    Application.Run(new Form1(serviceProvider));

            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal(ex, "Application crashed");
                throw;
            }
            finally
            {
                Serilog.Log.CloseAndFlush();
            }
        }
    }
}