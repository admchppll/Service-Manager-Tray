using System.Data.SQLite;
using System.Threading.Tasks;

namespace ServiceManagement.Data.Database
{
    public static class DbInitialise
    {
        public static async Task Initialise()
        {
            await Task.Run(() =>
            {
                CreateDatabase();
                UpdateDatabase();
            });
        }

        private static void CreateDatabase()
        {
            //SQLiteConnection.CreateFile(ServiceManagerDatabase.FullPath);
        }

        private static void UpdateDatabase()
        {
            var upgrader = new DbUpgrade(ServiceManagerDatabase.ConnectionString);
            upgrader.PerformUpgrade();
        }
    }
}