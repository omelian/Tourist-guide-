using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Reservation model
    /// </summary>
    public class Reservation
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
        [Column(TypeName = "DateTime2")]
        public virtual DateTime ReservationDate { get; set; }

        /// <summary>
        /// Gets or sets reference who have a comment 
        /// </summary>
        public virtual Profile Profile { get; set; }

        /// <summary>
        /// Gets or sets reference who gives a comment
        /// </summary>
        public virtual ApplicationUser User { get; set; }
    }
}