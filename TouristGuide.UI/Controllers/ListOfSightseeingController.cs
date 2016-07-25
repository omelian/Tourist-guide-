using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.BLL.Interfaces;
using TouristGuide.BLL;
using TouristGuide.INF.Models;
using TouristGuide.INF.Enums;
using TouristGuide.UI.Models;
using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Specialized;
using System.Configuration;
using Amazon;
using System.Globalization;

namespace TouristGuide.UI.Controllers
{
    /// <summary>
    /// Attractions manage class
    /// </summary>
    public class ListOfSightseeingController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        /// <summary>
        /// Initializes  GuestBL and UserBL classes
        /// </summary>
        /// <param name="guest">Guest Interface</param>
        /// <param name="user">User Interface</param>
        /// <see cref="ListOfRestaurantController(IGuestBL guest,IUserBL user)"/>
        public ListOfSightseeingController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets Restaurants from data Base
        /// </summary>
        /// <returns>JSON Result All Profiles</returns>
        public JsonResult GetAllSightseeings()
        {
            IEnumerable<ProfileListViewModel> sightseeingList = null;
            if (User.IsInRole(UserRoleEnum.Admin.ToString()))
            {
                sightseeingList = this.unitOfWork.GuestBL.GetAllSightseeings()
                .Where(p => p.Company.User.Id == User.Identity.GetUserId() && p.IsBanned == null && p.IsDeleted == false).ToList()
                .Select(profile => (
               new ProfileListViewModel
               {
                   ProfileId = profile.ProfileId,
                   Name = profile.Name,
                   City = profile.Address.City,
                   Street = profile.Address.Street,
                   Number = profile.Address.Number,
                   XCoord = profile.Address.Location.XCoord,
                   YCoord = profile.Address.Location.YCoord,
                   Rate = ListOfRestaurantController.CalculateAvgProfileRate(profile.Rates),
                   MainPhotoUrl = this.unitOfWork.GuestBL.GetMainPhotoOfProfileById(profile.MainPhoto),
                   IsShowed = profile.Address.Profile.IsShowed
               }));
            }
            else
            {
                if (User.IsInRole(UserRoleEnum.Moderator.ToString()))
                {
                    sightseeingList = this.unitOfWork.GuestBL.GetAllSightseeings()
                    .Where(profile => profile.IsBanned == null && profile.IsDeleted == false)
                    .Select(profile => (
              new ProfileListViewModel
              {
                  ProfileId = profile.ProfileId,
                  Name = profile.Name,
                  City = profile.Address.City,
                  Street = profile.Address.Street,
                  Number = profile.Address.Number,
                  MainPhotoUrl = this.unitOfWork.GuestBL.GetMainPhotoOfProfileById(profile.MainPhoto),
                  Rate = CalculateAvgProfileRate(profile.Rates),
                  IsShowed = profile.IsShowed
              }));
                }
                else
                {
                    sightseeingList = this.unitOfWork.GuestBL.GetAllSightseeings()
                        .Where(profile => profile.IsBanned == null && profile.IsDeleted == false && profile.IsShowed == true)
                        .Select(profile => (
                  new ProfileListViewModel
                  {
                      ProfileId = profile.ProfileId,
                      Name = profile.Name,
                      City = profile.Address.City,
                      Street = profile.Address.Street,
                      Number = profile.Address.Number,
                      Rate = ListOfRestaurantController.CalculateAvgProfileRate(profile.Rates),
                      MainPhotoUrl = this.unitOfWork.GuestBL.GetMainPhotoOfProfileById(profile.MainPhoto)
                  }));
                }

            }
            return this.Json(sightseeingList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Calculate avg rate
        /// </summary>
        /// <param name="rates">Collection of rates</param>
        /// <returns>Avg Rate</returns>
        public static double CalculateAvgProfileRate(ICollection<Rate> rates)
        {
            double result = 0;
            var profileMarksSum = 0;
            foreach (var item in rates)
            {
                profileMarksSum += item.Mark;
            }

            if (rates.Count > 0)
            {
                result = profileMarksSum / rates.Count;
            }

            return result;
        }

        /// <summary>
        /// Add favorite 
        /// </summary>    
        /// <returns>Json with rconfirm</returns>
        [HttpPost]
        public ActionResult AddFavorite(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.unitOfWork.UserBL.AddFavorite(id, User.Identity.GetUserId());
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Remove favorite 
        /// </summary>    
        /// <returns>Json with rconfirm</returns>
        [HttpPost]
        public ActionResult RemoveFavorite(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.unitOfWork.UserBL.DeleteFavorite(id, User.Identity.GetUserId());
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Check if in favorites
        /// </summary>
        /// <param name="id">ID of profile</param>
        public ActionResult IsInFavorites(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (this.unitOfWork.UserBL.HaveInFavorites(id, User.Identity.GetUserId()))
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}