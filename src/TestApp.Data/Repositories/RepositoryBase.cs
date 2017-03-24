using System.Collections.Generic;
using System.Linq;

using MongoDB.Bson;
using MongoDB.Driver;
using TestApp.Core.Interfaces.Data;
using TestApp.Core.Interfaces.Repositories;

namespace TestApp.Data.Repositories
{
    /// <summary>
    /// Represents common functionality for all DB collections.
    /// </summary>
    /// <typeparam name="T">The specific DB document type.</typeparam>
    public class RepositoryBase<T> : IRepositoryBase<T> where T : IDocument
    {
        #region Private_Fields        
        /// <summary>
        /// The documents collection name.
        /// </summary>
        private readonly string collectionName;

        /// <summary>
        /// The database access provider.
        /// </summary>
        private readonly IDataAccess dataAccess;
        #endregion

        #region Protected_Properties        
        /// <summary>
        /// Gets the DB documents collection.
        /// </summary>
        protected IMongoCollection<T> Documents
        {
            get
            {
                return this.dataAccess.Database.GetCollection<T>(this.collectionName);
            }
        }
        #endregion

        #region Constructor        
        /// <summary>
        /// Initializes a new instance of the <see cref="RepositoryBase{T}"/> class.
        /// </summary>
        /// <param name="dataAccess">The database access provider.</param>
        public RepositoryBase(IDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;

            // pluralize document type name - lets just add "s" for this test application :) 
            this.collectionName = typeof(T).Name + "s";
        }
        #endregion

        #region Public_Methods        
        /// <summary>
        /// Selects all DB documents.
        /// </summary>
        /// <returns>The list of items.</returns>
        public IEnumerable<T> GetAllDocuments()
        {
            return this.Documents
                .Find(i => true)
                .ToList();
        }

        /// <summary>
        /// Gets the single DB document by identifier.
        /// </summary>
        /// <param name="id">The document identifier.</param>
        /// <returns>Selected docuemnt.</returns>
        public T GetDocument(ObjectId id)
        {
            return this.Documents
                .Find(i => i.Id.Equals(id))
                .SingleOrDefault();
        }

        /// <summary>
        /// Creates new DB document.
        /// </summary>
        /// <param name="item">The DB document.</param>
        /// <returns>Created document.</returns>
        public T CreateDocument(T item)
        {
            this.Documents.InsertOne(item);

            return item;
        }

        /// <summary>
        /// Updates the existing DB document.
        /// </summary>
        /// <param name="id">The document identifier to update.</param>
        /// <param name="item">The document with new data.</param>
        public void UpdateDocument(ObjectId id, T item)
        {
            item.Id = id;
            this.Documents.ReplaceOne(i => i.Id.Equals(id), item, new UpdateOptions { IsUpsert = false });
        }

        /// <summary>
        /// Deletes the DB document.
        /// </summary>
        /// <param name="id">The document identifier.</param>
        public void DeleteDocument(ObjectId id)
        {
            this.Documents.DeleteOne(i => i.Id.Equals(id));
        }
        #endregion
    }
}
