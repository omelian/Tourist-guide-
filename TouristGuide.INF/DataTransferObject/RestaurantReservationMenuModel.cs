using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.INF.DataTransferObject
{
    /// <summary>
    /// class for object reservation menu which is used to getting datas from db
    /// </summary>
    public class RestaurantReservationMenuModel
    {
        /// <summary>
        /// RestaurantReservationMenuModel Id
        /// </summary>
        public int RestaurantReservationMenuModelId { get; set; }

        /// <summary>
        /// Menu item Id
        /// </summary>
        public int MenuId { get; set; }

        /// <summary>
        /// name field
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// price field
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// Calories field
        /// </summary>
        public int Callories { get; set; }

        /// <summary>
        /// Count of items
        /// </summary>
        public int Count { get; set; }
    }
}