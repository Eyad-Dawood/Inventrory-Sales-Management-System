using DataAccessLayer;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Abstractions.Invoices;
using DataAccessLayer.Abstractions.Payments;
using DataAccessLayer.Abstractions.Products;
using DataAccessLayer.DesignTimeOnly;
using DataAccessLayer.Repos;
using DataAccessLayer.Repos.Invoices;
using DataAccessLayer.Repos.Payments;
using DataAccessLayer.Repos.Products;
using LogicLayer.Services;
using LogicLayer.Services.Invoices;
using LogicLayer.Services.Payments;
using LogicLayer.Services.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Filters;

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

            var dbFolder = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData),
                "InventorySales");


            var logsFolder = Path.Combine(dbFolder, "Logs");
            Directory.CreateDirectory(logsFolder);



            //Logger Creation
            Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()

            // ===== Application Logs =====
            .WriteTo.Logger(lc => lc
                .Filter.ByExcluding(
                    Matching.FromSource("Microsoft.EntityFrameworkCore"))
                .WriteTo.File(
                    path: Path.Combine(logsFolder, "app-.log"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 14,
                    shared: true))

            // ===== EF Core Logs =====
            .WriteTo.Logger(lc => lc
                .Filter.ByIncludingOnly(
                    Matching.FromSource("Microsoft.EntityFrameworkCore"))
                .Filter.ByExcluding(
                    Matching.FromSource("Microsoft.EntityFrameworkCore.Migrations"))
                .WriteTo.File(
                    path: Path.Combine(logsFolder, "ef-.log"),
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 14,
                    shared: true))

            // ===== Migrations =====
            .WriteTo.Logger(lc => lc
            .MinimumLevel.Warning() //to avoid PRAGMA warning , EF conflict
            .Filter.ByIncludingOnly(
             Matching.FromSource("Microsoft.EntityFrameworkCore.Migrations"))
            .WriteTo.File(
             path: Path.Combine(logsFolder, "migration-.log"),
             rollingInterval: RollingInterval.Day,
             retainedFileCountLimit: 30,
             shared: true))
             .CreateLogger();


            //Service Config
            var services = new ServiceCollection();



            Directory.CreateDirectory(dbFolder);

            var dbPath = Path.Combine(dbFolder, "inventory.db");

            services.AddDbContext<InventoryDbContext>(options =>
                options.UseSqlite($"Data Source={dbPath}"));



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
            services.AddScoped<IPaymentRepository, PaymentRepository>();

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
            services.AddScoped<PersonService>();
            services.AddScoped<TownService>();
            services.AddScoped<UserService>();
            services.AddScoped<WorkerService>();
            services.AddScoped<SoldProductService>();
            services.AddScoped<TakeBatchService>();
            services.AddScoped<InvoiceService>();
            services.AddScoped<PaymentService>();


            var serviceProvider = services.BuildServiceProvider();

            try
            {
                using var scope = serviceProvider.CreateAsyncScope();
                var db = scope.ServiceProvider.GetRequiredService<InventoryDbContext>();

                var pendingMigrations = db.Database.GetPendingMigrations();

                if (pendingMigrations.Any())
                {
                    if (File.Exists(dbPath))
                    {
                        var backupFolder = Path.Combine(dbFolder, "Backups");
                        Directory.CreateDirectory(backupFolder);

                        File.Copy(
                            dbPath,
                            Path.Combine(backupFolder, $"inventory_{DateTime.Now:yyyyMMddHHmmss}.db"),
                            overwrite: false);
                    }

                    db.Database.Migrate();
                }

                //Check For Seeds
                DatabaseSeeder.Seed(db);
            }
            catch (Exception ex)
            {
                Serilog.Log.Fatal(ex, "Database migration failed");
                MessageBox.Show(
                    "حدث خطأ أثناء تحديث قاعدة البيانات.\nتواصل مع الدعم الفني.",
                    "Database Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
                return;
            }


            try
            {
                ApplicationConfiguration.Initialize();

                var loginForm = new frmLogin(serviceProvider);

                if(loginForm.ShowDialog()!= DialogResult.OK)
                    return;
                else
                    Application.Run(new FrmMain(serviceProvider));

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