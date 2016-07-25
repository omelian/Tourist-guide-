namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Attraction article
    /// </summary>
    public class Article
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

        /// <summary>
        /// Gets or sets reference who have a comment 
        /// </summary>
        public virtual Profile Profile { get; set; }
    }
}
