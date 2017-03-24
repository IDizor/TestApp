using System.Collections.Generic;

using MongoDB.Bson;
using MongoDB.Driver;
using TestApp.Core.Interfaces.Data;

namespace TestApp.Core.Interfaces.Repositories
{
    /// <summary>
    /// Represents base repository interface.
    /// </summary>
    /// <typeparam name="T">Some IDocument.</typeparam>
    public interface IRepositoryBase<T> where T : IDocument
    {
        /// <summary>
        /// Selects all DB documents.
        /// </summary>
        /// <returns>The list of items.</returns>
        IEnumerable<T> GetAllDocuments();

        /// <summary>
        /// Gets the single DB document by identifier.
        /// </summary>
        /// <param name="id">The document identifier.</param>
        /// <returns>Selected docuemnt.</returns>
        T GetDocument(ObjectId id);

        /// <summary>
        /// Creates new DB document.
        /// </summary>
        /// <param name="item">The DB document.</param>
        /// <returns>Created document.</returns>
        T CreateDocument(T item);

        /// <summary>
        /// Updates the existing DB document.
        /// </summary>
        /// <param name="id">The document identifier to update.</param>
        /// <param name="item">The document with new data.</param>
        void UpdateDocument(ObjectId id, T item);

        /// <summary>
        /// Deletes the DB document.
        /// </summary>
        /// <param name="id">The document identifier.</param>
        void DeleteDocument(ObjectId id);
    }
}
