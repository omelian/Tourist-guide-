using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    /// <summary>
    /// Event view model class
    /// </summary>
    public class EventViewModel
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
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets event photo
        /// </summary>
        public string EventPhoto { get; set; }

        /// <summary>
        /// Gets or sets event price
        /// </summary>
        public double Price { get; set; }

        /// <summary>
        /// Gets or sets date of event
        /// </summary>
        public string EventDate { get; set; }

        /// <summary>
        /// Gets or sets  event duration
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets number of participant
        /// </summary>
        public int NumberOfParticipant { get; set; }

        /// <summary>
        /// Gets or sets profile id
        /// </summary>
        public int ProfileId { get; set; }
    }
}