using System;
using System.Collections.Generic;
using System.Linq;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Models;

namespace TouristGuide.DAL.Repositories
{
    /// <summary>
    /// Repository for events
    /// </summary>
    public class EventRepository : IRepository<Event>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the ArticleRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public EventRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new Event item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(Event item)
        {
            this.dataBase.Events.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get Event item by id
        /// </summary>
        /// <param name="id">Id of Article item</param>
        /// <returns>Event item</returns>
        public Event Get(object id)
        {
            return this.dataBase.Events.Find(id);
        }

        /// <summary>
        /// Get collection of all Event items 
        /// </summary>
        /// <returns>Collection of Events</returns>
        public IEnumerable<Event> GetAll()
        {
            return this.dataBase.Events;
        }

        /// <summary>
        /// Find Event items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of Events </returns>
        public IEnumerable<Event> Find(Func<Event, bool> predicate)
        {
            return this.dataBase.Events.Where(predicate).ToList();
        }

        /// <summary>
        /// Update Event item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(Event item)
        {
            var result = this.dataBase.Events.SingleOrDefault(b => b.EventId == item.EventId);
            if (result != null)
            {
                result.Name = item.Name;
                result.Description = item.Description;
                result.EventPhoto = item.EventPhoto;
                result.EventDate = item.EventDate;
                result.Duration = item.Duration;
                result.NumberOfParticipant = item.NumberOfParticipant;
            }

            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Comment item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            Event someEvent = this.dataBase.Events.Find(id);
            if (someEvent != null)
            {
                this.dataBase.Events.Remove(someEvent);
                this.dataBase.SaveChanges();
            }
        }
    }

    
}
