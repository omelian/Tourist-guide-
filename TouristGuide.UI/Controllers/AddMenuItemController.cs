using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.Models;

namespace TouristGuide.UI.Controllers
{
    public class AddMenuItemController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        public AddMenuItemController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Method
        /// </summary>
        /// <returns>coords in json type</returns>
        [HttpPost]
        public JsonResult AddMenu(int restaurantId, string name, string description, double price, int calories, double preparationTime, string dishType, string photoUrl)
        {
            RestaurantMenuItem newMenuItem = new RestaurantMenuItem();
            newMenuItem.Profile = new Profile() { ProfileId = (int)restaurantId };
            newMenuItem.Name = name;
            newMenuItem.Description = description;
            newMenuItem.Price = price;
            newMenuItem.Calories = calories;
            newMenuItem.DoneTime = preparationTime.ToString();
            newMenuItem.DishType = dishType;
            newMenuItem.ItemPhoto = photoUrl;
            OperationDetails operationDetails = this.unitOfWork.ModeratorBL.AddMenuItem(newMenuItem);
            return null;
        }

        /// <summary>
        /// Delete menu item from restaurant
        /// </summary>
        /// <param name="menuId">ID of menu item</param>
        /// <param name="restaurantId">ID of restaurant</param>
        [HttpPost]
        public void DeleteMenu(int menuId, int restaurantId)
        {
            this.unitOfWork.ModeratorBL.DeleteMenu(menuId);
        }

        /// <summary>
        /// Update new menu
        /// </summary>
        [HttpPost]
        public void UpdateMenu(string name,double price,string pictureUrl,string description,int calories,double preparationTime,int id,string dishType,int menuId)
        {
            RestaurantMenuItem menuItem = new RestaurantMenuItem();
            menuItem.Profile = new Profile() { ProfileId = (int)id };
            menuItem.Name = name;
            menuItem.Description = description;
            menuItem.Price = price;
            menuItem.Calories = calories;
            menuItem.DoneTime = preparationTime.ToString();
            menuItem.DishType = dishType;
            menuItem.ItemPhoto = pictureUrl;
            menuItem.RestaurantMenuItemId = menuId;
            this.unitOfWork.ModeratorBL.EditMenu(menuItem);
        }
    }
}
