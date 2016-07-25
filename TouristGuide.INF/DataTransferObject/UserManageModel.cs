using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.INF.Enums;

namespace TouristGuide.INF.DataTransferObject
{
    public class UserManageModel
    {

        /// <summary>
        /// Gets or sets FirstName
        /// </summary>
        [Display(Name = "First Name")]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets LastName
        /// </summary>
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets Role
        /// </summary>
        [Display(Name = "Gender")]
        public GenderEnum Gender { get; set; }

        /// <summary>
        /// Gets or sets DateBirth
        /// </summary>
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:mm/dd/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateBirth { get; set; }
        /// <summary>
        /// Gets or sets Password
        /// </summary>
        [Display(Name = "Photo")]
        public string PhotoUrl { get; set; }
    }
}
