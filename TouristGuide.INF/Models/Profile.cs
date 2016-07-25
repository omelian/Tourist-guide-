using System.Collections.Generic;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Profile model
    /// </summary>
    public class Profile
    {
        /// <summary>
        /// Initializes a new instance of the Profile class
        /// </summary>
        public Profile()
        {
            this.Rates = new HashSet<Rate>();
            this.Comments = new HashSet<Comment>();
            this.Moders = new HashSet<ApplicationUser>();          
            this.News = new HashSet<News>();
            this.Photos = new HashSet<ProfilePhoto>();
            this.Reservations = new HashSet<Reservation>();
        }

        /// <summary>
        /// Gets or sets profile id
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets profile name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets main photo id
        /// </summary>
        public int MainPhoto { get; set; }

        /// <summary>
        ///  Gets or sets is profile deleted 
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Gets or sets collection photo for profile
        /// </summary>
        public virtual ICollection<ProfilePhoto> Photos { get; set; }

        /// <summary>
        /// Gets or sets reference company
        /// </summary>
        public virtual Company Company { get; set; }

        /// <summary>
        /// Gets or sets reference for profile address
        /// </summary>
        public virtual Address Address { get; set; }

        /// <summary>
        /// Gets or sets reference for profile type
        /// </summary>
        public virtual ProfileType Type { get; set; }

        /// <summary>
        /// Gets or sets reference for banned profile
        /// </summary>
        public virtual BannedProfile IsBanned { get; set; }

        /// <summary>
        /// Gets or sets collection of news for profile
        /// </summary>
        public virtual ICollection<News> News { get; set; }

        /// <summary>
        /// Gets or sets collection of rates for profile
        /// </summary>
        public virtual ICollection<Rate> Rates { get; set; }

        /// <summary>
        /// Gets or sets collection of comments for profile
        /// </summary>
        public virtual ICollection<Comment> Comments { get; set; }

        /// <summary>
        /// Gets or sets collection of moderators for profile
        /// </summary>
        public virtual ICollection<ApplicationUser> Moders { get; set; }
        
        /// <summary>
        /// Gets or sets collection of Reservation for profile
        /// </summary>
        public virtual ICollection<Reservation> Reservations { get; set; }               

        /// <summary>
        /// Gets or sets profile property if is showed for all users or not
        /// </summary>
        public virtual bool IsShowed { get; set; }
    }
}