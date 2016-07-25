using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    public class UserViewModel
    {
        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets user first name
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Get or set user last name
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Get or set user banned state
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// Get or set user deleted state
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Get or set user last banned reason
        /// </summary>
        public string BannedReason { get; set; }

        /// <summary>
        /// Get or set user last deleted reason
        /// </summary>
        public string DeletedReason { get; set; }
    }
}