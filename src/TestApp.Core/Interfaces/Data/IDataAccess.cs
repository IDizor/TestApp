using MongoDB.Driver;

namespace TestApp.Core.Interfaces.Data
{
    /// <summary>
    /// Represents fields to access Mongo database.
    /// </summary>
    public interface IDataAccess
    {
        /// <summary>
        /// Gets the Mongo database.
        /// </summary>
        IMongoDatabase Database { get; }
    }
}
