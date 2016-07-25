using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.Enums;
using TouristGuide.INF.Models;
using TouristGuide.UI.Models;
using Microsoft.AspNet.Identity;

namespace TouristGuide.UI.Controllers
{
    /// <summary>
    /// Controller for getting coordinates from database to UI
    /// </summary>
    public class CoordsController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        public CoordsController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Method that gets all coordinates from database and send them as json-type file
        /// </summary>
        /// <returns>coords in json type</returns>
        public JsonResult GetAllCoords()
        {
            IEnumerable<CoordViewModel> coordList = null;
            if (User.IsInRole(UserRoleEnum.Admin.ToString()))
            {
                coordList = this.unitOfWork.GuestBL.GetAllLocations()
                    .Where(profile => profile.Address.Profile.IsBanned == null && profile.Address.Profile.Company.CompanyId == User.Identity.GetUserId() && profile.Address.Profile.IsDeleted == false)
                    .Select(p => (new CoordViewModel
                    {
                        Id = p.LocationId,
                        Lantitude = p.XCoord,
                        Longtitude = p.YCoord,
                        Name = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Name,
                        TypeOfProfile = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Type.Name
                    }));
            }
            else
            {
                coordList = this.unitOfWork.GuestBL.GetAllLocations()
                    .Where(profile => profile.Address.Profile.IsBanned == null && profile.Address.Profile.IsDeleted == false && profile.Address.Profile.IsShowed)
                   .Select(p => (new CoordViewModel
                   {
                       Id = p.LocationId,
                       Lantitude = p.XCoord,
                       Longtitude = p.YCoord,
                       Name = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Name,
                       TypeOfProfile = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Type.Name
                   }));
            }

            return this.Json(coordList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method that gets all Attraction coordinates from database and send them as json-type file
        /// </summary>
        /// <returns>coords in json type</returns>
        public JsonResult GetSightseeingsCoords()
        {
            IEnumerable<CoordViewModel> coordList = null;
            if (User.IsInRole(UserRoleEnum.Admin.ToString()))
            {
                coordList = this.unitOfWork.GuestBL.GetAllLocations()
                    .Where(profile => profile.Address.Profile.IsBanned == null && profile.Address.Profile.Company.CompanyId == User.Identity.GetUserId() && profile.Address.Profile.IsDeleted == false)
                    .Select(p => (new CoordViewModel
                    {
                        Id = p.LocationId,
                        Lantitude = p.XCoord,
                        Longtitude = p.YCoord,
                        Name = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Name,
                        TypeOfProfile = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Type.Name
                    }));
            }
            else
            {
                coordList = this.unitOfWork.GuestBL.GetAllLocations()
                      .Where(profile => profile.Address.Profile.IsBanned == null && profile.Address.Profile.IsDeleted == false && profile.Address.Profile.IsShowed == true)
                      .Select(p => (new CoordViewModel
                      {
                          Id = p.LocationId,
                          Lantitude = p.XCoord,
                          Longtitude = p.YCoord,
                          Name = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Name,
                          TypeOfProfile = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Type.Name
                      }));
            }
            coordList = coordList.Where(coord => (coord.TypeOfProfile == ProfileTypeEnum.Sightseeing.ToString()));

            return this.Json(coordList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method that gets all Restaurant coordinates from database and send them as json-type file
        /// </summary>
        /// <returns>coords in json type</returns>
        public JsonResult GetRestaurantCoords()
        {
            IEnumerable<CoordViewModel> coordList = null;
            if (User.IsInRole(UserRoleEnum.Admin.ToString()))
            {
                coordList = this.unitOfWork.GuestBL.GetAllLocations()
                     .Where(profile => profile.Address.Profile.IsBanned == null && profile.Address.Profile.Company.CompanyId == User.Identity.GetUserId() && profile.Address.Profile.IsDeleted == false)
                     .Select(p => (new CoordViewModel
                     {
                         Id = p.LocationId,
                         Lantitude = p.XCoord,
                         Longtitude = p.YCoord,
                         Name = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Name,
                         TypeOfProfile = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Type.Name
                     }));
            }
            else
            {
                coordList = this.unitOfWork.GuestBL.GetAllLocations()
                    .Where(profile => profile.Address.Profile.IsBanned == null && profile.Address.Profile.IsDeleted == false && profile.Address.Profile.IsShowed == true)
                    .Select(p => (new CoordViewModel
                    {
                        Id = p.LocationId,
                        Lantitude = p.XCoord,
                        Longtitude = p.YCoord,
                        Name = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Name,
                        TypeOfProfile = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Type.Name
                    }));
            }
            coordList = coordList.Where(coord => (coord.TypeOfProfile == ProfileTypeEnum.Restaurant.ToString()));

            return this.Json(coordList, JsonRequestBehavior.AllowGet);
        }
        /// <summary>
        /// Method that gets all Leisure places coordinates from database and send them as json-type file
        /// </summary>
        /// <returns>coords in json type</returns>
        public JsonResult GetLeisureCoords()
        {
            IEnumerable<CoordViewModel> coordList = null;
            if (User.IsInRole(UserRoleEnum.Admin.ToString()))
            {
                coordList = this.unitOfWork.GuestBL.GetAllLocations()
                .Where(profile => profile.Address.Profile.IsBanned == null && profile.Address.Profile.Company.CompanyId == User.Identity.GetUserId() && profile.Address.Profile.IsDeleted == false)
                .Select(p => (new CoordViewModel
                {
                    Id = p.LocationId,
                    Lantitude = p.XCoord,
                    Longtitude = p.YCoord,
                    Name = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Name,
                    TypeOfProfile = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Type.Name
                }));
            }
            else
            {
                coordList = this.unitOfWork.GuestBL.GetAllLocations()
                    .Where(profile => profile.Address.Profile.IsBanned == null && profile.Address.Profile.IsDeleted == false && profile.Address.Profile.IsShowed == true)
                    .Select(p => (new CoordViewModel
                    {
                        Id = p.LocationId,
                        Lantitude = p.XCoord,
                        Longtitude = p.YCoord,
                        Name = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Name,
                        TypeOfProfile = this.unitOfWork.GuestBL.GetProfileById(p.LocationId).Type.Name
                    }));
            }
            coordList = coordList.Where(coord => (coord.TypeOfProfile == ProfileTypeEnum.Leisure.ToString()));

            return this.Json(coordList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Send one coordinates from database as json-type file
        /// </summary>
        /// <param name="RestaurantId"></param>
        /// <returns>Coordinate by restaurant id</returns>
        public JsonResult GetCoordById(int RestaurantId)
        {
            Location location = unitOfWork.GuestBL.GetLocationById(RestaurantId);
            CoordViewModel coord = new CoordViewModel
            {
                Lantitude = location.XCoord,
                Longtitude = location.YCoord
            };
            return this.Json(coord, JsonRequestBehavior.AllowGet);
        }
    }
}
