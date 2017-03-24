﻿using Microsoft.Extensions.Options;
using MongoDB.Driver;
using TestApp.Core.Configuration;
using TestApp.Core.Interfaces.Data;

namespace TestApp.Data
{
    /// <summary>
    /// Implements functionality to access Mongo database.
    /// </summary>
    /// <seealso cref="TestApp.Core.Interfaces.Data.IDataAccess" />
    public class DataAccess : IDataAccess
    {
        /// <summary>
        /// Gets the Mongo database.
        /// </summary>
        public IMongoDatabase Database { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="DataAccess"/> class.
        /// </summary>
        /// <param name="settings">The connection settings.</param>
        public DataAccess(IOptions<MongoSettings> settings)
        {
            var client = new MongoClient(settings.Value.ConnectionString);
            Database = client.GetDatabase(settings.Value.DatabaseName);
        }
    }
}
