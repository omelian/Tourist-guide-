using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TouristGuide.INF.Models;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using TouristGuide.INF.EntityFramework;

namespace TouristGuide.INF.Identity
{
    /// <summary>
    /// ApplicationUserManager class for work with ApplicationUser
    /// </summary>
    public class ApplicationUserManager : UserManager<ApplicationUser>
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationUserManager class
        /// </summary>
        /// <param name="store"> Context of database </param>
        public ApplicationUserManager(IUserStore<ApplicationUser> store) : base(store)
        {
        }        
        public static ApplicationUserManager Create(IdentityFactoryOptions<ApplicationUserManager> options, IOwinContext context) 
        {           
            var manager = new ApplicationUserManager(new UserStore<ApplicationUser>(context.Get<ApplicationContext>()));

            return manager;
        }
    }
}
