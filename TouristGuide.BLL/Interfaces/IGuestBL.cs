using System.Collections.Generic;
using TouristGuide.INF.Models;

namespace TouristGuide.BLL.Interfaces
{
    /// <summary>
    /// Defines methods for Guest business logic
    /// </summary>
    public interface IGuestBL
    {
        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns>Collection Profiles</returns>
        IEnumerable<Profile> GetAllRestaurants();

        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns>Collection Profiles</returns>
        IEnumerable<Profile> GetAllSightseeings();

        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns>Collection Profiles</returns>
        IEnumerable<Profile> GetAllLeisure();

        /// <summary>
        /// Get profile by id
        /// </summary>
        /// <param name="profileId">Id of profile</param>
        /// <returns>Collection Profiles</returns>
        Profile GetProfileById(int profileId);

        /// <summary>
        /// Gets all locations
        /// </summary>
        /// <returns>Collection of locations</returns>
        ICollection<Location> GetAllLocations();

        /// <summary>
        /// Gets location by id
        /// </summary>
        /// <param name="profileId">Id of location</param>
        /// <returns>Location item</returns>
        Location GetLocationById(int profileId);

        /// <summary>
        /// Get all menu items
        /// </summary>
        /// <param name="profileId">Id of profile</param>
        /// <returns>collection of restaurant item</returns>
        ICollection<RestaurantMenuItem> GetMenuItem(int profileId);

        /// <summary>
        /// Get article 
        /// </summary>
        /// <param name="attractionId">attraction id</param>
        /// <returns>Article<Reservation></returns>
        IEnumerable<Article> GetArticleByProfileId(int profileId);

        /// <summary>
        /// Get profile events 
        /// </summary>
        /// <param name="attractionId">sightseeing id</param>
        /// <returns>Events<Reservation></returns>
        IEnumerable<Event> GetEventsBySightseeingId(int sightseeingId);

        /// <summary>
        /// Get main photo of profile
        /// </summary>
        /// <param name="photoId">Photo id</param>
        /// <returns>Url of main photo</returns>
        string GetMainPhotoOfProfileById(int photoId);
    }
}
