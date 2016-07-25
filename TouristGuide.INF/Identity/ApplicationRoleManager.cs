using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using TouristGuide.INF.Models;

namespace TouristGuide.INF.Identity
{
    /// <summary>
    /// Application Role Manager for work with roles
    /// </summary>
    public class ApplicationRoleManager : RoleManager<ApplicationRole>
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationRoleManager class
        /// </summary>
        /// <param name="store"> Context of database </param>
        public ApplicationRoleManager(RoleStore<ApplicationRole> store) : base(store)
        {
        }
    }
}
