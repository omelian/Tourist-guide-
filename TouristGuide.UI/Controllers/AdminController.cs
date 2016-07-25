using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.UI.Models;
using Microsoft.AspNet.Identity;
using TouristGuide.INF.Enums;
using NLog;

namespace TouristGuide.UI.Controllers
{
    /// <summary>
    /// Administrator Controller
    /// </summary>
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Initializes a new instance of the AdminController class
        /// </summary>
        /// <param name="administrator">administrator</param>
        public AdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }
        
        /// <summary>
        /// Get All moderators by restaurant Id
        /// </summary>
        /// <param name="profileId">profile Id</param>
        /// <returns>List of Moderators</returns>
        public JsonResult GetModeratorsByRestaurantId(int profileId)        
        {
            var moderList = this.unitOfWork.AdminBL.GetAllModerators(profileId);
            var viewModerList = new List<ModeratorViewModel>();          
            foreach (var item in moderList)
            {
                viewModerList.Add(new ModeratorViewModel
                {
                    Id = item.Id,
                    Email = item.Email
                });
            }

            return this.Json(viewModerList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add new moderator to restaurant
        /// </summary>
        /// <param name="moderator">Moderator e-mail and password</param>
        /// <param name="profileId">Profile Id</param>
        /// <returns>Result operation</returns>
        [HttpPost]
        public ActionResult AddModerator(ModeratorViewModel moderator, int profileId)
        {
            if (ModelState.IsValid)
            {
                UserRegisterModel user = new UserRegisterModel { Email = moderator.Email, Password = moderator.Password, ConfirmPassword = moderator.ConfirmPassword };
                var result = this.unitOfWork.AdminBL.AddModerator(user, profileId);
                if (result.Successfully)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.Created);
                }
                else
                {
                    logger.Error(result.Message);
                    return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
                }
            }
            else
            {
                logger.Error("Model is not valid");
                return new HttpStatusCodeResult(HttpStatusCode.Conflict);
            }
        }

        public ActionResult SetModerator(string email, int profileId)
        {
            if(ModelState.IsValid)
            {
                var result = this.unitOfWork.AdminBL.SetModerator(email, profileId);
                if(result.Successfully)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.OK);
                }
                else 
                {
                    return  Content(result.Message);
                }
                
            }
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Delete moderator from profile
        /// </summary>
        /// <param name="moderId">user Id</param>
        /// <returns>Result operations</returns>
        [HttpPost]
        public ActionResult DeleteModerator(string moderId)
        {
            var result = this.unitOfWork.AdminBL.DeleteModerator(moderId);
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        /// <summary>
        /// Add new restaurant
        /// </summary>
        /// <param name="restaurant">Restaurant items</param>
        /// <returns>Result operation</returns>
        [HttpPost]
        public ActionResult AddProfile(ProfileAddViewModel newProfile)
        {

            newProfile.CompanyId = User.Identity.GetUserId();
            ProfileAddModel profile = new ProfileAddModel { Name = newProfile.Name, City = newProfile.City, Country = newProfile.Country, Street = newProfile.Street, CompanyId = newProfile.CompanyId, XCoord = newProfile.XCoord, YCoord = newProfile.YCoord, Number = newProfile.Number, ProfileType = (ProfileTypeEnum)newProfile.ProfileType };
            var result = this.unitOfWork.AdminBL.AddRestaurant(profile);
            if (result.Successfully)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }             
        }

        /// <summary>
        /// Edit  profile
        /// </summary>
        /// <param name="restaurant">Restaurant items</param>
        /// <returns>Result operation</returns>
        [HttpPost]
        public ActionResult EditProfile(ProfileEditViewModel newProfile)
        {
            
            ProfileEditModel profile = new ProfileEditModel { Name = newProfile.Name, City = newProfile.City, Country = newProfile.Country, Street = newProfile.Street, CompanyId = newProfile.CompanyId, XCoord = newProfile.XCoord, YCoord = newProfile.YCoord, Number = newProfile.Number, Id = newProfile.Id};
            var result = this.unitOfWork.AdminBL.EditProfile(profile);
            if (result.Successfully)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
            }
        }

        /// <summary>
        /// Delete  profile
        /// </summary>
        /// <param name="restaurant">Restaurant items</param>
        /// <returns>Result operation</returns>
        [HttpPost]
        public ActionResult DeleteProfile(int id)
        {
            var result = this.unitOfWork.AdminBL.DeleteProfile(id);
            if (result.Successfully)
            {
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
            else
            {
                return Content(result.Message);
            }           
        }
    }
}