using DbUp;
using DbUp.Engine;
using System.Reflection;

namespace ServiceManagement.Data.Database
{
    internal class DbUpgrade
    {
        private readonly UpgradeEngine _upgradeEngine;

        internal DbUpgrade(string connectionString)
        {
            _upgradeEngine = DeployChanges.To
                .SQLiteDatabase(connectionString)
                .WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
                .Build();
        }

        internal DatabaseUpgradeResult PerformUpgrade()
        {
            return _upgradeEngine.PerformUpgrade();
        }
    }
}