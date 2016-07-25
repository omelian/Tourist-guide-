using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    public class ResetPasswordViewModel
    {
        /// <summary>
        /// Gets or sets Email 
        /// </summary>
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

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