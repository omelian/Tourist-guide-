using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.INF.Models;
using TouristGuide.UI.Models;

namespace TouristGuide.UI.Controllers
{
    public class ReservationController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;


        public ReservationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

         /// <summary>
        /// Method that gets all coordinates from database and send them as json-type file
        /// </summary>
        /// <param name="dateString">Date of reservation</param>
        /// <param name="numberOfPersons">Number of Persons</param>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <returns>result of adding in json type</returns>
        [HttpPost]
        public JsonResult AddReservation(int? numberOfPersons, string dateString, int? restaurantId)
        {
            if (numberOfPersons != null && restaurantId != null)
            {

                Reservation newReserv = new Reservation();
                if (this.User.Identity.IsAuthenticated)
                {
                    newReserv.User = new ApplicationUser() { UserName = this.User.Identity.Name };
                }
                newReserv.Profile = new Profile() { ProfileId =(int) restaurantId };
                newReserv.NumberOfPersons =(int) numberOfPersons;
                newReserv.ReservationDate = DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                OperationDetails operationDetails = this.unitOfWork.UserBL.AddReservation(newReserv);
            }
            return null;
        }

        /// <summary>
        /// Method that gets all coordinates from database and send them as json-type file
        /// </summary>
        ///<param name="userName">User Name</param>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <returns>reservation in json type</returns>
        public JsonResult GetReservationByUserName(string userName,int restaurantId) 
        {
            IEnumerable<Reservation> reservations = unitOfWork.UserBL.GetReservationsByUserName(userName);
            if(reservations!=null){
                 IEnumerable<ReservationViewModel> reservationList = reservations.Select(reserv=>
                new ReservationViewModel
                {
                    ReservationId = reserv.ReservationId,
                    ReservationDate = reserv.ReservationDate.ToString("dd/MM/yyyy H:mm"),
                    NumberOfPersons = reserv.NumberOfPersons,
                    UserName = reserv.User.UserName,
                    ProfileId = reserv.Profile.ProfileId,
                    ProfileName =reserv.Profile.Name
                });

                ReservationViewModel reservation = null;
                foreach (ReservationViewModel r in reservationList) 
                {
                    if (r.ProfileId == restaurantId)
                        reservation = r;
                }
                if (reservation != null)
                {
                    reservation.MenuItems = this.unitOfWork.UserBL.GetMenuForRestaurantReservation(reservation.ReservationId).ToList();
                    return this.Json(reservation, JsonRequestBehavior.AllowGet);
                }
                else
                    return null;
            }
            else
                return null;
         }
            
        


        /// <summary>
        /// Method that gets all reservations from database and send them as json-type file
        /// </summary>
        ///<param name="userName">User Name</param>
        /// <returns>reservations in json type</returns>
        public JsonResult GetAllReservationsByUserName(string userName)
        {

            IEnumerable<Reservation> reservation = unitOfWork.UserBL.GetReservationsByUserName(userName);
            if (reservation != null)
            {
                IEnumerable<ReservationViewModel> reservationList = reservation.Select(reserv =>
                new ReservationViewModel
                {
                    ReservationId = reserv.ReservationId,
                    ReservationDate = reserv.ReservationDate.ToString("dd/MM/yyyy H:mm"),
                    NumberOfPersons = reserv.NumberOfPersons,
                    UserName = reserv.User.UserName,
                    ProfileId = reserv.Profile.ProfileId,
                    ProfileName = reserv.Profile.Name
                });            
                return this.Json(reservationList, JsonRequestBehavior.AllowGet);
            }
            else
               return null;
            
        }

        /// <summary>
        /// Method that gets all coordinates from database and send them as json-type file
        /// </summary>
        /// <param name="dateString">Date of reservation</param>
        /// <param name="numberOfPersons">Number of Persons</param>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <returns>coords in json type</returns>
        [HttpPost]
        public JsonResult EditReservation(int numberOfPersons, string dateString, int reservationId)
        {
            
                Reservation reservation = new Reservation();
                if (this.User.Identity.IsAuthenticated)
                {
                    reservation.User = new ApplicationUser() { UserName = this.User.Identity.Name };
                }

                reservation.ReservationId = reservationId;
                reservation.NumberOfPersons = numberOfPersons;
                reservation.ReservationDate = DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                OperationDetails operationDetails = this.unitOfWork.UserBL.EditReservation(reservation);
                return null;
        }

        /// <summary>
        /// Method that updates restaurant reservation menu 
        /// </summary>
        /// <param name="reservationId">Reservation Id</param>
        /// <param name="menu">Menu items</param>
        [HttpPost]
        public void UpdateRestaurantReservationMenu(int reservationId, RestaurantReservationMenuModel[] menu)
        {
            ICollection<RestaurantReservationMenuItem> list = new List<RestaurantReservationMenuItem>();
            foreach (var item in menu)
            {
                RestaurantReservationMenuItem restaurantMenuItem = new RestaurantReservationMenuItem();
                restaurantMenuItem.MenuItemId = item.MenuId;
                restaurantMenuItem.Count = item.Count;
                restaurantMenuItem.RestaurantReservationMenuItemId = item.RestaurantReservationMenuModelId;
                list.Add(restaurantMenuItem);
            }

            this.unitOfWork.UserBL.UpdateRestaurantReservationMenu(reservationId, list);
        }

        /// <summary>
        /// Method that delete reservation
        /// </summary>
        /// <param name="reservationId">Reservation Id</param>
        [HttpPost]
        public void DeleteReservation(int reservationId)
        {
            this.unitOfWork.UserBL.DeleteReservation(reservationId);
        }

	}
    
}