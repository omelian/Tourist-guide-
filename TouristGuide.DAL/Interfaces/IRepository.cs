using System;
using System.Collections.Generic;
using TouristGuide.INF.EntityFramework;

namespace TouristGuide.DAL.Interfaces
{
    /// <summary>
    /// Defines Repository for T
    /// </summary>
    /// <typeparam name="T">Type of Repository</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Create new T item
        /// </summary>
        /// <param name="item">Item to create</param>
        void Create(T item);

        /// <summary>
        /// Get T item by id
        /// </summary>
        /// <param name="id">Id of T item</param>
        /// <returns>T item</returns>
        T Get(object id);

        /// <summary>
        /// Get collection of all T items 
        /// </summary>
        /// <returns>Collection of items</returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// Find T item by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns>Collection of items</returns>
        IEnumerable<T> Find(Func<T, bool> predicate);

        /// <summary>
        /// Update T item
        /// </summary>
        /// <param name="item">Item to update</param>
        void Update(T item);

        /// <summary>
        /// Delete T item by id
        /// </summary>
        /// <param name="id">Id of item</param>
        void Delete(object id);
    }
}
