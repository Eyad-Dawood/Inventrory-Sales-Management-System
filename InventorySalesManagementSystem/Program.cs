using DataAccessLayer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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


            var configuration = new ConfigurationBuilder()
                    .SetBasePath(AppContext.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: false)
                    .Build();

            var connectionString =
                configuration.GetConnectionString("HospitalDb");

            var options =
                new DbContextOptionsBuilder<InventoryDbContext>()
                    .UseSqlServer(connectionString)
                    .Options;

            var dbContext = new InventoryDbContext(options);


            ApplicationConfiguration.Initialize();
            Application.Run(new Form1());
        }
    }
}