using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Address model
    /// </summary>
    public class Address
    {
        /// <summary>
        /// Gets or sets Id for Address, and it is a ForeignKey for Profile model
        /// </summary>
        [Key, ForeignKey("Profile")]
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or sets Name of country
        /// </summary>
        public string Country { get; set; }

        /// <summary>
        /// Gets or sets Name of City
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets Name of Street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        /// Gets or sets Number of House
        /// </summary>
        public string Number { get; set; }
       
        /// <summary>
        /// Gets or sets Reference with Profile model
        /// </summary>
        public virtual Profile Profile { get; set; }

        /// <summary>
        /// Gets or sets Reference with Location model
        /// </summary>
        public virtual Location Location { get; set; }
    }
}