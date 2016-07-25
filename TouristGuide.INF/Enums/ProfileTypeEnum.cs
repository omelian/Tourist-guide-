using System.ComponentModel.DataAnnotations;

namespace TouristGuide.INF.Enums
{
    /// <summary>
    /// Enum for Profile Type
    /// </summary>
    public enum ProfileTypeEnum
    {
        /// <summary>
        /// Restaurant enum
        /// </summary>
        [Display(Name = "Restaurant")]
        Restaurant,

        /// <summary>
        /// Attraction enum
        /// </summary>
        [Display(Name = "Sightseeing")]
        Sightseeing,

        /// <summary>
        /// Leisure enum
        /// </summary>
        [Display(Name = "Leisure")]
        Leisure


    }
}
