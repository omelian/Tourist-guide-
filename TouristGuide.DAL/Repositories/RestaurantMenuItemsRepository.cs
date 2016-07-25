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
    /// Repository for restaurant menu items
    /// </summary>
    public class RestaurantMenuItemsRepository : IRepository<RestaurantMenuItem>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the RestaurantMenuItemsRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public RestaurantMenuItemsRepository(ApplicationContext db)
        {
            this.dataBase = db;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Create new RestaurantMenuItem item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(RestaurantMenuItem item)
        {
            this.dataBase.Menus.Add(item);
        }

        /// <summary>
        /// Get RestaurantMenuItem item by id
        /// </summary>
        /// <param name="id">Id of RestaurantMenuItem item</param>
        /// <returns>RestaurantMenuItem item</returns>
        public RestaurantMenuItem Get(object id)
        {
            return this.dataBase.Menus.Find(id);
        }

        /// <summary>
        /// Get collection of all RestaurantMenuItem items 
        /// </summary>
        /// <returns>Collection of RestaurantMenuItems</returns>
        public IEnumerable<RestaurantMenuItem> GetAll()
        {
            return this.dataBase.Menus;
        }

        /// <summary>
        /// Find RestaurantMenuItem item by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of RestaurantMenuItems </returns>
        public IEnumerable<RestaurantMenuItem> Find(Func<RestaurantMenuItem, bool> predicate)
        {
            return this.dataBase.Menus.Where(predicate).ToList();
        }

        /// <summary>
        /// Update RestaurantMenuItem item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(RestaurantMenuItem item)
        {
            this.dataBase.Entry(item).State = EntityState.Modified;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete RestaurantMenuItem item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            RestaurantMenuItem restaurantMenuItem = this.dataBase.Menus.Find(id);
            if (restaurantMenuItem != null)
            {
                this.dataBase.Menus.Remove(restaurantMenuItem);
                this.dataBase.SaveChanges();
            }
        }
    }
}
