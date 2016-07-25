using System;
using TouristGuide.BLL.Interfaces;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Identity;
using TouristGuide.INF.Models;

namespace TouristGuide.BLL.DBContext
{
    /// <summary>
    /// Database controller that has all repositories to work with data base
    /// </summary>
    public class DataBaseManager : IDataBaseManager
    {
        /// <summary>
        /// reference to ApplicationUserManager
        /// </summary>
        private ApplicationUserManager userManager;

        /// <summary>
        /// reference to ApplicationRoleManager
        /// </summary>
        private ApplicationRoleManager roleManager;

         /// <summary>
        /// reference to AddressRepository
        /// </summary>
        private IRepository<Address> addressManager;

        /// <summary>
        /// reference to BannedProfileRepository
        /// </summary>
        private IRepository<BannedProfile> bannedProfileManager;

        /// <summary>
        /// reference to BannedUserRepository
        /// </summary>
        private IRepository<BannedUser> bannedUserManager;

        /// <summary>
        /// reference to CommentRepository
        /// </summary>
        private IRepository<Comment> commentManager;

        /// <summary>
        /// reference to CompanyRepository
        /// </summary>
        private IRepository<Company> companyManager;

        /// <summary>
        /// reference to LocationRepository
        /// </summary>
        private IRepository<Location> locationManager;

        /// <summary>
        /// reference to ProfileRepository
        /// </summary>
        private IRepository<Profile> profileManager;

        /// <summary>
        /// reference to ProfileTypeRepository
        /// </summary>
        private IRepository<ProfileType> profileTypeManager;

        /// <summary>
        /// reference to RateRepository
        /// </summary>
        private IRepository<Rate> rateManager;

        /// <summary>
        /// reference to newsRepository
        /// </summary>
        private IRepository<News> newsManager;

        /// <summary>
        /// reference to newsRepository
        /// </summary>
        private IRepository<Reservation> reservationManager;

        /// <summary>
        /// reference to menuRepository
        /// </summary>
        private IRepository<RestaurantMenuItem> menuManager;

        /// <summary>
        /// reference to profilePhotoManager
        /// </summary>
        private IRepository<ProfilePhoto> priflePhotoManager;

        /// <summary>
        /// reference to Article
        /// </summary>
        private IRepository<Article> articleManager;

        /// <summary>
        /// reference to Article
        /// </summary>
        private IRepository<RestaurantReservationMenuItem> restaurantReservationMenuItemsManager;

        /// <summary>
        /// reference to Event
        /// </summary>
        private IRepository<Event> eventManager;

        /// <summary>
        /// reference to Event
        /// </summary>
        private IRepository<EventSubscription> eventSubscriptionManager;

        /// <summary>
        /// Initializes a new instance of the DataBaseManager class.
        /// </summary>
        /// <param name="Repositories">Repositories for tables</param>
        public DataBaseManager(ApplicationUserManager userManager, ApplicationRoleManager roleManager, IRepository<Address> addressRepository, IRepository<BannedProfile> bannedProfileRepository, IRepository<BannedUser> bannedUserRepository,
            IRepository<Comment>  commentRepository, IRepository<Company>  companyRepository, IRepository<Location> locationRepository, IRepository<Profile>  profileRepository, IRepository<ProfileType>  profileTypeRepository,
            IRepository<Rate>  rateRepository, IRepository<News>  newsRepository, IRepository<Reservation>  reservationRepository, IRepository<RestaurantMenuItem>  menuRepository, IRepository<ProfilePhoto>  profilePhotosRepository,
            IRepository<Article> articleRepository, IRepository<RestaurantReservationMenuItem> restaurantReservationMenuItemsRepository, IRepository<Event> eventRepository, IRepository<EventSubscription> eventSubscriptionRepository)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.addressManager = addressRepository;
            this.bannedProfileManager = bannedProfileRepository;
            this.bannedUserManager = bannedUserRepository;
            this.commentManager = commentRepository;
            this.companyManager = companyRepository;
            this.locationManager = locationRepository;
            this.profileManager = profileRepository;
            this.profileTypeManager = profileTypeRepository;
            this.rateManager = rateRepository;
            this.newsManager = newsRepository; ;
            this.reservationManager = reservationRepository;
            this.menuManager = menuRepository;
            this.priflePhotoManager = profilePhotosRepository;
            this.articleManager = articleRepository;
            this.restaurantReservationMenuItemsManager = restaurantReservationMenuItemsRepository;
            this.eventManager = eventRepository;
            this.eventSubscriptionManager = eventSubscriptionRepository;
        }

