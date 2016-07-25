using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TouristGuide.INF.Enums
{
    public enum RequestStateEnum
    {
        /// <summary>
        /// Request has been approved
        /// </summary>
        Approved,

        /// <summary>
        /// Request has been rejected
        /// </summary>
        Rejected,

        /// <summary>
        /// Request hasn't been considered yet
        /// </summary>
        InProcess
    }
}
