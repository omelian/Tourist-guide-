namespace TouristGuide.UI.Models
{
    /// <summary>
    /// News List View Model
    /// </summary>
    public class ProfileNewsViewModel
    {
        /// <summary>
        ///  Gets or sets news id
        /// </summary>
        public int NewsId { get; set; }

        /// <summary>
        ///  Gets or sets profile news title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        ///  Gets or sets profile news content
        /// </summary>
        public string TextContent { get; set; }

        /// <summary>
        ///  Gets or sets profile news image
        /// </summary>
        public string NewsImageUrl { get; set; }

        /// <summary>
        ///  Gets or sets date of news content
        /// </summary>
        public string DateTime { get; set; }
    }
}