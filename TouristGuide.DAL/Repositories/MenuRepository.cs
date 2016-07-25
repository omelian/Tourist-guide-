using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Models;

namespace TouristGuide.DAL.Repositories
{
    /// <summary>
    /// repository for menu
    /// </summary>
    public class MenuRepository : IRepository<RestaurantMenuItem>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the MenuRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public MenuRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new RestaurantMenu item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(RestaurantMenuItem item)
        {
            this.dataBase.Menus.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get Menu item by id
        /// </summary>
        /// <param name="id">Id of Menu item</param>
        /// <returns>Location item</returns>
        public RestaurantMenuItem Get(object id)
        {
            return this.dataBase.Menus.Find(id);
        }

        /// <summary>
        /// Get collection of all Menu items 
        /// </summary>
        /// <returns>Collection of Menus</returns>
        public IEnumerable<RestaurantMenuItem> GetAll()
        {
            return this.dataBase.Menus;
        }

        /// <summary>
        /// Find menu items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of Menus </returns>
        public IEnumerable<RestaurantMenuItem> Find(Func<RestaurantMenuItem, bool> predicate)
        {
            return this.dataBase.Menus.Where(predicate).ToList();
        }

        /// <summary>
        /// Update Menu item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(RestaurantMenuItem item)
        {
            var result = this.dataBase.Menus.FirstOrDefault(p => p.RestaurantMenuItemId == item.RestaurantMenuItemId);
            if (result != null)
            {
                result.Calories = item.Calories;
                result.Description = item.Description;
                result.DishType = item.DishType;
                result.DoneTime = item.DoneTime;
                result.ItemPhoto = item.ItemPhoto;
                result.Name = item.Name;
                result.Price = item.Price;
            }

            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Menu item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            RestaurantMenuItem menu = this.dataBase.Menus.Find(id);
            if (menu != null)
            {
                this.dataBase.Menus.Remove(menu);
                this.dataBase.SaveChanges();
            }
        }
    }
}
