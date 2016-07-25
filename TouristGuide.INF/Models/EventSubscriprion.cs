using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Sighseeing event subscription model
    /// </summary>
    public class EventSubscription
    {
        /// <summary>
        /// Gets or sets subscription id
        /// </summary>
        [Key]
        public int SubscriptionId { get; set; }

        /// <summary>
        /// Gets or sets number of persons
        /// </summary>
        public int NumberOfPersons { get; set; }

      
        /// <summary>
        /// Gets or sets reference who gives a comment
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets reference who have event  subscription
        /// </summary>
        public virtual Event Event { get; set; }
    }
}

