using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Comment model
    /// </summary>
    public class Comment
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
        /// Gets or sets comment date
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public virtual DateTime CommentDateTime { get; set; }

        /// <summary>
        /// Gets or sets reference who have a comment 
        /// </summary>
        public virtual Profile Profile { get; set; }

        /// <summary>
        /// Gets or sets reference who gives a comment
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}