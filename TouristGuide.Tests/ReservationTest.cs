using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TouristGuide.BLL.DBContext;
using TouristGuide.UI.Controllers;
using TouristGuide.BLL.Services;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Identity;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using TouristGuide.DAL.Repositories;

namespace TouristGuide.Tests
{
    /// <summary>
    /// Class for reservation testing
    /// </summary>
    [TestClass]
    public class ReservationTest
    {

        IUnitOfWork unitOfWork;// for get operations
        ApplicationContext dataBase;
        ApplicationUserManager applicationUserManager;
        ApplicationRoleManager applicationRoleManager;
        IRepository<Address> addressRepository;
        IRepository<BannedProfile> bannedProfileRepository;
        IRepository<BannedUser> bannedUserRepository;
        IRepository<Comment> commentRepository;
        IRepository<Company> companyRepository;
        IRepository<Location> locationRepository;
        IRepository<Profile> profileRepository;
        IRepository<ProfileType> profileTypeRepository;
        IRepository<Rate> rateRepository;
        IRepository<News> newsRepository;
        IRepository<Reservation> reservationRepository;
        IRepository<RestaurantMenuItem> menuRepository;
        IRepository<ProfilePhoto> profilePhotosRepository;
        IRepository<Article> articleRepository;
        IRepository<RestaurantReservationMenuItem> restaurantReservationMenuItemsRepository;
        IRepository<Event> eventRepository;
        IRepository<EventSubscription> eventSubscriptionRepository;

        /// <summary>
        /// Initialize ReservationTest
        /// </summary>
       public ReservationTest()
        {
            dataBase = new ApplicationContext("Data Source=ssu-sql12\\tc; Initial Catalog=Lv-181-touristguideDB; User ID=lv-181.net; Password=lv-181.net;");
            applicationUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(dataBase));
            applicationRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(dataBase));
            addressRepository = new AddressRepository(dataBase);
            bannedProfileRepository = new BannedProfileRepository(dataBase);
            bannedUserRepository = new BannedUserRepository(dataBase);
            commentRepository = new CommentRepository(dataBase);
            companyRepository = new CompanyRepository(dataBase);
            locationRepository = new LocationRepository(dataBase);
            profileRepository = new ProfileRepository(dataBase);
            profileTypeRepository = new ProfileTypeRepository(dataBase);
            rateRepository = new RateRepository(dataBase);
            newsRepository = new NewsRepository(dataBase);
            reservationRepository = new ReservationRepository(dataBase);
            menuRepository = new MenuRepository(dataBase);
            profilePhotosRepository = new ProfilePhotosRepository(dataBase);
            articleRepository = new ArticleRepository(dataBase);
            restaurantReservationMenuItemsRepository = new RestaurantReservationMenuItemsRepository(dataBase);
            eventRepository = new EventRepository(dataBase);
            eventSubscriptionRepository = new EventSubscriptionRepository(dataBase);

            IDataBaseManager db = new DataBaseManager(applicationUserManager, applicationRoleManager, addressRepository, bannedProfileRepository, bannedUserRepository,
              commentRepository, companyRepository, locationRepository, profileRepository, profileTypeRepository,
             rateRepository, newsRepository, reservationRepository, menuRepository, profilePhotosRepository,
            articleRepository, restaurantReservationMenuItemsRepository, eventRepository, eventSubscriptionRepository);

            this.unitOfWork = new UnitOfWork(new GuestBL(db), new UserBL(db), new ModeratorBL(db), new AdministratorBL(db), new SuperAdminBL(db));
        }

        /// <summary>
        /// Testing method GetAllReservationByUserName with right name
        /// </summary>
        [TestMethod]
        public void GetAllReservationByUserName()
        {
            ReservationController reservation = new ReservationController(this.unitOfWork);

            var result = reservation.GetAllReservationsByUserName("Horse");

            Assert.IsNotNull(result);
        }


        /// <summary>
        /// Testing method GetAllReservationByUserName with wrong name
        /// </summary>
        [TestMethod]
        public void GetAllReservationByUserNameIsWrongName()
        {
            ReservationController reservation = new ReservationController(this.unitOfWork);

            var result = reservation.GetAllReservationsByUserName("lolyyyy");

            Assert.IsNull(result);
        }

        /// <summary>
        /// Testing method GetAllReservationByUserName with right name and id
        /// </summary>
        [TestMethod]
        public void GetReservationByUserName()
        {
            ReservationController reservation = new ReservationController(this.unitOfWork);

            var result = reservation.GetReservationByUserName("Horse",2);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Testing method GetAllReservationByUserName with wrong name
        /// </summary>
        [TestMethod]
        public void GetReservationByUserNameIsWrongName()
        {
            ReservationController reservation = new ReservationController(this.unitOfWork);

            var result = reservation.GetReservationByUserName("lolyyyy", 2);

            Assert.IsNull(result);
        }

        /// <summary>
        /// Testing method GetAllReservationByUserName with wrong id
        /// </summary>
        [TestMethod]
        public void GetReservationByUserNameIsWrongProfileId()
        {
            ReservationController reservation = new ReservationController(this.unitOfWork);

            var result = reservation.GetReservationByUserName("Horse", 3333);

            Assert.IsNull(result);
        }

        
    }
}
