using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    public class UserReservationViewModel
    {
        /// <summary>
        /// Gets or sets reservation id
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// Gets or sets number of persons
        /// </summary>
        public int NumberOfPersons { get; set; }

        /// <summary>
        /// Gets or sets date of reservation
        /// </summary>
        public string ReservationDate { get; set; }

        /// <summary>
        /// Gets or sets  who have a comment 
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets  who have a comment 
        /// </summary>
        public string  ProfileName { get; set; }

        /// <summary>
        /// Gets or sets main photo id
        /// </summary>
        public string MainPhoto { get; set; }

        /// <summary>
            /// Gets or sets type profile
            /// </summary>
            public string Type { get; set; }
    }
}