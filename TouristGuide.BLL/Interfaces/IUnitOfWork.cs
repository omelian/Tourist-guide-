using System;
using System.Threading.Tasks;
using TouristGuide.INF.EntityFramework;
using TouristGuide.INF.Identity;
using TouristGuide.INF.Models;

namespace TouristGuide.BLL.Interfaces
{
    /// <summary>
    /// Defines properties and methods for work with repositories
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the business logic for Guest
        /// </summary>
        IGuestBL GuestBL { get; }

        /// <summary>
        /// Gets the business logic for User
        /// </summary>
        IUserBL UserBL { get; }

        /// <summary>
        /// Gets the business logic for Moderator
        /// </summary>
        IModeratorBL ModeratorBL { get; }

        /// <summary>
        /// Gets the business logic for Admin
        /// </summary>
        IAdminBL AdminBL { get; }

        /// <summary>
        /// Gets the business logic for SuperAdmin
        /// </summary>
        ISuperAdminBL SuperAdminBL { get; }
    }
}
