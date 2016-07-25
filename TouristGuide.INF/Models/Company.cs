using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Company model
    /// </summary>
    public class Company
    {
        /// <summary>
        /// Initializes a new instance of the Company class
        /// </summary>
        public Company()
        {
            this.Profiles = new HashSet<Profile>();
        }

        /// <summary>
        /// Gets or sets company id
        /// </summary>
        [Key, ForeignKey("User")]   
        public string CompanyId { get; set; }

        /// <summary>
        /// Gets or sets name of company
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets reference who have this company
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets Collection of profiles 
        /// </summary>
        public ICollection<Profile> Profiles { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether approving
        /// </summary>
        public string RequestState { get; set; }

        /// <summary>
        /// Gets or sets description for unapproved
        /// </summary>
        public string Description { get; set; }
    }
}