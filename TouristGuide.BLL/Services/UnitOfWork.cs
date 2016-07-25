using System;
using TouristGuide.BLL.Interfaces;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Identity;
using TouristGuide.INF.Models;

namespace TouristGuide.BLL.Services
{
    /// <summary>
    /// Database controller that has all repositories to work with data base
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Reference to business logic for Guest
        /// </summary>
        private IGuestBL guestBL;

        /// <summary>
        /// Reference to business logic for User
        /// </summary>
        private IUserBL userBL;

        /// <summary>
        /// Reference to business logic for Moderator
        /// </summary>
        private IModeratorBL moderatorBL;

        /// <summary>
        /// Reference to business logic for Admin
        /// </summary>
        private IAdminBL adminBL;

        /// <summary>
        /// Reference to business logic for SuperAdmin
        /// </summary>
        private ISuperAdminBL superAdminBL;

        /// <summary>
        /// Initializes a new instance of the UnitOfWork class.
        /// </summary>
        /// <param name="dataBaseManager">Data base manager for tables</param>
        public UnitOfWork(IGuestBL guestBL, IUserBL userBL, IModeratorBL moderatorBL, IAdminBL adminBL, ISuperAdminBL superAdminBL)
        {
            this.guestBL = guestBL;
            this.userBL = userBL;
            this.moderatorBL = moderatorBL;
            this.adminBL = adminBL;
            this.superAdminBL = superAdminBL;
        }

        /// <summary>
        /// Gets the business logic for Guest
        /// </summary>
        public IGuestBL GuestBL
        {
            get { return this.guestBL; }
        }

        /// <summary>
        /// Gets the business logic for User
        /// </summary>
        public IUserBL UserBL
        {
            get { return this.userBL; }
        }

        /// <summary>
        /// Gets the business logic for Moderator
        /// </summary>
        public IModeratorBL ModeratorBL 
        {
            get { return this.moderatorBL; }
        }

        /// <summary>
        /// Gets the business logic for Admin
        /// </summary>
        public IAdminBL AdminBL 
        {
            get { return this.adminBL; }
        }

        /// <summary>
        /// Gets the business logic for SuperAdmin
        /// </summary>
        public ISuperAdminBL SuperAdminBL
        {
            get { return this.superAdminBL; }
        }
    }
}
