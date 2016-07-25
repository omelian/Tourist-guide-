using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    public class VerifyCodeViewModel
    {
        /// <summary>
        ///  Gets or sets profile news title
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        ///  Gets or sets profile news content
        /// </summary>
        public string Code { get; set; }
    }
}