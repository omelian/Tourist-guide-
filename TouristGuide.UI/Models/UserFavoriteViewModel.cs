using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TouristGuide.INF.Models;

namespace TouristGuide.UI.Models
{
    public class UserFavoriteViewModel
    {

            /// <summary>
            /// Gets or sets profile id
            /// </summary>
            public int ProfileId { get; set; }

            /// <summary>
            /// Gets or sets profile name
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// Gets or sets main photo id
            /// </summary>
            public string MainPhoto { get; set; }

            /// <summary>
            /// Gets or sets type profile
            /// </summary>
            public string Type { get; set; }
        }
    }
