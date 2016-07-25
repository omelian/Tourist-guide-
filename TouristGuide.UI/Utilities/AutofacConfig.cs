using Autofac;
using Autofac.Integration.Mvc;
using System.Collections.Generic;
using System.Web.Mvc;
using TouristGuide.BLL.DBContext;
using TouristGuide.BLL.Interfaces;
using TouristGuide.BLL.Services;
using TouristGuide.INF.EntityFramework;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.Identity;
using TouristGuide.INF.Models;
using TouristGuide.DAL.Repositories;
using Autofac.Core;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Configuration;

namespace TouristGuide.UI.Utilities
{
    /// <summary>
    /// Inversion of Control container for BLL Interfaces and Services
    /// </summary>
    public class AutofacConfig
    {
        /// <summary>
        /// Method that sets dependencies between BLL Interfaces and services
        /// </summary>
        public static void Configure()
        {
            //"Data Source=ssu-sql12\\tc; Initial Catalog=Lv-181-touristguideDB; User ID=lv-181.net; Password=lv-181.net;";//
            ApplicationContext dataBase = new ApplicationContext("Data Source=ssu-sql12\\tc; Initial Catalog=Lv-181-touristguideDB; User ID=lv-181.net; Password=lv-181.net;");//ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            var dataBaseManagerBuilder = new ContainerBuilder();
          
            ApplicationUserManager applicationUserManager = new ApplicationUserManager(new UserStore<ApplicationUser>(dataBase));
            ApplicationRoleManager applicationRoleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(dataBase));
            IRepository<Address> addressRepository = new AddressRepository(dataBase);
            IRepository<BannedProfile> bannedProfileRepository = new BannedProfileRepository(dataBase);
            IRepository<BannedUser> bannedUserRepository = new BannedUserRepository(dataBase);
            IRepository<Comment> commentRepository = new CommentRepository(dataBase);
            IRepository<Company> companyRepository = new CompanyRepository(dataBase);
            IRepository<Location> locationRepository = new LocationRepository(dataBase);
            IRepository<Profile> profileRepository = new ProfileRepository(dataBase);
            IRepository<ProfileType> profileTypeRepository = new ProfileTypeRepository(dataBase);
            IRepository<Rate> rateRepository = new RateRepository(dataBase);
            IRepository<News> newsRepository = new NewsRepository(dataBase);
            IRepository<Reservation> reservationRepository = new ReservationRepository(dataBase);
            IRepository<RestaurantMenuItem> menuRepository = new MenuRepository(dataBase);
            IRepository<ProfilePhoto> profilePhotosRepository = new ProfilePhotosRepository(dataBase);
            IRepository<Article> articleRepository = new ArticleRepository(dataBase);
            IRepository<RestaurantReservationMenuItem> restaurantReservationMenuItemsRepository = new RestaurantReservationMenuItemsRepository(dataBase);
            IRepository<Event> eventRepository = new EventRepository(dataBase);
            IRepository<EventSubscription> eventSubscriptionRepository = new EventSubscriptionRepository(dataBase);

            List<Parameter> parameters = new List<Parameter>();
            parameters.Add(new NamedParameter("userManager", applicationUserManager));
            parameters.Add(new NamedParameter("roleManager", applicationRoleManager));
            parameters.Add(new NamedParameter("addressRepository", addressRepository));
            parameters.Add(new NamedParameter("bannedProfileRepository", bannedProfileRepository));
            parameters.Add(new NamedParameter("bannedUserRepository", bannedUserRepository));
            parameters.Add(new NamedParameter("commentRepository", commentRepository));
            parameters.Add(new NamedParameter("companyRepository", companyRepository));
            parameters.Add(new NamedParameter("locationRepository", locationRepository));
            parameters.Add(new NamedParameter("profileRepository", profileRepository));
            parameters.Add(new NamedParameter("profileTypeRepository", profileTypeRepository));

            parameters.Add(new NamedParameter("rateRepository", rateRepository));
            parameters.Add(new NamedParameter("newsRepository", newsRepository));
            parameters.Add(new NamedParameter("reservationRepository", reservationRepository));
            parameters.Add(new NamedParameter("menuRepository", menuRepository));
            parameters.Add(new NamedParameter("profilePhotosRepository", profilePhotosRepository));
            parameters.Add(new NamedParameter("articleRepository", articleRepository));
            parameters.Add(new NamedParameter("restaurantReservationMenuItemsRepository", restaurantReservationMenuItemsRepository));
            parameters.Add(new NamedParameter("eventRepository", eventRepository));
            parameters.Add(new NamedParameter("eventSubscriptionRepository", eventSubscriptionRepository));

            dataBaseManagerBuilder.RegisterType<DataBaseManager>().As<IDataBaseManager>().WithParameters(parameters);
            var dataBaseManagerContainer = dataBaseManagerBuilder.Build();
             IDataBaseManager db = dataBaseManagerContainer.Resolve<IDataBaseManager>();

             var BLLBuilder = new ContainerBuilder();
             BLLBuilder.RegisterType<GuestBL>().As<IGuestBL>().WithParameter("db", db);
             BLLBuilder.RegisterType<UserBL>().As<IUserBL>().WithParameter("db", db);
             BLLBuilder.RegisterType<ModeratorBL>().As<IModeratorBL>().WithParameter("db", db);
             BLLBuilder.RegisterType<AdministratorBL>().As<IAdminBL>().WithParameter("db", db);
             BLLBuilder.RegisterType<SuperAdminBL>().As<ISuperAdminBL>().WithParameter("db", db);
             var BLLContainer = BLLBuilder.Build();

            var unitOfWorkBuilder = new ContainerBuilder();
            unitOfWorkBuilder.RegisterControllers(typeof(MvcApplication).Assembly);
            parameters = new List<Parameter>();
            parameters.Add(new NamedParameter("guestBL", BLLContainer.Resolve < IGuestBL>()));
            parameters.Add(new NamedParameter("userBL", BLLContainer.Resolve<IUserBL>()));
            parameters.Add(new NamedParameter("moderatorBL", BLLContainer.Resolve<IModeratorBL>()));
            parameters.Add(new NamedParameter("adminBL", BLLContainer.Resolve<IAdminBL>()));
            parameters.Add(new NamedParameter("superAdminBL", BLLContainer.Resolve<ISuperAdminBL>()));
            unitOfWorkBuilder.RegisterType<UnitOfWork>().As<IUnitOfWork>().WithParameters(parameters);
            var container = unitOfWorkBuilder.Build();
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
        }
    }
}