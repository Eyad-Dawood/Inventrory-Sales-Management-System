using DataAccessLayer;
using DataAccessLayer.Entities;
using DataAccessLayer.Entities.Products;
using System.Linq;



namespace DataAccessLayer.DesignTimeOnly
{
    public static class DatabaseSeeder
    {
        public static void Seed(InventoryDbContext db)
        {

            if (!db.MasurementUnits.Any())
            {
                db.MasurementUnits.AddRange(
                    new MasurementUnit { UnitName = "قطعة" },
                    new MasurementUnit { UnitName = "كجم" },
                    new MasurementUnit { UnitName = "متر" },
                    new MasurementUnit { UnitName = "لتر" }
                );
            }

            if (!db.Towns.Any())
            {
                db.Towns.Add(new Town { TownName = "عام" });
            }

            db.SaveChanges();

            if (!db.Users.Any())
            {
                db.Users.Add(new User
                {
                    Username = "admin",
                    Password = "123456", 
                    IsActive = true,
                    Permissions = Permission.Admin 
                });
            }

            db.SaveChanges();
        }
    }
}
