using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.Models;
using TouristGuide.UI.Models;

namespace TouristGuide.UI.Controllers
{
    /// <summary>
    /// Controller for getting events from database to UI
    /// </summary>
    public class EventController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

         /// <summary>
        /// Initializes  GuestBL  classes
        /// </summary>
        /// <param name="guest">Guest Interface</param>
        /// <see cref="ListOfRestaurantController(IGuestBL guest)"/>
        public EventController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Gets events for sightseeig page
        /// </summary>
        /// <param name="restaurantId"> Profile Id</param>
        /// <returns>JSON Result with Profile Comments</returns>
        public JsonResult GetEventBySightseeingId(int sightseeingId)
        {
            IEnumerable<EventViewModel> profileEvents = this.unitOfWork.GuestBL.GetEventsBySightseeingId(sightseeingId).Select(
                evnt => (new EventViewModel {
                    EventId = evnt.EventId,
                    Name = evnt.Name,
                    Description = evnt.Description,
                    EventPhoto = evnt.EventPhoto,
                    EventDate = evnt.EventDate.ToString(),
                    Duration = evnt.Duration,
                    Price = evnt.Price,
                    NumberOfParticipant= evnt.NumberOfParticipant,
                    ProfileId = evnt.Profile.ProfileId
                })
                );


            return this.Json(profileEvents, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method that add event to database
        /// </summary>
        /// <param name="dateString">Date of reservation</param>
        /// <param name="numberOfPersons">Number of Persons</param>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <returns>result of adding in json type</returns>
        [HttpPost]
        public JsonResult AddEvent(int profileId, string name, string description, string dateString, double price, int duration, int numberOfParticipant,string photoUrl)
        {
            

                Event newEvent = new Event();

                newEvent.Profile = new Profile() { ProfileId = profileId };
                newEvent.Name = name;
                newEvent.EventDate = DateTime.ParseExact(dateString, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
                newEvent.Description = description;
                newEvent.Price = price;
                newEvent.Duration = duration;
                newEvent.NumberOfParticipant = numberOfParticipant;
                newEvent.EventPhoto = photoUrl;
                OperationDetails operationDetails = this.unitOfWork.ModeratorBL.AddEvent(newEvent);
                return this.Json("Success", JsonRequestBehavior.AllowGet);
            
        }

        /// <summary>
        /// Method that add event to database
        /// </summary>
        /// <param name="dateString">Date of reservation</param>
        /// <param name="numberOfPersons">Number of Persons</param>
        /// <param name="restaurantId">Restaurant Id</param>
        /// <returns>result of adding in json type</returns>
        [HttpPost]
        public JsonResult UpdateEvent(int eventId,int profileId, string name, string description, string dateString, double price, int duration, int numberOfParticipant, string photoUrl)
        {
           

            Event newEvent = new Event();
            newEvent.EventId = eventId;
            newEvent.Profile = new Profile() { ProfileId = profileId };
            newEvent.Name = name;
            newEvent.EventDate = DateTime.ParseExact(dateString, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            newEvent.Description = description;
            newEvent.Price = price;
            newEvent.Duration = duration;
            newEvent.NumberOfParticipant = numberOfParticipant;
            newEvent.EventPhoto = photoUrl;
            OperationDetails operationDetails = this.unitOfWork.ModeratorBL.UpdateEvent(newEvent);
            return this.Json("Success", JsonRequestBehavior.AllowGet);

        }

        /// <summary>
        /// Method that delete event
        /// </summary>
        /// <param name="eventId">Event Id</param>
        [HttpPost]
        public void DeleteEvent(int eventId)
        {
            this.unitOfWork.ModeratorBL.DeleteEvent(eventId);
        }

        /// <summary>
        /// Gets events for sightseeig page
        /// </summary>
        /// <param name="profileId"> Profile Id</param>
        /// <param name="userName"> User name</param>
        /// <returns>JSON Result with Profile Comments</returns>
        public JsonResult GetEventSubscriptionByProfileId(int profileId, string userId)
        {
            IEnumerable<EventSubscription> profileEventSubscriptions = this.unitOfWork.UserBL.GetEventSubscriptionsByUserName(this.User.Identity.Name);
            IEnumerable<EventSubscriptionViewModel> userSubscriptions = null;
            if (profileEventSubscriptions != null)
            {
                userSubscriptions = profileEventSubscriptions.Select(
                evnt => (new EventSubscriptionViewModel
                {
                    SubscriptionId = evnt.SubscriptionId,
                    NumberOfPersons = evnt.NumberOfPersons,
                    UserId= evnt.User.Id,
                    ProfileId = evnt.Event.Profile.ProfileId,
                    EventId = evnt.Event.EventId,
                    EventName = evnt.Event.Name,
                    EventDate = evnt.Event.EventDate.ToString(),
                    EventPhoto = evnt.Event.EventPhoto

                })
                )
                .Where(evnt => evnt.ProfileId == profileId && evnt.UserId == userId);
            }

            return this.Json(userSubscriptions, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Method that add event to database
        /// </summary>
        /// <param name="numberOfPersons">Number of Persons</param>
        /// <param name="eventId">event Id</param>
        /// <returns>result of adding in json type</returns>
        [HttpPost]
        public JsonResult AddEventSubscription( int numberOfPersons, int eventId)
        {


            EventSubscription newEvent = new EventSubscription();

            newEvent.Event = new Event() { EventId = eventId };
            if (this.User.Identity.IsAuthenticated)
            {
                newEvent.User = new ApplicationUser() { UserName = this.User.Identity.Name };
            }

            newEvent.NumberOfPersons = numberOfPersons;
            OperationDetails operationDetails = this.unitOfWork.UserBL.AddEventSubscription(newEvent);
            return this.Json("Success", JsonRequestBehavior.AllowGet);

        }
        /// <summary>
        /// Gets event for sightseeig page
        /// </summary>
        /// <param name="eventId"> Event Id</param>
        /// <returns>JSON Result with Event Subscription</returns>
        public JsonResult GetEventSubscriptionByEventId(int eventId, string userId)
        {
            IEnumerable<EventSubscription> profileEventSubscription = this.unitOfWork.UserBL.GetEventSubscriptionsByEventId(eventId);
            if (profileEventSubscription != null)
            {
                 IEnumerable<EventSubscriptionViewModel> profileEventSubscriptionModels = profileEventSubscription.Select(subs => new EventSubscriptionViewModel
                    {
                        SubscriptionId = subs.SubscriptionId,
                        NumberOfPersons = subs.NumberOfPersons,
                        UserId= subs.User.Id,
                        ProfileId = subs.Event.Profile.ProfileId,
                        EventId = subs.Event.EventId,
                        EventName = subs.Event.Name,
                        EventDate = subs.Event.EventDate.ToString(),
                        EventPhoto = subs.Event.EventPhoto
                        
                         
                    })
                    .Where(subs => subs.UserId == userId);

                 if (profileEventSubscriptionModels.Count() >0)
                 {
                     EventSubscriptionViewModel model = profileEventSubscriptionModels.First();
                     return this.Json(model, JsonRequestBehavior.AllowGet);
                 }
                 else
                     this.Json(null, JsonRequestBehavior.AllowGet);
            }
            return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        // <summary>
        /// Method that delete event
        /// </summary>
        /// <param name="subscriptionId">Subscription Id</param>
        [HttpPost]
        public void DeleteEventSubscription(int subscriptionId)
        {
            this.unitOfWork.UserBL.DeleteEventSubscription(subscriptionId);
        }

    }
}