using MongoDB.Bson;

namespace TestApp.Core.Interfaces.Data
{
    /// <summary>
    /// Represents common fields for MongoDB documents.
    /// </summary>
    public interface IDocument
    {
        /// <summary>
        /// Gets or sets the document identifier.
        /// </summary>
        ObjectId Id { get; set; }
    }
}
