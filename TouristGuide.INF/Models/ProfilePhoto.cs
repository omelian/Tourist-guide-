namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Profile photo model
    /// </summary>
    public class ProfilePhoto
    {
        /// <summary>
        /// Gets or sets profile photo id
        /// </summary>
        public int ProfilePhotoId { get; set; }

        /// <summary>
        /// Gets or sets photo url (Amazon)
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets photo description
        /// </summary>
        public string Descripton { get; set; }

        /// <summary>
        /// Gets or sets reference to profile
        /// </summary>
        public virtual Profile Profile { get; set; }
    }
}
