using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Models;

namespace TouristGuide.DAL.Repositories
{
    /// <summary>
    /// Repository of reserve
    /// </summary>
    public class ReservationRepository : IRepository<Reservation>
    {
        /// <summary>
        /// Reference to database
        /// </summary>
        private ApplicationContext dataBase;

        /// <summary>
        /// Initializes a new instance of the ReservationRepository class.
        /// </summary>
        /// <param name="db">Reference to data base</param>
        public ReservationRepository(ApplicationContext db) 
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Create new reserve item
        /// </summary>
        /// <param name="item">item to create</param>
        public void Create(Reservation item)
        {
            this.dataBase.Reservations.Add(item);
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Get Reservation item by id
        /// </summary>
        /// <param name="id">Id of Rate item</param>
        /// <returns>Rate item</returns>
        public Reservation Reservation(object id)
        {
            return this.dataBase.Reservations.Find(id);
        }

        /// <summary>
        /// Get Reservation item by id
        /// </summary>
        /// <param name="id">Id of Reservation item</param>
        /// <returns>Reservation item</returns>
        public Reservation Get(object id)
        {
            return this.dataBase.Reservations.Find(id);
        }

        /// <summary>
        /// Get collection of all Reservations items 
        /// </summary>
        /// <returns>Collection of Reservations</returns>
        public IEnumerable<Reservation> GetAll()
        {
            return this.dataBase.Reservations;
        }

        /// <summary>
        /// Find Reservation items by predicate
        /// </summary>
        /// <param name="predicate">Predicate to search</param>
        /// <returns> Collection of Reservation </returns>
        public IEnumerable<Reservation> Find(Func<Reservation, bool> predicate)
        {
            return this.dataBase.Reservations.Where(predicate).ToList();
        }

        /// <summary>
        /// Update Reservation item
        /// </summary>
        /// <param name="item">Item to update</param>
        public void Update(Reservation item)
        {
            var result = this.dataBase.Reservations.SingleOrDefault(b => b.ReservationId == item.ReservationId);
            if (result != null)
            {
                result.NumberOfPersons = item.NumberOfPersons;
                result.ReservationDate = item.ReservationDate;
            }    
        
            this.dataBase.SaveChanges();
        }

        /// <summary>
        /// Delete Reservation item by id
        /// </summary>
        /// <param name="id">Id of item to delete</param>
        public void Delete(object id)
        {
            Reservation rate = this.dataBase.Reservations.Find(id);
            if (rate != null)
            {
                this.dataBase.Reservations.Remove(rate);
                this.dataBase.SaveChanges();
            }
        }
    }
}
