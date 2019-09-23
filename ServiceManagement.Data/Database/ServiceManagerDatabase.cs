using System;
using System.IO;
using System.Reflection;

namespace ServiceManagement.Data.Database
{
    internal static class ServiceManagerDatabase
    {
        private static readonly string _databaseName = "data.sqlite";
        private static readonly string _databaseFileLocation = GetExecutingDirectory();

        internal static string Name => _databaseName;
        internal static string FullPath => Path.Combine(_databaseFileLocation, _databaseName);
        internal static string ConnectionString => $"Data Source={FullPath};Version=3;";

        private static string GetExecutingDirectory()
        {
            var location = new Uri(Assembly.GetEntryAssembly().GetName().CodeBase);
            return new FileInfo(location.AbsolutePath).Directory.FullName;
        }
    }
}