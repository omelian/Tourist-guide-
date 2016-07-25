using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    /// <summary>
    /// Profile Photo Model
    /// </summary>
    public class ProfilePhotoViewModel
    {
        /// <summary>
        /// Gets or sets photo id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets url to photo
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets decription to photo
        /// </summary>
        public string Descripton { get; set; }
    }
}