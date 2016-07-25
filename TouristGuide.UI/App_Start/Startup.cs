using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Google;
using Microsoft.Owin.Security.Facebook;
using Owin;
using System.Configuration;
using TouristGuide.BLL.DBContext;
using TouristGuide.INF.EntityFramework;
using TouristGuide.DAL;
using TouristGuide.UI.Models;
using TouristGuide.INF.Identity;

namespace TouristGuide.UI.App_Start
{
    /// <summary>
    /// OWIN class which specifies components for the application pipeline.
    /// </summary>
    public class Startup
    {
        static ApplicationContext GetNewContext()
        {
            //ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString//
            //"Data Source=ssu-sql12\\tc; Initial Catalog=Lv-181-touristguideDB; User ID=lv-181.net; Password=lv-181.net;"
            return new ApplicationContext("Data Source=ssu-sql12\\tc; Initial Catalog=Lv-181-touristguideDB; User ID=lv-181.net; Password=lv-181.net;");
        }
        /// <summary>
        /// The Katana infrastructure will build the pipeline of middleware components based on the order in which they were added to the IAppBuilder object
        /// </summary>
        /// <param name="app">It enables the
        /// possibility to add middleware to an OWIN pipeline</param>
        public void Configuration(IAppBuilder app)
        {
            app.CreatePerOwinContext<ApplicationContext>(GetNewContext);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "AppCookie",
                LoginPath = new PathString("/Account/Login")
            });

            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "56892801647-v8eemkrukgl0qph47r4i16vj0hsgcv55.apps.googleusercontent.com",
                ClientSecret = "-tzinnL49fWUm2KkPimiW_9E"
            });

            app.UseFacebookAuthentication(
               appId: "161652674248549",
               appSecret: "7973f78a8ce382caf35762aa3a1a1427");
        }
    }
}