using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Restaurant menu items class
    /// </summary>
    public class RestaurantMenuItem
    {
        /// <summary>
        /// Gets or sets restaurant menu items id
        /// </summary>
        public int RestaurantMenuItemId { get; set; }

        /// <summary>
        /// Gets or sets name field
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets url of photo
        /// </summary>
        public string ItemPhoto { get; set; }

        /// <summary>
        /// Gets or sets type of dish
        /// </summary>
        public string DishType { get; set; }

        /// <summary>
        /// Gets or sets done time for dish
        /// </summary>
        public string DoneTime { get; set; }

        /// <summary>
        /// Gets or sets calories for dish
        /// </summary>
        public int Calories { get; set; }

        /// <summary>
        /// Gets or sets reference for profile
        /// </summary>
        public virtual Profile Profile { get; set; } 
    }
}
