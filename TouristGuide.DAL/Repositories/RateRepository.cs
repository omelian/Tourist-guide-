using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Models;

namespace TouristGuide.DAL.Repositories
{
    /// <summary>
    /// Repository for rates
    /// </summary>
    public class RateRepository : IRepository<Rate>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the RateRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public RateRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new Rate item
        /// </summary>
        /// <param name="item">Item to create</param>
        public virtual void Create(Rate item)
        {
            this.dataBase.Rates.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get Rate item by id
        /// </summary>
        /// <param name="id">Id of Rate item</param>
        /// <returns>Rate item</returns>
        public virtual Rate Get(object id)
        {
            return this.dataBase.Rates.Find(id);
        }

        /// <summary>
        /// Get collection of all Rate items 
        /// </summary>
        /// <returns>Collection of Rates</returns>
        public virtual IEnumerable<Rate> GetAll()
        {
            return this.dataBase.Rates;
        }

        /// <summary>
        /// Find Rate items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of Rates </returns>
        public virtual IEnumerable<Rate> Find(Func<Rate, bool> predicate)
        {
            return this.dataBase.Rates.Where(predicate).ToList();
        }

        /// <summary>
        /// Update Rate item
        /// </summary>
        /// <param name="item">Item to update</param>
        public virtual void Update(Rate item)
        {
            this.dataBase.Entry(item).State = EntityState.Modified;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Rate item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public virtual void Delete(object id)
        {
            Rate rate = this.dataBase.Rates.Find(id);
            if (rate != null)
            {
                this.dataBase.Rates.Remove(rate);
                this.dataBase.SaveChanges();
            }
        }
    }
}
