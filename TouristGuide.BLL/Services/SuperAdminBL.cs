using System;
using System.Collections.Generic;
using Microsoft.AspNet.Identity;
using System.Linq;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.Enums;
using TouristGuide.INF.Models;
using TouristGuide.BLL.Infrastructure;
namespace TouristGuide.BLL.Services
{
    /// <summary>
    /// Class that implements super admin business logic
    /// </summary>
    public class SuperAdminBL : ISuperAdminBL
    {
        /// <summary>
        /// reference to data base controller
        /// </summary>
        private IDataBaseManager dataBase;

        /// <summary>
        /// Initializes a new instance of the SuperAdminBL class
        /// </summary>
        public SuperAdminBL(IDataBaseManager db)
        {
            this.dataBase = db;
        }

        /// <summary>
        /// Gets collection of users
        /// </summary>
        /// <returns>Collection of users</returns>
        public ICollection<ApplicationUser> GetAllUsers()
        {
            lock (this.dataBase)
            {
                return this.dataBase.UserManager.Users.ToList();
            }
        }

        /// <summary>
        /// Gets collection of profiles
        /// </summary>
        /// <returns>Collection of profiles</returns>
        public ICollection<Profile> GetAllProfiles()
        {
            lock (this.dataBase)
            {
                return this.dataBase.ProfileManager.GetAll().ToList();
            }
        }

        /// <summary>
        /// Get all requests from admin to have a company
        /// </summary>
        /// <returns>Collection of users with correct requests</returns>
        public ICollection<ApplicationUser> GetAllAdminsWithRequest()
        {
            lock (this.dataBase)
            {
                ICollection<ApplicationUser> adminsWithRequest = this.dataBase.UserManager.Users.Where(p => p.Company != null).ToList();
                return adminsWithRequest;
            }
        }

        /// <summary>
        /// Approve request from admin to have a company
        /// </summary>
        /// <param name="adminId">Admin id value</param>
        /// <returns>Operation details</returns>
        public OperationDetails ApproveRequestFromAdmin(string adminId)
        {
            lock (this.dataBase)
            {
                if (adminId == null)
                {
                    return new OperationDetails(false, "Incorrect admin id value", "Approve request from admin");
                }
                else
                {
                    var company = (Company)this.dataBase.CompanyManager.Get(adminId);
                    company.RequestState = RequestStateEnum.Approved.ToString();
                    this.dataBase.CompanyManager.Update(company);
                }
                return new OperationDetails(true, "Request has been successfully approved", "Approve request form admin");
            }
        }

        /// <summary>
        /// Reject request from admin to have a company
        /// </summary>
        /// <param name="adminId">Admin id value</param>
        /// <param name="rejectDescription">Reject description</param>
        /// <returns>Opearation details</returns>
        public OperationDetails RejectRequestFromAdmin(string adminId, string rejectDescription)
        {
            lock (this.dataBase)
            {
                if (adminId == null)
                {
                    return new OperationDetails(false, "Incorrect admin id value", "Reject request from admin");
                }
                else
                {
                    var company = (Company)this.dataBase.CompanyManager.Get(adminId);
                    company.RequestState = RequestStateEnum.Rejected.ToString();
                    company.Description = rejectDescription;
                    this.dataBase.CompanyManager.Update(company);
                }
                return new OperationDetails(true, "Request has been successfully rejected", "Reject request from admin");
            }
        }

        /// <summary>
        /// Ban profile by id value pointing ban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="banReason">Ban reason</param>
        /// <returns>Operation details</returns>
        public OperationDetails BanProfileById(int? profileId, string banReason)
        {
            lock (this.dataBase)
            {
                if (profileId == null)
                {
                    return new OperationDetails(false, "Incorrect profile id value", "Ban profile by id");
                }
                else
                {
                    try
                    {
                        var profile = this.dataBase.ProfileManager.Get((int)profileId);

                        BannedProfile newBannedProfile = new BannedProfile
                        {
                            BannedProfileDateTime = DateTime.Now,
                            Profile = profile,
                            Reason = banReason
                        };
                        this.dataBase.BannedProfileManager.Create(newBannedProfile);
                    }
                    catch (Exception)
                    {
                        return new OperationDetails(false, "Incorrect profile id value", "Unban profile by id");
                    }
                    return new OperationDetails(true, "Profile has been successfully banned", "Ban profile by id");
                }
            }
        }

