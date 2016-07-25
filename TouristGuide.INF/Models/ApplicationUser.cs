using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Application User Model
    /// </summary>
    public class ApplicationUser : IdentityUser
    {
        /// <summary>
        /// Initializes a new instance of the ApplicationUser class.        
        /// </summary>
        public ApplicationUser()
        {
            this.GivenRates = new HashSet<Rate>();
            this.GivenComments = new HashSet<Comment>();            
            this.FavoriteProfiles = new HashSet<Profile>();
            this.MyReservations = new HashSet<Reservation>();
            this.MyEventSubscriptions = new HashSet<EventSubscription>();
        }

        /// <summary>
        /// Gets or sets user FirstName
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets user LastName
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets user Gender
        /// </summary>
        public string Gender { get; set; }

        /// <summary>
        /// Gets or sets user DateBirth
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public virtual DateTime DateBirth { get; set; }

        /// <summary>
        /// Gets or sets url of user photo
        /// </summary>
        public string Photo { get; set; }

        /// <summary>
        /// Gets or sets reference to Company
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Gets or sets Collection to Rates
        /// </summary>
        public virtual ICollection<Rate> GivenRates { get; set; }

        /// <summary>
        /// Gets or sets Collection to Comments
        /// </summary>
        public virtual ICollection<Comment> GivenComments { get; set; }

        /// <summary>
        /// Gets or sets Collection to FavoriteProfiles
        /// </summary>
        public virtual ICollection<Profile> FavoriteProfiles { get; set; }

        /// <summary>
        /// Gets or sets Collection to FavoriteProfiles
        /// </summary>
        public virtual ICollection<Reservation> MyReservations { get; set; }

        /// <summary>
        /// Gets or sets Collection to FavoriteProfiles
        /// </summary>
        public virtual ICollection<EventSubscription> MyEventSubscriptions { get; set; }

        /// <summary>
        /// Gets or sets reference to BannedUser
        /// </summary>
        public virtual BannedUser IsBanned { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets a reason why user has been deleted
        /// </summary>
        public string DeletedReason { get; set; }

        /// <summary>
        /// Gets or sets reference to ManageProfile
        /// </summary>
        public virtual Profile ManageProfile { get; set; }

        /// <summary>
        /// Generate user identity
        /// </summary>
        /// <param name="manager"> user manager</param>
        /// <returns>claims</returns>
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {            
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie); 
            return userIdentity;
        }
    }
}
