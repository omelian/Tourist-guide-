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
    /// Repository for google maps locations
    /// </summary>
    public class LocationRepository : IRepository<Location>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the LocationRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public LocationRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new Location item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(Location item)
        {
            this.dataBase.Locations.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get Location item by id
        /// </summary>
        /// <param name="id">Id of Location item</param>
        /// <returns>Location item</returns>
        public Location Get(object id)
        {
            return this.dataBase.Locations.Find(id);
        }

        /// <summary>
        /// Get collection of all Location items 
        /// </summary>
        /// <returns>Collection of Locations</returns>
        public IEnumerable<Location> GetAll()
        {
            return this.dataBase.Locations;
        }

        /// <summary>
        /// Find Location items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of Locations </returns>
        public IEnumerable<Location> Find(Func<Location, bool> predicate)
        {
            return this.dataBase.Locations.Where(predicate).ToList();
        }

        /// <summary>
        /// Update Location item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(Location item)
        {
            this.dataBase.Entry(item).State = EntityState.Modified;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Location item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            Location location = this.dataBase.Locations.Find(id);
            if (location != null)
            {
                this.dataBase.Locations.Remove(location);
                this.dataBase.SaveChanges();
            }
        }
    }
}
