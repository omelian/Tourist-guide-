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
    /// Repository for addresses
    /// </summary>
    public class RestaurantReservationMenuItemsRepository : IRepository<RestaurantReservationMenuItem>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the RestaurantReservationMenuItemsRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public RestaurantReservationMenuItemsRepository(ApplicationContext db)
        {
            this.dataBase = db;
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Create new RestaurantReservationMenuItem item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(RestaurantReservationMenuItem item)
        {
            this.dataBase.RestaurantReservationMenuItems.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get RestaurantReservationMenuItem item by id
        /// </summary>
        /// <param name="id">Id of RestaurantReservationMenuItem item</param>
        /// <returns>RestaurantReservationMenuItem item</returns>
        public RestaurantReservationMenuItem Get(object id)
        {
            return this.dataBase.RestaurantReservationMenuItems.Find(id);
        }

        /// <summary>
        /// Get collection of all RestaurantReservationMenuItem items 
        /// </summary>
        /// <returns>Collection of RestaurantReservationMenuItem</returns>
        public IEnumerable<RestaurantReservationMenuItem> GetAll()
        {
            return this.dataBase.RestaurantReservationMenuItems;
        }

        /// <summary>
        /// Find RestaurantReservationMenuItem item by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of RestaurantReservationMenuItems </returns>
        public IEnumerable<RestaurantReservationMenuItem> Find(Func<RestaurantReservationMenuItem, bool> predicate)
        {
            return this.dataBase.RestaurantReservationMenuItems.Where(predicate).ToList();
        }

        /// <summary>
        /// Update RestaurantReservationMenuItem item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(RestaurantReservationMenuItem item)
        {
            var result = this.dataBase.RestaurantReservationMenuItems.SingleOrDefault(b => b.RestaurantReservationMenuItemId == item.RestaurantReservationMenuItemId);
            if (result != null)
            {
                result.Count = item.Count;
                result.MenuItemId = item.MenuItemId;
                result.ReservationId = item.ReservationId;
            }
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete RestaurantReservationMenuItem item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            RestaurantReservationMenuItem restaurantReservationMenuItem = this.dataBase.RestaurantReservationMenuItems.Find(id);
            if (restaurantReservationMenuItem != null)
            {
                this.dataBase.RestaurantReservationMenuItems.Remove(restaurantReservationMenuItem);
                this.dataBase.SaveChanges();
            }
        }
    }
}
