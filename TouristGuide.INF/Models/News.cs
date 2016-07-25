using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// News model
    /// </summary>
    public class News
    {
        /// <summary>
        /// Gets or sets news id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        /// Gets or sets news title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets content of news
        /// </summary>
        public string TextContent { get; set; }

        /// <summary>
        /// Gets or sets image url of news
        /// </summary>
        public string NewsImageUrl { get; set; }

        /// <summary>
        /// Gets or sets date of news
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public virtual DateTime NewsDate { get; set; }

        /// <summary>
        /// Gets or sets reference for profile
        /// </summary>
        public virtual Profile Profile { get; set; }
    }
}
