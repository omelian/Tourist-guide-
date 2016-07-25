using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Location model
    /// </summary>
    public class Location
    {
        /// <summary>
        /// Gets or sets location id
        /// </summary>
        [Key, ForeignKey("Address")]
        public int LocationId { get; set; }

        /// <summary>
        /// Gets or sets x coordinate
        /// </summary>
        public double XCoord { get; set; }

        /// <summary>
        /// Gets or sets y coordinate
        /// </summary>
        public double YCoord { get; set; }

        /// <summary>
        /// Gets or sets reference 
        /// </summary>
        public virtual Address Address { get; set; }
    }
}