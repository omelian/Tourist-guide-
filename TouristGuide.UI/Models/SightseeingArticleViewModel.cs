using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    public class SightseeingArticleViewModel
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
    }
}