using System.ComponentModel.DataAnnotations;

namespace TouristGuide.INF.Enums
{
    /// <summary>
    /// Enum for Gender
    /// </summary>
    public enum GenderEnum
    {
        /// <summary>
        /// Male enum
        /// </summary>
        [Display(Name = "Male")]
        Male,

        /// <summary>
        /// Female enum
        /// </summary>
        [Display(Name = "Female")]
        Female
    }
}
