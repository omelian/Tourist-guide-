using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using TouristGuide.BLL.DBContext;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.BLL.Interfaces;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.INF.Enums;
using TouristGuide.INF.Models;

namespace TouristGuide.BLL.Services
{
    /// <summary>
    /// Class that implements Moderator business logic
    /// </summary>
    public class ModeratorBL : UserBL, IModeratorBL
    {
        /// <summary>
        /// reference to data base controller
        /// </summary>
        private IDataBaseManager dataBase;

        /// <summary>
        /// Initializes a new instance of the ModeratorBL class.
        /// </summary>
        public ModeratorBL(IDataBaseManager db)
            : base(db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Add profile news
        /// </summary>
        /// <param name="news">News item</param>
        /// <returns>Operation details</returns>
        public OperationDetails AddNews(News news)
        {
            lock (this.dataBase)
            {
                Profile profile = this.GetProfileById(news.Profile.ProfileId);
                if (profile != null)
                {
                    news.Profile = profile;
                    this.dataBase.NewsManager.Create(news);
                    return new OperationDetails(true, "News item successfully created!", "News");
                }
                else
                {
                    return new OperationDetails(false, "Profile doesn`t exists!", "News");
                }
            }
        }
        
        /// <summary>
        /// Add sightseeing article
        /// </summary>
        /// <param name="article">Article</param>
        /// <returns>Operation details</returns>
        public OperationDetails AddSightseeingArticle(Article article)
        {
            lock(this.dataBase)
            {
                Profile profile = this.GetProfileById(article.Profile.ProfileId);
                if(profile != null)
                {
                    article.Profile = profile;
                    this.dataBase.ArticleManager.Create(article);
                    return new OperationDetails(true, "Article has been successfully created!", "Article");
                }
                else
                {
                    return new OperationDetails(false, "Profile doesn't exist!", "Article");
                }
            }
        }

        /// <summary>
        /// Edit sightseeing article
        /// </summary>
        /// <param name="article">Article</param>
        /// <returns>Operation details</returns>
        public OperationDetails EditSightseeingArticle(Article article)
        {
            lock(this.dataBase)
            {
                this.dataBase.ArticleManager.Update(article);
                return new OperationDetails(true, "Article has been successfully updated!", "Article");
            }
        }

        /// <summary>
        /// Delete profile news
        /// </summary>
        /// <param name="newsId">ID of news item</param>
        /// <returns>Operation details</returns>
        public OperationDetails DeleteNews(int newsId)
        {
            lock (this.dataBase)
            {
                this.dataBase.NewsManager.Delete(newsId);
                return new OperationDetails(true, "News item successfully deleted!", "News");
            }
        }

        /// <summary>
        /// Edit profile news
        /// </summary>
        /// <param name="news">News item</param>
        /// <returns>Operation details</returns>
        public OperationDetails EditNews(News news)
        {
            lock (this.dataBase)
            {
                     this.dataBase.NewsManager.Update(news);
                     return new OperationDetails(true, "News item successfully updated!", "News");
            }
        }

        /// <summary>
        /// Add photo to profile
        /// </summary>
        /// <param name="photo">ProfilePhoto item</param>
        /// <returns>Operation details</returns>
        public OperationDetails AddPhoto(ProfilePhoto photo)
        {
            lock (this.dataBase)
            {
                Profile profile = this.GetProfileById(photo.Profile.ProfileId);
                if (profile != null)
                {
                    photo.Profile = profile;
                    this.dataBase.ProfilePhotoManager.Create(photo);
                    return new OperationDetails(true, "ProfilePhoto item successfully created!", "ProfilePhoto");
                }
                else
                {
                    return new OperationDetails(false, "Profile doesn`t exists!", "ProfilePhoto");
                }
            }
        }


        /// <summary>
        /// Add photo to profile
        /// </summary>
        /// <param name="photo">ProfilePhoto item</param>
        /// <returns>Operation details</returns>
        public Profile GetModerProfile(string userId)
        {
            lock (this.dataBase)
            {
                return this.dataBase.UserManager.FindById(userId).ManageProfile;     
            }
        }


        /// <summary>
        /// Delete photo of profile
        /// </summary>
        /// <param name="profilePhotoId">ProfilePhoto item id</param>
        /// <returns>Operation details</returns>
        public OperationDetails DeletePhoto(int profilePhotoId)
        {
            lock (this.dataBase)
            {
                this.dataBase.ProfilePhotoManager.Delete(profilePhotoId);
                return new OperationDetails(true, "ProfilePhoto item successfully deleted!", "ProfilePhoto");
            }
        }

        /// <summary>
        /// Add menu item to profile
        /// </summary>
        /// <param name="menuItem">Menu item to add</param>
        /// <returns>Operation details</returns>
        public OperationDetails AddMenuItem(RestaurantMenuItem menuItem)
        {
            lock (this.dataBase)
            {
                Profile profile = this.GetProfileById(menuItem.Profile.ProfileId);
                if (profile != null)
                {
                    menuItem.Profile = profile;
                    this.dataBase.MenuManager.Create(menuItem);
                    return new OperationDetails(true, "Menu successfully created!", "Menus");
                }
                else
                {
                    return new OperationDetails(false, "Profile doesn`t exists!", "Menus");
                }
            }
        }

        /// <summary>
        /// Delete menu item of profile
        /// </summary>
        /// <param name="menu id">Menu item id</param>
        /// <returns>Operation details</returns>
        public OperationDetails DeleteMenu(int menuId)
        {
            lock (this.dataBase)
            {
                this.dataBase.MenuManager.Delete(menuId);
                return new OperationDetails(true, "Menu item successfully deleted!", "Menu");
            }
        }

        /// <summary>
        /// Edit Menu item of profile
        /// </summary>
        /// <param name="menu">Menu item</param>
        /// <returns>Operation details</returns>
        public OperationDetails EditMenu(RestaurantMenuItem menu)
        {
            lock (this.dataBase)
            {
                this.dataBase.MenuManager.Update(menu);
                return new OperationDetails(true, "Menu item successfully updated!", "Menu");
            }
        }

        /// <summary>
        /// Set main photo of profile
        /// </summary>
        /// <param name="photoId">ID of photo</param>
        /// <param name="profileId">ID of profile</param>
        /// <returns>Operation details</returns>
        public OperationDetails SetMainPhotoOfProfile(int photoId, int profileId)
        {
            lock (this.dataBase)
            {
                Profile profile = this.GetProfileById(profileId);
                if (profile != null)
                {
                    profile.MainPhoto = photoId;
                    this.dataBase.ProfileManager.Update(profile);
                    return new OperationDetails(true, "Main photo of profile successfully changed!", "Profile photo");
                }
                else
                {
                    return new OperationDetails(false, "Profile doesn`t exists!", "Profile photo");
                }
            }
        }

        

        /// <summary>
        /// Get list of reservation for profile
        /// </summary>
        /// <param name="profileid">id of profile</param>
        /// <returns>ICollection<Reservation></returns>
        public ICollection<Reservation> GetReservationsByProfileId(int profileid)
        {
            lock (this.dataBase)
            {
                Profile profile = this.dataBase.ProfileManager.GetAll().First(x => x.ProfileId == profileid);
                if (profile != null)
                {
                    return profile.Reservations.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

       




        /// <summary>
        /// Get list of meals for reservation
        /// </summary>
        /// <param name="reservationId">id of reservation</param>
        /// <returns>ICollection<RestaurantReservationMenuItem></returns>
        public ICollection<ModerReservationMenuItem> GetMealsByReservationId(int reservationId)
        {
            lock (this.dataBase)
            {
                List<RestaurantReservationMenuItem> reservationlist =  this.dataBase.RestaurantReservationMenuItemsManager.GetAll().Where(x => x.ReservationId == reservationId).ToList();
              List<ModerReservationMenuItem> finalList = new List<ModerReservationMenuItem>();
                foreach(RestaurantReservationMenuItem x in reservationlist)
                {
                    ModerReservationMenuItem toAdd = new ModerReservationMenuItem();
                    toAdd.Count = x.Count;
                    toAdd.MenuItemName = this.dataBase.MenuManager.GetAll().Where(xx => xx.RestaurantMenuItemId == x.MenuItemId).First().Name;
                    toAdd.MenuItemURL = this.dataBase.MenuManager.GetAll().Where(xx => xx.RestaurantMenuItemId == x.MenuItemId).First().ItemPhoto;
                    finalList.Add(toAdd);
                }

                return finalList;
            }
        }
        public OperationDetails EditIsShowed(bool IsShowed, int profileId)
        {
            lock (this.dataBase)
            {
                Profile localProfile = this.dataBase.ProfileManager.Get(profileId);
                localProfile.IsShowed = IsShowed;
                this.dataBase.ProfileManager.Update(localProfile);
                return new OperationDetails(true, "Profile changed", string.Empty);
            }
        }

        // <summary>
        /// Add event to profile
        /// </summary>
        /// <param name="reservation">Event to add</param>
        /// <returns>Operation details</returns>
        public OperationDetails AddEvent(Event profileEvent)
        {
            lock (this.dataBase)
            {
                Profile profile = this.GetProfileById(profileEvent.Profile.ProfileId);
                if (profile != null)
                {


                    profileEvent.Profile = profile;
                    this.dataBase.EventManager.Create(profileEvent);
                    return new OperationDetails(true, "Event successfully created!", "Events");
                }
                else
                {
                    return new OperationDetails(false, "Profile doesn`t exists!", "Events");
                }
            }
        }

        /// <summary>
        /// not ready
        /// </summary>
        /// <param name="reservation">event for delete</param>
        /// <returns>Operation details for delete event</returns>
        public OperationDetails DeleteEvent(int eventId)
        {
            lock (this.dataBase)
            {
                this.dataBase.EventManager.Delete(eventId);
                return new OperationDetails(true, "Event item successfully deleted!", "Event");
            }

        }


        /// <summary>
        /// Update event to profile
        /// </summary>
        /// <param name="reservation">Event to update</param>
        /// <returns>Operation details</returns>
        public OperationDetails UpdateEvent(Event profileEvent)
        {
            lock (this.dataBase)
            {
                lock (this.dataBase)
                {
                    this.dataBase.EventManager.Update(profileEvent);
                    return new OperationDetails(true, "Event item successfully updated!", "Event");
                }
            }
        }
    }
}