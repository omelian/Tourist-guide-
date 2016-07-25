using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.INF.Models;

namespace TouristGuide.UI.Models
{
    public class ModerReservationViewModel
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
        /// Gets or sets who have a Reservation 
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets  who have a Reservation 
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets  Type profile
        /// </summary>
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets  who have a Reservation 
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets collection of meals
        /// </summary>
        public ICollection<ModerReservationMenuItem> Meals { get; set; }
    }
}