using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TouristGuide.BLL.DBContext;
using TouristGuide.BLL.Services;
using TouristGuide.INF.Models;
using Moq;
using TouristGuide.DAL.Interfaces;
using System.Collections.Generic;
using TouristGuide.UI.Controllers;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TouristGuide.DAL.Repositories;

namespace TouristGuide.Tests
{
    [TestClass]
    public class MenuTest
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

        public MenuTest()
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
        [TestMethod]
        public void AddMenuTest()
        {
            IModeratorBL moderBl = this.unitOfWork.ModeratorBL;
            RestaurantMenuItem menu = new RestaurantMenuItem();

            menu.Profile = new Profile();

            var result = moderBl.AddMenuItem(menu);

            Assert.IsFalse(result.Successfully);
        }

        [TestMethod]
        public void EditMenuTest()
        {
            IModeratorBL moderBl = this.unitOfWork.ModeratorBL;
            RestaurantMenuItem menu = new RestaurantMenuItem();

            menu.Profile = new Profile();

            var result = moderBl.EditMenu(menu);

            Assert.IsTrue(result.Successfully);
        }

        [TestMethod]
        public void GetMenuTest()
        {
            MenuController menu = new MenuController(this.unitOfWork);

            var result = menu.TestGetMenu();

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetInformationTest()
        {
            IGuestBL guestBl = this.unitOfWork.GuestBL;

            var result = guestBl.GetArticleByProfileId(29);

            Assert.IsNotNull(result);
        }
    }
}
