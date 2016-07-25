using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    /// <summary>
    /// class for object menu which is used to getting datas from db
    /// </summary>
    public class MenuModel
    {
        /// <summary>
        /// field for id number
        /// </summary>
        public int ProfileId { get; set; }
        /// <summary>
        /// name field
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// price field
        /// </summary>
        public double Price { get; set; }
        /// <summary>
        /// description field
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Filed with url where is picture
        /// </summary>
        public string PictureUrl { get; set; }
        /// <summary>
        /// Field which describes dish type
        /// </summary>
        public string DishType { get; set; }
        /// <summary>
        /// Callories field
        /// </summary>
        public int Callories { get; set; }
        /// <summary>
        /// Preparation time
        /// </summary>
        public string PreparationTime { get; set; }

        /// <summary>
        /// Menu item Id
        /// </summary>
        public int MenuId { get; set; }
    }
}