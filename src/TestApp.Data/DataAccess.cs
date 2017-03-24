using System.Collections.Generic;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using TestApp.Core.Configuration;
using TestApp.Core.Interfaces.Data;
using TestApp.Core.Models;

namespace TestApp.Data
{
    public class DataAccess : IDataAccess
    {
        public IMongoDatabase Database { get; private set; }

        public DataAccess(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            Database = client.GetDatabase(settings.Value.DatabaseName);
        }
    }
}
