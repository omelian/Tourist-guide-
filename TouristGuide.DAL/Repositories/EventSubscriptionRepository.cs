using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Models;

namespace TouristGuide.DAL.Repositories
{
    /// <summary>
    /// EventSubscription Repository
    /// </summary>
    public class EventSubscriptionRepository : IRepository<EventSubscription>
    {

        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the ArticleRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public EventSubscriptionRepository(ApplicationContext db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new EventSubscription item
        /// </summary>
        /// <param name="item">Item to create</param>
        public void Create(EventSubscription item)
        {
            this.dataBase.EventSubscriptions.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get EventSubscription item by id
        /// </summary>
        /// <param name="id">Id of EventSubscription item</param>
        /// <returns>Event item</returns>
        public EventSubscription Get(object id)
        {
            return this.dataBase.EventSubscriptions.Find(id);
        }

        /// <summary>
        /// Get collection of all Event items 
        /// </summary>
        /// <returns>Collection of EventSubscriptions</returns>
        public IEnumerable<EventSubscription> GetAll()
        {
            return this.dataBase.EventSubscriptions;
        }

        /// <summary>
        /// Find EventSubscriptiont items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of EventSubscriptions </returns>
        public IEnumerable<EventSubscription> Find(Func<EventSubscription, bool> predicate)
        {
            return this.dataBase.EventSubscriptions.Where(predicate).ToList();
        }

        /// <summary>
        /// Update EventSubscription item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(EventSubscription item)
        {
            var result = this.dataBase.EventSubscriptions.SingleOrDefault(b => b.SubscriptionId == item.SubscriptionId);
            if (result != null)
            {
                
                result.NumberOfPersons = item.NumberOfPersons;
            }

            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Comment item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            EventSubscription someEvent = this.dataBase.EventSubscriptions.Find(id);
            if (someEvent != null)
            {
                this.dataBase.EventSubscriptions.Remove(someEvent);
                this.dataBase.SaveChanges();
            }
        }
    }
}
