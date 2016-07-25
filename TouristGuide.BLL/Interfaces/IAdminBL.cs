using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.INF.Models;

namespace TouristGuide.BLL.Interfaces
{
    /// <summary>
    /// Administrator interface
    /// </summary>
    public interface IAdminBL
    {
        /// <summary>
        /// Gets all moderators for profile
        /// </summary>
        /// <param name="profileId">Profile id</param>
        /// <returns>Collection of application users</returns>
        ICollection<ApplicationUser> GetAllModerators(int profileId);

        /// <summary>
        /// Add new moderator to profile
        /// </summary>
        /// <param name="user">New moderator</param>
        /// <param name="profileId">Profile id</param>
        /// <returns>Operation Details</returns>
        OperationDetails AddModerator(UserRegisterModel user, int profileId);

        /// <summary>
        /// Set moderator to profile
        /// </summary>
        /// <param name="email">Moderator email</param>
        /// <param name="profileId">Profile id</param>
        /// <returns>Operation Details</returns>
        OperationDetails SetModerator(string email, int profileId);

        /// <summary>
        /// Delete profile moderator
        /// </summary>
        /// <param name="moderId">Moderator id</param>
        /// <returns>Operation Details</returns>
        OperationDetails DeleteModerator(string moderId);

        /// <summary>
        /// Add new restaurants
        /// </summary>
        /// <param name="profile">Profile input</param>
        /// <returns>Operation Details</returns>
        OperationDetails AddRestaurant(ProfileAddModel profile);

        /// <summary>
        /// Edit profile
        /// </summary>
        /// <param name="profile">profile items</param>
        /// <returns>Operation Details</returns>
        OperationDetails EditProfile(ProfileEditModel profile);

        /// <summary>
        /// gets all profiles from this admin
        /// </summary>
        /// <param name="userId">id of admin</param>
        /// <returns></returns>
        ICollection<Profile> GetAllOwnProfiles(string userId);

        /// <summary>
        /// gets all moders for all prodiles from this admin
        /// </summary>
        /// <param name="userId">id of admin</param>
        /// <returns>ICollection<ApplicationUser></returns>
        ICollection<ApplicationUser> GetAllModersToAllProfiles(string userId);

        /// <summary>
        /// Delete Profile
        /// </summary>
        /// <param name="id">profile id</param>
        /// <returns>Operation Details</returns>
        OperationDetails DeleteProfile(int id);
    }
}
