using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.INF.Models
{
    public class RestaurantReservationMenuItem
    {
        /// <summary>
        /// Gets or sets id of RestaurantReservationMenuItem
        /// </summary>
        public int RestaurantReservationMenuItemId { get; set; }

        /// <summary>
        /// Gets or sets  reservation
        /// </summary>
        public int ReservationId { get; set; }

        /// <summary>
        /// Gets or sets  MenuItem
        /// </summary>
        public int MenuItemId { get; set; }

        /// <summary>
        /// Gets or sets count of menu item
        /// </summary>
        public int Count { get; set; } 
    }
}
