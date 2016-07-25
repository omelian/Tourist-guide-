using System;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.Identity;
using TouristGuide.INF.Models;

namespace TouristGuide.BLL.Interfaces
{
    /// <summary>
    /// Defines properties and methods for work with repositories
    /// </summary>
    public interface IDataBaseManager
    {
        /// <summary>
        /// Gets the manager for Users
        /// </summary>
        ApplicationUserManager UserManager { get; }

        /// <summary>
        /// Gets the manager for roles
        /// </summary>
        ApplicationRoleManager RoleManager { get; }

        /// <summary>
        /// Gets the repository for addresses
        /// </summary>
        IRepository<Address> AddressManager { get; }

        /// <summary>
        /// Gets the repository for BannedProfiles
        /// </summary>
        IRepository<BannedProfile> BannedProfileManager { get; }

        /// <summary>
        ///  Gets the repository for BannedUsers
        /// </summary>
        IRepository<BannedUser> BannedUserManager { get; }

        /// <summary>
        /// Gets the repository for Comments
        /// </summary>
        IRepository<Comment> CommentManager { get; }

        /// <summary>
        /// Gets the repository for Companies
        /// </summary>
        IRepository<Company> CompanyManager { get; }

        /// <summary>
        /// Gets the repository for Locations
        /// </summary>
        IRepository<Location> LocationManager { get; }

        /// <summary>
        /// Gets the repository for Profiles
        /// </summary>
        IRepository<Profile> ProfileManager { get; }

        /// <summary>
        /// Gets the repository for ProfileTypes
        /// </summary>
        IRepository<ProfileType> ProfileTypeManager { get; }

        /// <summary>
        /// Gets the repository for Rates
        /// </summary>
        IRepository<Rate> RateManager { get; }

        /// <summary>
        /// Gets the repository for News
        /// </summary>
        IRepository<News> NewsManager { get; }

        /// <summary>
        /// Gets the repository for Reservation
        /// </summary>
        IRepository<Reservation> ReservationManager { get; }

        /// <summary>
        /// Gets the repository for Menu
        /// </summary>
        IRepository<RestaurantMenuItem> MenuManager { get; }

        /// <summary>
        /// Gets the repository for ProfilePhoto
        /// </summary>
        IRepository<ProfilePhoto> ProfilePhotoManager { get; }

        /// <summary>
        /// Gets the repository for Article
        /// </summary>
        IRepository<Article> ArticleManager { get; }

        /// <summary>
        /// Gets the repository for RestaurantReservationMenuItems
        /// </summary>
        IRepository<RestaurantReservationMenuItem> RestaurantReservationMenuItemsManager { get; }

        /// <summary>
        /// Gets the repository for Event
        /// </summary>
        IRepository<Event> EventManager { get; }

        /// <summary>
        /// Gets the repository for Event
        /// </summary>
        IRepository<EventSubscription> EventSubscriptionManager { get; }
    }
}
