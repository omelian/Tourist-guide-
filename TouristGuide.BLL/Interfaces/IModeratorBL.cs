using System.Collections.Generic;
using System.Threading.Tasks;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.INF.Models;

namespace TouristGuide.BLL.Interfaces
{
    /// <summary>
    /// Defines methods for Moderator business logic
    /// </summary>
    public interface IModeratorBL : IUserBL
    {
        /// <summary>
        /// Add profile news
        /// </summary>
        /// <param name="news">News item</param>
        /// <returns>Operation details</returns>
        OperationDetails AddNews(News news);

        /// <summary>
        /// Delete profile news
        /// </summary>
        /// <param name="newsId">ID of news item</param>
        /// <returns>Operation details</returns>
        OperationDetails DeleteNews(int newsId);

        /// <summary>
        /// Edit profile news
        /// </summary>
        /// <param name="news">News item</param>
        /// <returns>Operation details</returns>
        OperationDetails EditNews(News news);

        /// <summary>
        /// Add sightseeing article
        /// </summary>
        /// <param name="article">Article</param>
        /// <returns>Operation details</returns>
        OperationDetails AddSightseeingArticle(Article article);

        /// <summary>
        /// Edit sightseeing article
        /// </summary>
        /// <param name="article">Article</param>
        /// <returns>Operation details</returns>
        OperationDetails EditSightseeingArticle(Article article);

        /// <summary>
        /// Add photo to profile
        /// </summary>
        /// <param name="photo">ProfilePhoto item</param>
        /// <returns>Operation details</returns>
        OperationDetails AddPhoto(ProfilePhoto photo);


        /// <summary>
        /// Set main photo of profile
        /// </summary>
        /// <param name="photoId">ID of photo</param>
        /// <param name="profileId">ID of profile</param>
        /// <returns>Operation details</returns>
        OperationDetails SetMainPhotoOfProfile(int photoId, int profileId);

        /// <summary>
        /// Delete photo of profile
        /// </summary>
        /// <param name="profilePhotoId">ProfilePhoto item id</param>
        /// <returns>Operation details</returns>
        OperationDetails DeletePhoto(int profilePhotoId);

        /// <summary>
        /// Add new menu item to profile
        /// </summary>
        /// <param name="menuItem">menu item to add</param>
        /// <returns>Operation details</returns>
        OperationDetails AddMenuItem(RestaurantMenuItem menuItem);

        /// <summary>
        /// Delete profile news
        /// </summary>
        /// <param name="menuId">ID of menu item</param>
        /// <returns>Operation details</returns>
        OperationDetails DeleteMenu(int menuId);

        /// <summary>
        /// Edit menu item
        /// </summary>
        /// <param name="menu">Menu item</param>
        /// <returns>Operation details</returns>
        OperationDetails EditMenu(RestaurantMenuItem menu);
        
        /// <summary>
        /// gets moder profile
        /// </summary>
        /// <param name="userId">User to find</param>
        /// <returns>Profile</returns>
        Profile GetModerProfile(string userId);

        /// <summary>
        /// Get list of reservation for profile
        /// </summary>
        /// <param name="profileid">id of profile</param>
        /// <returns>ICollection<Reservation></returns>
        ICollection<Reservation> GetReservationsByProfileId(int profileid);

        /// <summary>
        /// Get list of meals for reservation
        /// </summary>
        /// <param name="reservationId">id of reservation</param>
        /// <returns>ICollection<RestaurantReservationMenuItem></returns>
        ICollection<ModerReservationMenuItem> GetMealsByReservationId(int reservationId);
        /// <summary>
        /// Edit if profile showed property
        /// </summary>
        /// <param name="IsShowed">IsShowed</param>
        /// <param name="profileId">Profile Id</param>
        /// <returns>Operation details</returns>
        OperationDetails EditIsShowed(bool IsShowed, int profileId);

        /// <summary>
        /// Add event to profile
        /// </summary>
        /// <param name="reservation">Event to add</param>
        /// <returns>Operation details</returns>
        OperationDetails AddEvent(Event profileEvent);

        /// <summary>
        /// Delete event from profile
        /// </summary>
        /// <param name="reservation">Event to delete</param>
        /// <returns>Operation details</returns>
        OperationDetails DeleteEvent(int eventId);

        /// <summary>
        /// Update event to profile
        /// </summary>
        /// <param name="reservation">Event to update</param>
        /// <returns>Operation details</returns>
        OperationDetails UpdateEvent(Event profileEvent);
    }
}
