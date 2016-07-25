using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TouristGuide.BLL.Interfaces;
using Moq;
using TouristGuide.INF.Models;
using TouristGuide.BLL.Services;
using TouristGuide.BLL.DBContext;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.DAL.Interfaces;
using System.Threading.Tasks;
using TouristGuide.INF.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TouristGuide.INF.EntityFramework;
using TouristGuide.DAL.Repositories;


namespace TouristGuide.Tests
{
    [TestClass]
    public class ModeratorBlTesting
    {
        //"6f69a3cd-fc5c-4f79-806b-2e904ae83385"

        string userId = "6f69a3cd-fc5c-4f79-806b-2e904ae83385";
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
            public ModeratorBlTesting()
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
              commentRepository,  companyRepository,  locationRepository,  profileRepository,  profileTypeRepository,
             rateRepository,  newsRepository,  reservationRepository,  menuRepository,  profilePhotosRepository,
            articleRepository,restaurantReservationMenuItemsRepository,eventRepository,  eventSubscriptionRepository);

            this.unitOfWork = new UnitOfWork(new GuestBL(db), new UserBL(db), new ModeratorBL(db), new AdministratorBL(db), new SuperAdminBL(db));
        }

        [TestMethod]
            public void GetReservationsByProfileIdTest()
        {
            IUserBL userBl = this.unitOfWork.UserBL;

            var result = this.unitOfWork.ModeratorBL.GetReservationsByProfileId(1061);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetMealsByReservationIdTest()
        {
            IUserBL userBl = this.unitOfWork.UserBL;

            var result = this.unitOfWork.ModeratorBL.GetMealsByReservationId(1142);

            Assert.IsNotNull(result);
        }


        [TestMethod]
        public void AddMenuItemTest()
        {
            IModeratorBL moderetorBL = this.unitOfWork.ModeratorBL;
            RestaurantMenuItem menuItem = new RestaurantMenuItem();
            menuItem.Calories = 100;
            menuItem.Description = "dsfadsf";
            menuItem.DishType = "Drink";
            menuItem.DoneTime = "135";
            menuItem.ItemPhoto = "https://www.cicis.com/media/1138/pizza_trad_pepperoni.png";
            menuItem.Profile = this.unitOfWork.GuestBL.GetProfileById(1061);
            var MenuRepositoryMock = new Mock<MenuRepository>(dataBase);
            MenuRepositoryMock.Setup(a => a.Create(menuItem));
            MenuRepositoryMock.Setup(a => a.Update(menuItem));
            IDataBaseManager Createdb = new DataBaseManager(applicationUserManager, applicationRoleManager, addressRepository, bannedProfileRepository, bannedUserRepository,
                commentRepository, companyRepository, locationRepository, profileRepository, profileTypeRepository,
               rateRepository, newsRepository, reservationRepository, MenuRepositoryMock.Object, profilePhotosRepository,
              articleRepository, restaurantReservationMenuItemsRepository, eventRepository, eventSubscriptionRepository);
            ProfileRate profRate = new ProfileRate();
            profRate.Mark = 10;
            profRate.ProfileId = 2;
            profRate.UserId = userId;
            var result = new ModeratorBL(Createdb).AddMenuItem(menuItem);
            Assert.IsTrue(result.Successfully);
        }


        [TestMethod]
        public void DeleteMenuTest()
        {
            IModeratorBL moderetorBL = this.unitOfWork.ModeratorBL;
            RestaurantMenuItem menuItem = new RestaurantMenuItem();
            menuItem.Calories = 100;
            menuItem.Description = "dsfadsf";
            menuItem.DishType = "Drink";
            menuItem.DoneTime = "135";
            menuItem.ItemPhoto = "https://www.cicis.com/media/1138/pizza_trad_pepperoni.png";
            menuItem.Profile = this.unitOfWork.GuestBL.GetProfileById(1061);
            var res = moderetorBL.AddMenuItem(menuItem);

            var MenuRepositoryMock = new Mock<MenuRepository>(dataBase);
            MenuRepositoryMock.Setup(a => a.Create(menuItem));
            MenuRepositoryMock.Setup(a => a.Update(menuItem));
            IDataBaseManager Createdb = new DataBaseManager(applicationUserManager, applicationRoleManager, addressRepository, bannedProfileRepository, bannedUserRepository,
                commentRepository, companyRepository, locationRepository, profileRepository, profileTypeRepository,
               rateRepository, newsRepository, reservationRepository, MenuRepositoryMock.Object, profilePhotosRepository,
              articleRepository, restaurantReservationMenuItemsRepository, eventRepository, eventSubscriptionRepository);
           
            var result = new ModeratorBL(Createdb).DeleteMenu(menuItem.RestaurantMenuItemId);
            Assert.IsTrue(result.Successfully);
        }


        [TestMethod]
        public void EditMenuTest()
        {
            IModeratorBL moderetorBL = this.unitOfWork.ModeratorBL;
            RestaurantMenuItem menuItem = new RestaurantMenuItem();
            menuItem.Calories = 100;
            menuItem.Description = "dsfadsf";
            menuItem.DishType = "Drink";
            menuItem.DoneTime = "135";
            menuItem.ItemPhoto = "https://www.cicis.com/media/1138/pizza_trad_pepperoni.png";
            menuItem.Profile = this.unitOfWork.GuestBL.GetProfileById(1061);
            var res = moderetorBL.AddMenuItem(menuItem);

            var MenuRepositoryMock = new Mock<MenuRepository>(dataBase);
            MenuRepositoryMock.Setup(a => a.Create(menuItem));
            MenuRepositoryMock.Setup(a => a.Update(menuItem));
            IDataBaseManager Createdb = new DataBaseManager(applicationUserManager, applicationRoleManager, addressRepository, bannedProfileRepository, bannedUserRepository,
                commentRepository, companyRepository, locationRepository, profileRepository, profileTypeRepository,
               rateRepository, newsRepository, reservationRepository, MenuRepositoryMock.Object, profilePhotosRepository,
              articleRepository, restaurantReservationMenuItemsRepository, eventRepository, eventSubscriptionRepository);


            menuItem.Calories = 1488;
            menuItem.Description = "AAAAAAAAAA";
            menuItem.DishType = "Snack";
            menuItem.DoneTime = "1488";
            menuItem.ItemPhoto = "https://www.pizzahut.com/assets/w/tile/thor/Pepperoni_Lovers_Pizza.png";
            menuItem.Profile = this.unitOfWork.GuestBL.GetProfileById(1060);

            var result = new ModeratorBL(Createdb).EditMenu(menuItem);
            Assert.IsTrue(result.Successfully);
        }
    }
}
