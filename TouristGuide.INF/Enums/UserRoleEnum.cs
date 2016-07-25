using System.ComponentModel.DataAnnotations;

namespace TouristGuide.INF.Enums
{
    /// <summary>
    /// Enum for user roles
    /// </summary>
    public enum UserRoleEnum
    {
        /// <summary>
        /// User enum
        /// </summary>
       [Display(Name = "User")]
        User,

        /// <summary>
        /// Admin enum
        /// </summary>
        [Display(Name = "Admin")]
        Admin,

        /// <summary>
        /// Moderator enum
        /// </summary>
        [Display(Name = "Moderator")]
        Moderator,

        /// <summary>
        /// SuperAdmin enum
        /// </summary>
        [Display(Name = "SuperAdmin")]
        SuperAdmin
    }
}
