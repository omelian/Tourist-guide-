using System.Collections.Generic;

namespace TouristGuide.UI.Models
{
    /// <summary>
    /// Rest page View Model
    /// </summary>
    public class ProfilePageViewModel
    {
        /// <summary>
        ///  Gets or sets profile name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets profile city
        /// </summary>
        public string City { get; set; }
        
        /// <summary>
        /// Gets or sets profile street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets profile number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets profile photo
        /// </summary>
        public int MainPhoto { get; set; }

        /// <summary>
        /// Gets or sets moderators
        /// </summary>
        public ICollection<string> Moderators { get; set; }

        /// <summary> 
        /// Gets or sets profile property if is showed for all users or not 
        /// </summary> 
        public bool IsShowed { get; set; }
    }

}