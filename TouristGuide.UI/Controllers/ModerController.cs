using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TouristGuide.BLL.Interfaces;
using NLog;

namespace TouristGuide.UI.Controllers
{
    public class ModerController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        /// <summary>
        /// Logger
        /// </summary>
        private Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        ///  User Interface
        /// </summary>
        public ModerController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Edit  profile property isShowed
        /// </summary>
        /// <param name="isShowed">Boolean value if is showed</param>
        /// <param name="profileId">Profile Id</param>
        /// <returns>Result operation</returns>
        [HttpPost]
        public ActionResult EditProfile(bool IsShowed, int profileId)
        {
            IsShowed = !IsShowed;
            var result = this.unitOfWork.ModeratorBL.EditIsShowed(IsShowed, profileId);
            if (result.Successfully)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                logger.Error(result.Message);
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }
    }
}
