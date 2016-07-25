using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Sighseeing event model
    /// </summary>
    public class Event
    {
        /// <summary>
        /// Gets or sets event id
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets event name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets description
        /// </summary>
        public string  Description { get; set; }

        /// <summary>
        /// Gets or sets event photo
        /// </summary>
        public string EventPhoto { get; set; }

        /// <summary>
        /// Gets or sets date of event
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public virtual DateTime EventDate { get; set; }
        
        /// <summary>
        /// Gets or sets price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets  event duration
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets number of participant
        /// </summary>
        public int NumberOfParticipant { get; set; }

        /// <summary>
        /// Gets or sets reference who have event 
        /// </summary>
        public virtual Profile Profile { get; set; }
    }
}
