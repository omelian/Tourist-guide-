using System.Collections.Generic;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Profile type model
    /// </summary>
    public class ProfileType
    {
        /// <summary>
        /// Initializes a new instance of the ProfileType class
        /// </summary>
        public ProfileType()
        {
            this.Profiles = new HashSet<Profile>();
        }

        /// <summary>
        /// Gets or sets profile type id
        /// </summary>
        public int ProfileTypeId { get; set; }

        /// <summary>
        /// Gets or sets profile type name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets collection of profiles
        /// </summary>
        public virtual ICollection<Profile> Profiles { get; set; }
    }
}