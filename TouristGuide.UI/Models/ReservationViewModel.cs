using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TouristGuide.INF.DataTransferObject;

namespace TouristGuide.UI.Models
{
    /// <summary>
    /// reservation View Model
    /// </summary>
    public class ReservationViewModel
    {
        /// <summary>
        /// reservation id
        /// </summary>
         public int ReservationId { get; set; }


        /// <summary>
        /// number of persons that reserv 
        /// </summary>
        public int NumberOfPersons { get; set; }

        /// <summary>
        /// date of reservation
        /// </summary>
        public string ReservationDate { get; set; }

        /// <summary>
        /// User Name that reservation done
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Profile Id that reservation done
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Profile Name that reservation done
        /// </summary>
        public string ProfileName{ get; set; }

        /// <summary>
        /// Gets or sets collection of menu items for reservation
        /// </summary>
        public List<RestaurantReservationMenuModel> MenuItems { get; set; }

    }
}