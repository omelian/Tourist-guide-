using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using TouristGuide.BLL.Interfaces;
using TouristGuide.BLL.Services;
using TouristGuide.UI.Models;
using TouristGuide.INF.Models;
using TouristGuide.INF.Enums;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using NLog;
namespace TouristGuide.UI.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    /// <summary>
    /// Controller for managing users profiles and requests data
    /// </summary>
    public class SuperAdminController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        /// <summary>
        /// logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Instantiate the class that holds the super admin logic via dependency injection
        /// </summary>
        /// <param name="unitOfWork">logic interface</param>
        public SuperAdminController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get list of profiles
        /// </summary>
        /// <returns>Profile view model list</returns>
        public ActionResult ProfilesTable()
        {
            ICollection<ProfileViewModel> profiles = new List<ProfileViewModel>();
            var lprofiles = this.unitOfWork.SuperAdminBL.GetAllProfiles();
            foreach (var item in lprofiles)
            {
                profiles.Add(new ProfileViewModel
                {
                    Name = item.Name,
                    ProfileId = item.ProfileId,
                    IsBanned = (item.IsBanned == null) ? false : true,
                    BannedReason = (item.IsBanned == null) ? String.Empty : item.IsBanned.Reason
                });
            }
            return View(profiles);
        }

        /// <summary>
        /// Get list of users
        /// </summary>
        /// <returns>User view model list</returns>
        public ActionResult UsersTable()
        {
            string id = User.Identity.GetUserId();
            ICollection<UserViewModel> users = new List<UserViewModel>();
            var lusers = this.unitOfWork.SuperAdminBL.GetAllUsers();
            foreach (var item in lusers)
            {
                if(item.Id == id)
                {
                    continue;
                }
                if (item.IsBanned == null)
                {
                    users.Add(new UserViewModel
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserId = item.Id,
                        IsDeleted = item.IsDeleted,
                        IsBanned = false
                    });
                }
                else
                {
                    users.Add(new UserViewModel
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        UserId = item.Id,
                        IsDeleted = item.IsDeleted,
                        IsBanned = (item.IsBanned.IsBanned == false) ? false : true,
                        BannedReason = item.IsBanned.Reason
                    });
                }
            }
            return View(users);
        }

        public ActionResult RequestsTable()
        {
            ICollection<AdminRequestsViewModel> requests = new List<AdminRequestsViewModel>();
            var lrequests = this.unitOfWork.SuperAdminBL.GetAllAdminsWithRequest();
            foreach(var item in lrequests)
            {
                if (
                    (item.Company.RequestState == RequestStateEnum.InProcess.ToString()) ||
                    (String.IsNullOrEmpty(item.Company.RequestState))
                  )
                {
                    requests.Add(new AdminRequestsViewModel
                    {
                        AdminId = item.Id,
                        AdminName = item.UserName,
                        CompanyName = item.Company.Name,
                        RequestState = RequestStateEnum.InProcess.ToString(),
                        RequestDescription = item.Company.Description
                    });
                }
                else
                {
                    requests.Add(new AdminRequestsViewModel
                    {
                        AdminId = item.Id,
                        AdminName = item.UserName,
                        CompanyName = item.Company.Name,
                        RequestState =(item.Company.RequestState == RequestStateEnum.Approved.ToString())?
                        RequestStateEnum.Approved.ToString() :
                        RequestStateEnum.Rejected.ToString(),
                        RequestDescription = item.Company.Description
                    });
                }
            }
            return View(requests);
        }

        /// <summary>
        /// Ban profile by id value pointing ban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="banReason">Ban reason</param>
        /// <returns>Action result</returns>
        public ActionResult BanProfileById(int? Id, string banReason)
        {
            logger.Info("Profile [id = {0} : reason = {1}] is bannned.", Id, banReason);
            this.unitOfWork.SuperAdminBL.BanProfileById(Id, banReason);
            return RedirectToAction("ProfilesTable");
        }

        /// <summary>
        /// Unban profile by id value pointing unban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="unBanReason">Ban reason</param>
        /// <returns>Action result</returns>
        public ActionResult UnbanProfileById(int? Id, string unbanReason)
        {
            logger.Info("Profile [id = {0} : reason = {1}] is unbannned.", Id, unbanReason);
            this.unitOfWork.SuperAdminBL.UnbanProfileById(Id, unbanReason);
            return RedirectToAction("ProfilesTable");
        }

        /// <summary>
        /// Ban profile by id value pointing ban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="banReason">Ban reason</param>
        /// <returns>Action result</returns>
        public ActionResult BanUserById(string Id, string banReason)
        {
            logger.Info("User [id = {0} : reason = {1}] is bannned.", Id, banReason);
            this.unitOfWork.SuperAdminBL.BanUserById(Id, banReason);
            return RedirectToAction("UsersTable");
        }

        /// <summary>
        /// Unban profile by id value pointing unban reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="unBanReason">Ban reason</param>
        /// <returns>Action result</returns>
        
        public ActionResult UnbanUserById(string Id, string unbanReason)
        {
            logger.Info("User [id = {0} : reason = {1}] is unbannned.", Id, unbanReason);
            this.unitOfWork.SuperAdminBL.UnbanUserById(Id, unbanReason);
            return RedirectToAction("UsersTable");
        }

        /// <summary>
        /// Delete profile by id value pointing delete reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="deleteReason">Delete reason</param>
        /// <returns>Action result</returns>
        public ActionResult DeleteUserById(string Id, string deleteReason)
        {
            logger.Info("User [id = {0} : reason = {1}] is deleted.", Id, deleteReason);
            this.unitOfWork.SuperAdminBL.DeleteUserById(Id, deleteReason);
            return RedirectToAction("UsersTable");
        }


        /// <summary>
        /// Undelete(recreate profile after he has been deleted) profile by id value
        /// pointing delete reason
        /// </summary>
        /// <param name="profileId">Profile id value</param>
        /// <param name="undeleteProfile">Undelete reason</param>
        /// <returns>Action result</returns>
        public ActionResult UndeleteUserById(string Id, string undeleteReason)
        {
            logger.Info("User [id = {0} : reason = {1}] is undeleted.", Id, undeleteReason);
            this.unitOfWork.SuperAdminBL.UndeleteUserById(Id, undeleteReason);
            return RedirectToAction("UsersTable");
        }

        /// <summary>
        /// Approve request from admin to create company
        /// </summary>
        /// <param name="Id">Admin id value</param>
        /// <returns>Action result</returns>
        public ActionResult ApproveRequestFromAdmin(string Id)
        {
            logger.Info("Request [id = {0}] is approved.", Id);
            this.unitOfWork.SuperAdminBL.ApproveRequestFromAdmin(Id);
            return RedirectToAction("RequestsTable");
        }

        /// <summary>
        /// Reject request from admin to create company
        /// </summary>
        /// <param name="Id">Admin id value</param>
        /// <returns>Action result</returns>
        public ActionResult RejectRequestFromAdmin(string Id, string rejectReason)
        {
            logger.Info("Request [id = {0} : reason = {1}] is rejected.", Id, rejectReason);
            this.unitOfWork.SuperAdminBL.RejectRequestFromAdmin(Id, rejectReason);
            return RedirectToAction("ReqiestsTable");
        }
    }
}