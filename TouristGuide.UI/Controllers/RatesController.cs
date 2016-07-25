using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.Models;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.UI.Models;

namespace TouristGuide.UI.Controllers
{
    public class RatesController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        public RatesController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        public JsonResult GetRatesByRestaurantId(int restaurantId)
        {
            Rate[] rates = this.unitOfWork.GuestBL.GetProfileById(restaurantId).Rates.ToArray();
            List<ProfileRateViewModel> restaurantRates = new List<ProfileRateViewModel>();
            for (int i = 0; i < rates.Length; ++i)
            {
                if (rates[i] != null)
                {

                        ProfileRateViewModel restaurantRate = new ProfileRateViewModel();
                        restaurantRate.Mark = rates[i].Mark;
                        restaurantRate.RateId = rates[i].RateId;
                        restaurantRates.Add(restaurantRate);
                }

            }

            ProfileRateViewModel[] ProfileRatesArray = restaurantRates.ToArray();
            return this.Json(ProfileRatesArray, JsonRequestBehavior.AllowGet);


        }

        [HttpPost]
        public void SetRestaurantRate(int restaurantId, int mark, string userId)
        {
            ProfileRate rate = new ProfileRate();
            rate.Mark = mark;
            rate.UserId = userId;
            rate.ProfileId = restaurantId;
            this.unitOfWork.UserBL.SetRate(rate);
        }

    }
}