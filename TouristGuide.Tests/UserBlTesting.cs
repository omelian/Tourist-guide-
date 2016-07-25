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
using System;


namespace TouristGuide.Tests
{
    [TestClass]
    public class UserBlTesting
    {
        string userId = "82d1c306-6cb6-47c4-99f8-4ce8b12eebdf";
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
        public UserBlTesting()
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
       public void GetUserInfoTest()
        {
            string result = this.unitOfWork.UserBL.GetUserInfo(userId).LastName;

            Assert.IsTrue(result.Equals("DefaultModerator"));
        }

        [TestMethod]
        public void GetUserFavoritesTest()
        {
            IUserBL userBl = this.unitOfWork.UserBL;

            var result = userBl.GetUserFavorites(userId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUserCommentsTest()
        {
            IUserBL userBl = this.unitOfWork.UserBL;

            var result = userBl.GetUserComments(userId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetUserRatesTest()
        {
            IUserBL userBl = this.unitOfWork.UserBL;

            var result = userBl.GetUserRates(userId);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void CreateTest()
        {
            ApplicationUser user = new ApplicationUser();
            user.UserName = "CreateTestQQQ@test.com";
            user.Email = "CreateTestQQQ@test.com";
            user.LastName = "wqQeqwqwfqw";

            Func<object> function = delegate()
            { return null; };
            var ApplicationUserStoreMock = new Mock<UserStore<ApplicationUser>>();
            ApplicationUserStoreMock.Setup(a => a.CreateAsync(user)).Returns(new Task<object>(function));
            ApplicationUserStoreMock.Setup(a => a.AddToRoleAsync(user, "User")).Returns(new Task<object>(function));
            ApplicationUserManager applicationUserManager = new ApplicationUserManager(ApplicationUserStoreMock.Object);

            IDataBaseManager createDB = new DataBaseManager(applicationUserManager, null, null, null, null,
             null, null, null, null, null,
            null, null, null, null, null,
           null, null, null, null);

         IUserBL userBl = new UserBL(createDB);

            UserRegisterModel model = new UserRegisterModel();
            model.Role = INF.Enums.UserRoleEnum.User;
            model.UserName = "CreateTestQQQ@test.com";
            model.Email = "CreateTestQQQ@test.com";
            model.LastName = "Qeqwqwfqw";
            model.Password = "Qfasfasfasfas123*";
            model.ConfirmPassword = "Qfasfasfasfas123*";
           var result = userBl.Create(model);


           Assert.IsTrue(result.Successfully);
        }


        [TestMethod]
        public void AuthenticateTest()
        {
            IUserBL userBl = this.unitOfWork.UserBL;
            UserLoginModel loginModel = new UserLoginModel();
            loginModel.Email = "geruch@gamil.com";
            loginModel.Password = "Qwer27512*";

           var result = userBl.Authenticate(loginModel);

           Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SetRateTest()
        {
            IUserBL userBl = this.unitOfWork.UserBL;

            Rate rate = new Rate();
            rate.Profile = new Profile() { ProfileId = 1 };
            rate.Mark = 10;
            rate.User = new ApplicationUser() { Id = userId };
            var RateRepositoryMock = new Mock<RateRepository>(dataBase);
            RateRepositoryMock.Setup(a => a.Create(rate));
            RateRepositoryMock.Setup(a => a.Update(rate));

            IDataBaseManager Createdb = new DataBaseManager(applicationUserManager, applicationRoleManager, addressRepository, bannedProfileRepository, bannedUserRepository,
                commentRepository, companyRepository, locationRepository, profileRepository, profileTypeRepository,
               RateRepositoryMock.Object, newsRepository, reservationRepository, menuRepository, profilePhotosRepository,
              articleRepository, restaurantReservationMenuItemsRepository, eventRepository, eventSubscriptionRepository);


            ProfileRate profRate = new ProfileRate();
            profRate.Mark = 10;
            profRate.ProfileId = 2;
            profRate.UserId = userId;
            var result = new UserBL(Createdb).SetRate(profRate);

            Assert.IsTrue(result.Successfully);
        }

        [TestMethod]
        public void AddCommentTest()
        {
            IUserBL userBl = this.unitOfWork.UserBL;
            Comment comment = new Comment();
            comment.Text = "ХХХХХХХХХХХХХХХХХХХХХХХХХХХХХХХ";
            comment.Profile = new Profile() { ProfileId = 2};
            comment.User = new ApplicationUser() { Id = userId };
            var CommentRepositoryMock = new Mock<CommentRepository>(dataBase);
            CommentRepositoryMock.Setup(a => a.Create(comment));
            IDataBaseManager Createdb = new DataBaseManager(applicationUserManager, applicationRoleManager, addressRepository, bannedProfileRepository, bannedUserRepository,
               CommentRepositoryMock.Object, companyRepository, locationRepository, profileRepository, profileTypeRepository,
              rateRepository, newsRepository, reservationRepository, menuRepository, profilePhotosRepository,
             articleRepository, restaurantReservationMenuItemsRepository, eventRepository, eventSubscriptionRepository);


            var result = new UserBL(Createdb).AddComment(comment);

            Assert.IsTrue(result.Successfully);
        }
      
    }
}
