using System;
using System.ComponentModel.DataAnnotations;
using TouristGuide.INF.Enums;

namespace TouristGuide.INF.DataTransferObject
{
    /// <summary>
    /// Model for user registration
    /// </summary>
    public class UserRegisterModel
    {
        /// <summary>
        /// Gets or sets Email 
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets FirstName
        /// </summary>
        [Required]
        [Display(Name = "First Name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets LastName
        /// </summary>
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Role
        /// </summary>      
        [Display(Name = "Role")]
        public UserRoleEnum Role { get; set; }

        /// <summary>
        /// Gets or sets Company
        /// </summary>
        [Required]
        [Display(Name = "Company")]
        public string Company { get; set; }

        /// <summary>
        /// Gets or sets Password
        /// </summary>
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be longer than {2} symbols", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets ConfirmPassword
        /// </summary>
        [DataType(DataType.Password)]
        [Display(Name = "Comfirm password")]
        [System.ComponentModel.DataAnnotations.Compare("Password", ErrorMessage = "Passwords are different")]
        public string ConfirmPassword { get; set; }
    }
}
