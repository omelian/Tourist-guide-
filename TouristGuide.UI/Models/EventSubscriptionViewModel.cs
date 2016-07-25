using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    public class EventSubscriptionViewModel
    {

        /// <summary>
        /// Gets or sets subscription id
        /// </summary>
        public int SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets number of persons
        /// </summary>
        public int NumberOfPersons { get; set; }


        /// <summary>
        /// Gets or sets user name
        /// </summary>
        public string UserId{ get; set; }

        /// <summary>
        /// Gets or sets profile id
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets  event  id
        /// </summary>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets  event  id
        /// </summary>
        public string EventName { get; set; }

        /// <summary>
        /// Gets or sets  event  id
        /// </summary>
        public string EventDate { get; set; }

        /// <summary>
        /// Gets or sets  event  id
        /// </summary>
        public string EventPhoto { get; set; }
    }
}