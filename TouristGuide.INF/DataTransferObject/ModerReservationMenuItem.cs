using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.INF.DataTransferObject
{
    public class ModerReservationMenuItem
    {
        /// <summary>
        /// Gets or sets  MenuItemname
        /// </summary>
        public string MenuItemName { get; set; }

        /// <summary>
        /// Gets or sets count of menu item
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets  URL
        /// </summary>
        public string MenuItemURL { get; set; }
    }
}
