using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TouristGuide.UI.Models
{
    public class AdminRequestsViewModel
    {
        /// <summary>
        /// Admin id value
        /// </summary>
        public string AdminId { get; set; }

        /// <summary>
        /// Admin name with requests
        /// </summary>
        public string AdminName { get; set; }

        /// <summary>
        /// Comany name that admin want to approve
        /// </summary>
        public string CompanyName { get; set; }

        /// <summary>
        /// Approved request state
        /// </summary>
        public string RequestState { get; set; }

        /// <summary>
        /// Request description
        /// </summary>
        public string RequestDescription { get; set; }
    }
}