        /// <summary>
        /// Unban profile by id value pointing unban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="unBanReason">Ban reason</param>
        /// <returns>Operation details</returns>
        public OperationDetails UnbanProfileById(int? profileId, string unbanReason)
        {
            lock (this.dataBase)
            {
                if (profileId == null)
                {
                    return new OperationDetails(false, "Incorrect profile id value", "Unban profile by id");
                }
                else
                {
                    try
                    {
                        var profile = this.dataBase.ProfileManager.Get((int)profileId);
                        profile.IsBanned = null;
                        this.dataBase.ProfileManager.Update(profile);
                    }
                    catch (Exception)
                    {
                        return new OperationDetails(false, "Incorrect profile id value", "Unban profile by id");
                    }
                    return new OperationDetails(true, "Profile has been successfully unbanned", "Unban profile by id");
                }
            }
        }

        /// <summary>
        /// Ban profile by id value pointing ban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="banReason">Ban reason</param>
        /// <returns>Operation details</returns>
        public OperationDetails BanUserById(string userId, string banReason)
        {
            lock (this.dataBase)
            {
                if (String.IsNullOrEmpty(userId))
                {
                    return new OperationDetails(false, "Incorrect user id value", "Ban user by id");
                }
                else
                {
                    try
                    {

                        ApplicationUser user = this.dataBase.UserManager.FindById(userId);
                        if (user.IsBanned == null)
                        {
                            BannedUser newBannedUser = new BannedUser
                            {
                                UserDateTime = DateTime.Now,
                                IsBanned = true,
                                User = user,
                                Reason = banReason
                            };
                            this.dataBase.BannedUserManager.Create(newBannedUser);
                        }
                        else
                        {
                            user.IsBanned.IsBanned = true;
                            user.IsBanned.Reason = banReason;
                            this.dataBase.UserManager.Update(user);
                        }


                    }

                    catch (Exception)
                    {
                        return new OperationDetails(false, "Incorrect user id value", "Ban user by id");
                    }
                    return new OperationDetails(true, "User has been successfully banned", "Ban user by id");
                }
            }
        }

        /// <summary>
        /// Unban profile by id value pointing unban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="unBanReason">Ban reason</param>
        /// <returns>Operation details</returns>
        public OperationDetails UnbanUserById(string userId, string unbanReason)
        {
            lock (this.dataBase)
            {
                if (String.IsNullOrEmpty(userId))
                {
                    return new OperationDetails(false, "Incorrect user id value", "Unban user by id");
                }
                else
                {
                    try
                    {
                        ApplicationUser user = this.dataBase.UserManager.FindById(userId);
                        if (user.IsBanned == null)
                        {
                            BannedUser newUnbannedUser = new BannedUser
                            {
                                UserDateTime = DateTime.Now,
                                IsBanned = false,
                                User = user,
                                Reason = unbanReason
                            };
                            this.dataBase.BannedUserManager.Create(newUnbannedUser);
                        }
                        else
                        {
                            user.IsBanned.IsBanned = false;
                            user.IsBanned.Reason = unbanReason;
                            this.dataBase.UserManager.Update(user);
                        }
                    }
                    catch (Exception)
                    {
                        return new OperationDetails(false, "Incorrect user id value", "Ban user by id");
                    }
                    return new OperationDetails(true, "User has been successfully unbanned", "Unban user by id");
                }
            }
        }
        /// <summary>
        /// Delete profile by id value pointing delete reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="deleteReason">Delete reason</param>
        /// <returns>Operation details</returns>
        public OperationDetails DeleteUserById(string userId, string deleteReason)
        {
            lock (this.dataBase)
            {
                if (String.IsNullOrEmpty(userId))
                {
                    return new OperationDetails(false, "Incorrect user id value", "Delete user by id");
                }
                else
                {
                    try
                    {
                        var user = this.dataBase.UserManager.FindById(userId);
                        user.IsDeleted = true;
                        user.DeletedReason = deleteReason;
                        this.dataBase.UserManager.Update(user);
                    }
                    catch (Exception)
                    {
                        return new OperationDetails(false, "Incorrect user id value", "Delete user by id");
                    }
                    return new OperationDetails(true, "User has been successfully deleted", "Delete user by id");
                }
            }
        }

        /// <summary>
        /// Undelete(recreate profile after he has been deleted) profile by id value
        /// pointing delete reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="undeleteProfile">Undelete reason</param>
        /// <returns>Operation details</returns>
        public OperationDetails UndeleteUserById(string userId, string undeletedReason)
        {
            lock (this.dataBase)
            {
                if (String.IsNullOrEmpty(userId))
                {
                    return new OperationDetails(false, "Incorrect user id value", "Undelete user by id");
                }
                else
                {
                    try
                    {
                        var user = this.dataBase.UserManager.FindById(userId);
                        user.IsDeleted = false;
                        user.DeletedReason = undeletedReason;
                        this.dataBase.UserManager.Update(user);
                    }
                    catch (Exception)
                    {
                        return new OperationDetails(false, "Incorrect user id value", "Undelete user by id");
                    }
                    return new OperationDetails(true, "User has been successfully undeleted", "Undelete user by id");
                }

            }
        }
    }
}