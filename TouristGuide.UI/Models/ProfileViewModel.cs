using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    public class ProfileViewModel
    {
        /// <summary>
        /// Gets or sets profile id
        /// </summary>
        public int? ProfileId { get; set; }

        /// <summary>
        /// Gets or sets profile name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set profile banned state
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// Get or set profile last banned reason
        /// </summary>
        public string BannedReason { get; set; }

        /// <summary> 
        /// Gets or sets profile property if is showed for all users or not 
        /// </summary> 
        public bool IsShowed { get; set; }
    }
}