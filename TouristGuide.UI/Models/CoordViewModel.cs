using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    /// <summary>
    /// Class for coordinate object type
    /// </summary>
    public class CoordViewModel
    {
        /// <summary>
        /// x-coordinate
        /// </summary>
        public double Lantitude { get; set; }

        /// <summary>
        /// y-coordinate
        /// </summary>
        public double Longtitude { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Coord Type of Profile
        /// </summary>
        public string TypeOfProfile { get; set; }
        /// <summary>
        /// Coord id
        /// </summary>
        public int Id { get; set; }
    }
}