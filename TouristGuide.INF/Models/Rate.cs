namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Rate model
    /// </summary>
    public class Rate
    {
        /// <summary>
        /// Gets or sets rate id
        /// </summary>
        public int RateId { get; set; }

        /// <summary>
        /// Gets or sets rate mark
        /// </summary>
        public int Mark { get; set; }

        /// <summary>
        /// Gets or sets reference to profile
        /// </summary>
        public virtual Profile Profile { get; set; }

        /// <summary>
        /// Gets or sets reference to user
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}