        /// <summary>
        /// Gets the manager for Users
        /// </summary>
        public ApplicationUserManager UserManager
        {
            get { return this.userManager; }
        }

        /// <summary>
        /// Gets the manager for roles
        /// </summary>
        public ApplicationRoleManager RoleManager
        {
            get { return this.roleManager; }
        }

        /// <summary>
        /// Gets the repository for addresses
        /// </summary>
        public IRepository<Address> AddressManager
        {
            get { return this.addressManager; }
        }

        /// <summary>
        /// Gets the repository for BannedProfiles
        /// </summary>
        public IRepository<BannedProfile> BannedProfileManager
        {
            get { return this.bannedProfileManager; }
        }

        /// <summary>
        ///  Gets the repository for BannedUsers
        /// </summary>
        public IRepository<BannedUser> BannedUserManager
        {
            get { return this.bannedUserManager; }
        }

        /// <summary>
        /// Gets the repository for Comments
        /// </summary>
        public IRepository<Comment> CommentManager
        {
            get { return this.commentManager; }
        }

        /// <summary>
        /// Gets the repository for Companies
        /// </summary>
        public IRepository<Company> CompanyManager
        {
            get { return this.companyManager; }
        }

        /// <summary>
        /// Gets the repository for Locations
        /// </summary>
        public IRepository<Location> LocationManager
        {
            get { return this.locationManager; }
        }

        /// <summary>
        /// Gets the repository for Profiles
        /// </summary>
        public IRepository<Profile> ProfileManager
        {
            get { return this.profileManager; }
        }

        /// <summary>
        /// Gets the repository for ProfileTypes
        /// </summary>
        public IRepository<ProfileType> ProfileTypeManager
        {
            get { return this.profileTypeManager; }
        }

        /// <summary>
        /// Gets the repository for Rates
        /// </summary>
        public IRepository<Rate> RateManager
        {
            get { return this.rateManager; }
        }

        /// <summary>
        /// Gets the repository for news
        /// </summary>
        public IRepository<News> NewsManager
        {
            get { return this.newsManager; }
        }

        /// <summary>
        /// Gets the repository for reservations
        /// </summary>
        public IRepository<Reservation> ReservationManager
        {
            get { return this.reservationManager; }
        }

        /// <summary>
        /// Gets the repository for menu
        /// </summary>
        public IRepository<RestaurantMenuItem> MenuManager
        {
            get { return this.menuManager; }
        }

        /// <summary>
        /// Gets the repository for ProfilePhotos
        /// </summary>
        public IRepository<ProfilePhoto> ProfilePhotoManager
        {
            get { return this.priflePhotoManager; }
        }

        /// <summary>
        /// Gets the repository for article
        /// </summary>
        public IRepository<Article> ArticleManager
        {
            get { return this.articleManager; }
        }

        /// <summary>
        /// Gets the repository for menu restaurant reservation
        /// </summary>
        public IRepository<RestaurantReservationMenuItem> RestaurantReservationMenuItemsManager
        {
            get { return this.restaurantReservationMenuItemsManager; }
        }

        /// <summary>
        /// Gets the repository for event
        /// </summary>
        public IRepository<Event> EventManager
        {
            get { return this.eventManager; }
        }

        /// <summary>
        /// Gets the repository for event
        /// </summary>
        public IRepository<EventSubscription> EventSubscriptionManager
        {
            get { return this.eventSubscriptionManager; }
        }
    }
}
