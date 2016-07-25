using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    /// <summary>
    /// Information Attraction View Model
    /// </summary>
    public class InformationViewModel
    {
        /// <summary>
        /// Gets or sets id article
        /// </summary>
        public int ArticleId { get; set; }

        /// <summary>
        /// Gets or sets title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets or sets picture
        /// </summary>
        public string PictureUrl { get; set; }
        
        /// <summary>
        /// Gets or sets email 
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets PhoneNumber text
        /// </summary>
        public string PhoneNumber { get; set; }

        /// <summary>
        /// Gets or sets Facebook reference
        /// </summary>
        public string FacebookReference { get; set; }

        /// <summary>
        /// Gets or sets website reference
        /// </summary>
        public string WebSiteReference { get; set; }
    }
}