using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TouristGuide.INF.Models;

namespace TouristGuide.INF.EntityFramework
{
    /// <summary>
    /// Context of TouristGuide data base
    /// </summary>
    public class ApplicationContext : IdentityDbContext<ApplicationUser> 
    {        
            /// <summary>
            /// Initializes a new instance of the ApplicationContext class.
            /// </summary>
            /// <param name="connetionString">Connection string to data base</param>
            public ApplicationContext(string connetionString) :
                base(connetionString)
            {
            }

            /// <summary>
            /// Gets or sets table of addresses
            /// </summary>
            public DbSet<Address> Addresses { get; set; }

            /// <summary>
            /// Gets or sets table of BannedProfiles
            /// </summary>
            public DbSet<BannedProfile> BannedProfiles { get; set; }

            /// <summary>
            /// Gets or sets table of BannedUsers
            /// </summary>
            public DbSet<BannedUser> BannedUsers { get; set; }

            /// <summary>
            /// Gets or sets table of Comments
            /// </summary>
            public DbSet<Comment> Comments { get; set; }

            /// <summary>
            /// Gets or sets table of Companies
            /// </summary>
            public DbSet<Company> Companies { get; set; }

            /// <summary>
            /// Gets or sets table of Locations
            /// </summary>
            public DbSet<Location> Locations { get; set; }

            /// <summary>
            /// Gets or sets table of Profiles
            /// </summary>
            public DbSet<Profile> Profiles { get; set; }

            /// <summary>
            /// Gets or sets table of ProfileTypes
            /// </summary>
            public DbSet<ProfileType> ProfileTypes { get; set; }

            /// <summary>
            /// Gets or sets table of Rates
            /// </summary>
            public DbSet<Rate> Rates { get; set; }

            /// <summary>
            /// Gets or sets table of News
            /// </summary>
            public DbSet<News> ProfileNews { get; set; }

            /// <summary>
            /// Gets or sets table of Reservation
            /// </summary>
            public DbSet<Reservation> Reservations { get; set; }
            
            /// <summary>
            /// Gets or sets table of News
            /// </summary>
            public DbSet<RestaurantMenuItem> Menus { get; set; }

            /// <summary>
            /// Gets or sets table of ProfilePhotos
            /// </summary>
            public DbSet<ProfilePhoto> ProfilePhotos { get; set; }

            /// <summary>
            /// Gets or sets table of Articles
            /// </summary>
            public DbSet<Article> Articles { get; set; }

            /// <summary>
            /// Gets or sets table of RestaurantReservationMenuItems
            /// </summary>
            public DbSet<RestaurantReservationMenuItem> RestaurantReservationMenuItems { get; set; }

            /// <summary>
            /// Gets or sets table of Events
            /// </summary>
            public DbSet<Event> Events { get; set; }

            /// <summary>
            /// Gets or sets table of Events
            /// </summary>
            public DbSet<EventSubscription> EventSubscriptions { get; set; }
    }
}
