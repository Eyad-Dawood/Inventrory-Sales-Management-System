using DataAccessLayer;
using DataAccessLayer.Abstractions;
using DataAccessLayer.Entities;
using DataAccessLayer.Repos;
using LogicLayer.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.Logging;
using Serilog;
using Serilog.Sinks.File;

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

            Serilog.Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Information()
            .WriteTo.File(
                path: "Logs\\app-.log",
                rollingInterval: RollingInterval.Day,
                retainedFileCountLimit: 14,
                shared: true)
            .CreateLogger();


            var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

            var connectionString =
                configuration.GetConnectionString("HospitalDb");


            var services = new ServiceCollection();

            services.AddDbContextFactory<InventoryDbContext>(options =>
            options.UseSqlServer(connectionString));

            var serviceProvider = services.BuildServiceProvider();

            try
            {
                ApplicationConfiguration.Initialize();
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