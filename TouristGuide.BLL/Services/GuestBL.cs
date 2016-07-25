using System.Collections.Generic;
using System.Linq;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.Enums;
using TouristGuide.INF.Models;

namespace TouristGuide.BLL.Services
{
    /// <summary>
    /// Class that implements Guest business logic
    /// </summary>
    public class GuestBL : IGuestBL
    {
        /// <summary>
        /// reference to data base controller
        /// </summary>
        private IDataBaseManager dataBase;

        /// <summary>
        /// Initializes a new instance of the GuestBL class.
        /// </summary>
        public GuestBL(IDataBaseManager db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns>Collection Restaurants</returns>
        public IEnumerable<Profile> GetAllRestaurants()
        {
            lock (this.dataBase)
            {
                ICollection < Profile > profiles = this.dataBase.ProfileManager.GetAll().ToList();
                IEnumerable<Profile> restaurants = profiles.Where(restaurant =>( restaurant.Type.Name == ProfileTypeEnum.Restaurant.ToString()));
                return restaurants;
            }
        }

        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns>Collection Sightseeings</returns>
        public IEnumerable<Profile> GetAllSightseeings()
        {
            lock (this.dataBase)
            {
                ICollection<Profile> profiles = this.dataBase.ProfileManager.GetAll().ToList();
                IEnumerable<Profile> sightseeings = profiles.Where(sightseeing => (sightseeing.Type.Name == ProfileTypeEnum.Sightseeing.ToString()));
                return sightseeings;
            }
        }

        /// <summary>
        /// Gets all profiles
        /// </summary>
        /// <returns>Collection Leisure</returns>
        public IEnumerable<Profile> GetAllLeisure()
        {
            lock (this.dataBase)
            {
                ICollection<Profile> profiles = this.dataBase.ProfileManager.GetAll().ToList();
                IEnumerable<Profile> leisures = profiles.Where(leisure => (leisure.Type.Name == ProfileTypeEnum.Leisure.ToString()));
                return leisures;
            }
        }

        /// <summary>
        /// Gets all locations
        /// </summary>
        /// <returns>Collection of locations</returns>
        public ICollection<Location> GetAllLocations()
        {
            lock (this.dataBase)
            {
                return this.dataBase.LocationManager.GetAll().ToList();
            }
        }

        /// <summary>
        /// Gets profile by id
        /// </summary>
        /// <param name="profileId">profile id</param>
        /// <returns>Profile by id</returns>
        public Profile GetProfileById(int profileId)
        {
            lock (this.dataBase)
            {
                return this.dataBase.ProfileManager.Get(profileId);
            }
        }

        /// <summary>
        /// Gets location by id
        /// </summary>
        /// <param name="profileId">profile id</param>
        /// <returns>Location by id</returns>
        public Location GetLocationById(int profileId)
        {
            lock (this.dataBase)
            {
                var profile = this.dataBase.ProfileManager.Get(profileId);
                var address = this.dataBase.AddressManager.Get(profile.Address.AddressId);
                return this.dataBase.LocationManager.Get(address.Location.LocationId);
            }
        }

        /// <summary>
        /// get menu by id
        /// </summary>
        /// <param name="profileId">profile id</param>
        /// <returns>Collection of menu items</returns>
        public ICollection<RestaurantMenuItem> GetMenuItem(int profileId)
        {
            lock (this.dataBase)
            {
                var items = this.dataBase.MenuManager.Find(t => t.Profile.ProfileId == profileId).ToList();
                return items;
            }
        }

        /// <summary>
        /// Get article 
        /// </summary>
        /// <param name="attractionId">Sightseeing id</param>
        /// <returns>Article<Reservation></returns>
        public IEnumerable<Article> GetArticleByProfileId(int profileId)
        {
            lock (this.dataBase)
            {
                IEnumerable<Article> article = this.dataBase.ArticleManager.Find(t => t.Profile.ProfileId == profileId);
                if (article != null)
                {
                    return article;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get events 
        /// </summary>
        /// <param name="attractionId">Sightseeing id</param>
        /// <returns>Event<Events></returns>
        public IEnumerable<Event> GetEventsBySightseeingId(int sightseeingId)
        {
            lock (this.dataBase)
            {
                IEnumerable<Event> profileEvents = this.dataBase.EventManager.Find(t => t.Profile.ProfileId == sightseeingId);
                if (profileEvents != null)
                {
                    return profileEvents;
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get main photo of profile
        /// </summary>
        /// <param name="photoId">Photo id</param>
        /// <returns>Url of main photo</returns>
        public string GetMainPhotoOfProfileById(int photoId)
        {
            lock (this.dataBase)
            {
                string Url = "";
                ProfilePhoto profilePhoto = this.dataBase.ProfilePhotoManager.Get(photoId);
                if (profilePhoto != null)
                {
                    Url = profilePhoto.Url;
                }
                else
                {
                    Url = (string)"http://www.cliparthut.com/clip-arts/708/office-building-clip-art-708383.jpg";
                }

                return Url;
            }
        }
    }
}