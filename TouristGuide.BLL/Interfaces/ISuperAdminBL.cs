using System.Collections.Generic;
using System.Threading.Tasks;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.INF.Models;

namespace TouristGuide.BLL.Interfaces
{
    /// <summary>
    /// Defines methods for SuperAdmin business logic
    /// </summary>
    public interface ISuperAdminBL
    {
        /// <summary>
        /// Gets collection of users
        /// </summary>
        /// <returns>Collection of users</returns>
        ICollection<ApplicationUser> GetAllUsers();

        /// <summary>
        /// Gets collection of profiles
        /// </summary>
        /// <returns>Collection of profiles</returns>
        ICollection<Profile> GetAllProfiles();

        /// <summary>
        /// Get all requests from admin to have a company
        /// </summary>
        /// <returns>Collection of users with correct requests</returns>
        ICollection<ApplicationUser> GetAllAdminsWithRequest();

        /// <summary>
        /// Approve request from admin to have a company
        /// </summary>
        /// <param name="adminId">Admin id value</param>
        /// <returns>Operation details</returns>
        OperationDetails ApproveRequestFromAdmin(string adminId);

        /// <summary>
        /// Reject request from admin to have a company
        /// </summary>
        /// <param name="adminId">Admin id value</param>
        /// <param name="rejectDescription">Reject description</param>
        /// <returns>Opearation details</returns>
        OperationDetails RejectRequestFromAdmin(string adminId, string rejectDescription);

        /// <summary>
        /// Ban profile by id value pointing ban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="banReason">Ban reason</param>
        /// <returns>Operation details</returns>
        OperationDetails BanProfileById(int? profileId, string banReason);

        /// <summary>
        /// Unban profile by id value pointing unban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="unBanReason">Ban reason</param>
        /// <returns>Operation details</returns>
        OperationDetails UnbanProfileById(int? profileId, string unbanReason);

        /// <summary>
        /// Ban user by id value pointing ban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="banReason">Ban reason</param>
        /// <returns>Operation details</returns>
        OperationDetails BanUserById(string userId, string banReason);

        /// <summary>
        /// Unban profile by id value pointing unban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="unBanReason">Ban reason</param>
        /// <returns>Operation details</returns>
        OperationDetails UnbanUserById(string userId, string unbanReason);

        /// <summary>
        /// Delete profile by id value pointing delete reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="deleteReason">Delete reason</param>
        /// <returns>Operation details</returns>
        OperationDetails DeleteUserById(string userId, string deleteReason);

        /// <summary>
        /// Undelete(recreate profile after he has been deleted) profile by id value
        /// pointing delete reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="undeleteProfile">Undelete reason</param>
        /// <returns>Operation details</returns>
        OperationDetails UndeleteUserById(string userId, string deleteReason);

    }
}
