namespace TouristGuide.UI.Models
{
    /// <summary>
    /// Authentication Model View
    /// </summary>
    public class AuthenticationModel
    {
        /// <summary>
        /// Gets or sets Id of user
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets User role in system
        /// </summary>
        public string Role { get; set; }

        /// <summary>
        /// Gets or sets User name in system
        /// </summary>
        public string Name { get; set; }
    }
}