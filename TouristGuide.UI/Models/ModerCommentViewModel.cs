using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    public class ModerCommentViewModel
    {
        /// <summary>
        /// Gets or sets id comment
        /// </summary>
        public int CommentId { get; set; }

        /// <summary>
        /// Gets or sets text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Gets  comment date
        /// </summary>
        public string CommentDateTime { get; set; }

        /// <summary>
        /// Gets or sets reference about whom is comment 
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets id about whom is comment 
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        ///  Gets or sets user id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        /// Gets or sets  Type profile
        /// </summary>
        public string Type { get; set; }


        /// <summary>
        ///  Gets or sets name of user
        /// </summary>
        public string User { get; set; }

        /// <summary>
        ///  Gets or sets url of user photo
        /// </summary>
        public string UserPhotoUrl { get; set; }
    }
}