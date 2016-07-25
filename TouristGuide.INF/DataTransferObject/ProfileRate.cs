using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.INF.DataTransferObject
{
    public class ProfileRate
    {
        /// <summary>
        /// Gets or sets rate mark
        /// </summary>
        public int Mark { get; set; }

        /// <summary>
        /// Gets or sets profile id value
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        /// Gets or sets users id value
        /// </summary>
        public string UserId { get; set; }
    }
}
