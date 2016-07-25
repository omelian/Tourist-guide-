using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.Models;
using TouristGuide.UI.Models;

namespace TouristGuide.UI.Controllers
{
    /// <summary>
    /// Class which gets datas which are needed for menu
    /// </summary>
    public class MenuController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        public MenuController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Demo Method for getting datas 
        /// </summary>
        /// <returns>Menu datas</returns>
        // GET: /Menu/
        public JsonResult TestGetMenu()
        {
            List<MenuModel> datas = new List<MenuModel>();
            datas.Add(new MenuModel { ProfileId = 1, Name = "Piza", Price = 40, Description = "Pizzza carbonara with pasta and lets find a normal way to check this sheat if there are no bugs not too much text i think", PictureUrl = @"http://toninospizzaandpasta.com/wp-content/uploads/2014/04/pizza-page.png", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 6, Name = "Fish Soup", Price = 20, Description = "Soup with sea fish", PictureUrl = @"http://kitchenculinaire.com/wp-content/uploads/2010/03/DSC_0077.jpg", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 1, Name = "Piza", Price = 40, Description = "Pizzza carbonara with pasta", PictureUrl = @"http://toninospizzaandpasta.com/wp-content/uploads/2014/04/pizza-page.png", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 2, Name = "Pasta", Price = 30, Description = "Italian pasta", PictureUrl = @"http://chexfoods.com/wp-content/uploads/2013/04/white-pasta.jpg", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 3, Name = "Fish Soup", Price = 20, Description = "Soup with sea fish", PictureUrl = @"http://kitchenculinaire.com/wp-content/uploads/2010/03/DSC_0077.jpg", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 1, Name = "Piza", Price = 40, Description = "Pizzza carbonara with pasta", PictureUrl = @"http://toninospizzaandpasta.com/wp-content/uploads/2014/04/pizza-page.png", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 4, Name = "Piza", Price = 40, Description = "Pizzza carbonara with pasta", PictureUrl = @"http://toninospizzaandpasta.com/wp-content/uploads/2014/04/pizza-page.png", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 5, Name = "Pasta", Price = 30, Description = "Italian pasta", PictureUrl = @"http://chexfoods.com/wp-content/uploads/2013/04/white-pasta.jpg", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 6, Name = "Fish Soup", Price = 20, Description = "Soup with sea fish", PictureUrl = @"http://kitchenculinaire.com/wp-content/uploads/2010/03/DSC_0077.jpg", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 6, Name = "Fish Soup", Price = 20, Description = "Soup with sea fish", PictureUrl = @"http://kitchenculinaire.com/wp-content/uploads/2010/03/DSC_0077.jpg", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 7, Name = "Piza", Price = 40, Description = "Pizzza carbonara with pasta", PictureUrl = @"http://toninospizzaandpasta.com/wp-content/uploads/2014/04/pizza-page.png", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 1, Name = "Piza", Price = 40, Description = "Pizzza carbonara with pasta", PictureUrl = @"http://toninospizzaandpasta.com/wp-content/uploads/2014/04/pizza-page.png", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 8, Name = "Pasta", Price = 30, Description = "Italian pasta", PictureUrl = @"http://chexfoods.com/wp-content/uploads/2013/04/white-pasta.jpg", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 9, Name = "Fish Soup", Price = 20, Description = "Soup with sea fish", PictureUrl = @"http://kitchenculinaire.com/wp-content/uploads/2010/03/DSC_0077.jpg", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 10, Name = "Piza", Price = 40, Description = "Pizzza carbonara with pasta", PictureUrl = @"http://toninospizzaandpasta.com/wp-content/uploads/2014/04/pizza-page.png", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 11, Name = "Pasta", Price = 30, Description = "Italian pasta", PictureUrl = @"http://chexfoods.com/wp-content/uploads/2013/04/white-pasta.jpg", DishType = "Hot" });
            datas.Add(new MenuModel { ProfileId = 12, Name = "Fish Soup", Price = 20, Description = "Soup with sea fish", PictureUrl = @"http://kitchenculinaire.com/wp-content/uploads/2010/03/DSC_0077.jpg", DishType = "Hot" });

            return this.Json(datas, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Method for getting datas from database to Ui
        /// </summary>
        /// <returns>Menu datas</returns>
        public JsonResult GetMenu(int restaurantId)
        {
            var menu = this.unitOfWork.GuestBL.GetMenuItem(restaurantId).ToList();
            List<MenuModel> datas = new List<MenuModel>();
            foreach(var menuItem  in menu)
            {
                datas.Add(new MenuModel { Name = menuItem.Name, Price = menuItem.Price, Callories = menuItem.Calories, Description = menuItem.Description, PreparationTime = menuItem.DoneTime, DishType = menuItem.DishType, PictureUrl = menuItem.ItemPhoto, ProfileId = menuItem.Profile.ProfileId,MenuId = menuItem.RestaurantMenuItemId });
            }
            return this.Json(datas, JsonRequestBehavior.AllowGet);
        }
    }
}
