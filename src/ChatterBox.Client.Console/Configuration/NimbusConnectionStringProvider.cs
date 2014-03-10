using System;
using System.IO;

namespace ChatterBox.Client.Console.Configuration
{
    public class NimbusConnectionStringProvider
    {
        private readonly string _nimbusConnectionString;
        private readonly Lazy<string> _connectionString;

        private const string _localFilePath = @"C:\Artifacts\ChatterBoxConnectionString.txt";

        public NimbusConnectionStringProvider(NimbusConnectionStringSetting nimbusConnectionString)
        {
            _nimbusConnectionString = nimbusConnectionString;
            _connectionString = new Lazy<string>(FetchConnectionString);
        }

        public string ConnectionString
        {
            get { return _connectionString.Value; }
        }

        private string FetchConnectionString()
        {
            // this file can (and usually does) have passwords in it so it's important to have it NOT under source control anywhere
            return File.Exists(_localFilePath)
                ? File.ReadAllText(_localFilePath).Trim()
                : _nimbusConnectionString;
        }
    }
}