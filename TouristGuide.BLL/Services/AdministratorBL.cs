using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using Microsoft.AspNet.Identity.Owin;
using TouristGuide.BLL.DBContext;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.BLL.Interfaces;
using TouristGuide.DAL.Interfaces;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.INF.Enums;
using TouristGuide.INF.Models;
using System.Security.Claims;

namespace TouristGuide.BLL.Services
{
    /// <summary>
    /// Administrator functionality
    /// </summary>
    public class AdministratorBL : IAdminBL
    {
        /// <summary>
        /// reference to data base controller
        /// </summary>
        private IDataBaseManager dataBase;

        /// <summary>
        /// Initializes a new instance of the AdministratorBL class.
        /// </summary>
        /// <param name="db">Data base</param>
        public AdministratorBL(IDataBaseManager db)
        {            
            this.dataBase = db;
        }

        /// <summary>
        /// Send email to user
        /// </summary>
        /// <param name="smtpServer">smtpServer</param>
        /// <param name="from">from</param>
        /// <param name="password">password</param>
        /// <param name="mailto">mailto</param>
        /// <param name="caption">caption</param>
        /// <param name="message">message</param>
        /// <param name="attachFile">attachFile</param>
        public static void SendMail(string smtpServer, string from, string password, string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;
                if (!string.IsNullOrEmpty(attachFile))
                {
                    mail.Attachments.Add(new Attachment(attachFile));
                }

                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new System.Net.NetworkCredential(from.Split('@')[0], password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);
                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception("Mail.Send: " + e.Message);
            }
        }

        /// <summary>
        /// Get all moderators for profile
        /// </summary>
        /// <param name="profileId">Profile id</param>
        /// <returns>Collection of moderators</returns>
        public ICollection<ApplicationUser> GetAllModerators(int profileId)
        {
            lock (this.dataBase)
            {
                var profile = this.dataBase.ProfileManager.Get(profileId);
                if (profile != null)
                {
                var moderators = profile.Moders.Where(t => t.IsDeleted == false && ((t.IsBanned != null && t.IsBanned.IsBanned == false) || t.IsBanned == null)).ToList();
                return moderators; 
            }           
                else
                {
                    return null;
        }
            }           
        }

        /// <summary>
        /// Add new moderator to profile
        /// </summary>
        /// <param name="user">New moderator</param>
        /// <param name="profileId">Profile id</param>
        /// <returns>Operation Details</returns>
        public OperationDetails AddModerator(UserRegisterModel user, int profileId)
        {
            lock (this.dataBase)
            {
                ApplicationUser applicationUser = this.dataBase.UserManager.FindByEmail(user.Email);
                if (applicationUser == null)
                {
                    string role = UserRoleEnum.Moderator.ToString();
                    var profile = this.dataBase.ProfileManager.Get(profileId);
                    applicationUser = new ApplicationUser
                    {
                        Email = user.Email,
                        UserName = user.Email,
                        FirstName = "YourFirstName",
                        LastName = "YourLastName",
                        ManageProfile = profile
                    };
                    this.dataBase.UserManager.Create(applicationUser, user.Password);
                    this.dataBase.UserManager.AddToRole(applicationUser.Id, role);
                    profile.Moders.Add(applicationUser);
                    this.dataBase.ProfileManager.Update(profile);
                    string textForEmail = string.Empty;
                    textForEmail += @"Hi! You are registered as moderator at http://travellertourist.azurewebsites.net/"
                        + System.Environment.NewLine + "Your login: " + user.Email + System.Environment.NewLine + "Password: " + user.Password;
                    try
                    {
                        SendMail("smtp.gmail.com", "traveleradm1@gmail.com", "Qwer27512*", user.Email, "Register in traveler", textForEmail, null);
                    }
                    catch (Exception ex)
                    {
                        return new OperationDetails(false, ex.ToString(), string.Empty);
                    }     
              
                    return new OperationDetails(true, "Moderator Added", string.Empty);
                }
                else
                {
                    return new OperationDetails(false, "This email is alredy used.", "Email");
                }
            }
        }
        
        /// <summary>
        /// Set moderator to profile
        /// </summary>
        /// <param name="email">Moderator email</param>
        /// <param name="profileId">Profile id</param>
        /// <returns>Operation Details</returns>
        public OperationDetails SetModerator(string email, int profileId)
        {
            lock(this.dataBase)
            {
                var moderator = this.dataBase.UserManager.FindByEmail(email);
                if(moderator == null)
                {
                    return new OperationDetails(false, "Can not find moderator", "Email");
                }
                else
                {
                    var list = this.dataBase.UserManager.GetRoles(moderator.Id);
                    string roleName = list.First();
                    if( roleName == UserRoleEnum.Moderator.ToString() && moderator.IsDeleted == true && (moderator.IsBanned == null || moderator.IsBanned.IsBanned == false))
                    {
                        var profile = this.dataBase.ProfileManager.Get(profileId);
                        moderator.IsDeleted = false;
                        moderator.ManageProfile = profile;
                        this.dataBase.UserManager.Update(moderator);
                        profile.Moders.Add(moderator);
                        this.dataBase.ProfileManager.Update(profile);
                        return new OperationDetails(true, string.Empty, string.Empty);
                    }
                    else
                    {
                        return new OperationDetails(false, "Can not assign user with this e-mail", string.Empty);
                    }
                }
            }                     
        }

        /// <summary>
        /// Delete profile moderator
        /// </summary>
        /// <param name="moderId">Moderator Id</param>
        /// <returns>Operation Details</returns>
        public OperationDetails DeleteModerator(string moderId)
        {
            lock (this.dataBase)
            {
                ApplicationUser applicationUser = this.dataBase.UserManager.FindById(moderId);
                if (applicationUser != null)
                {
                    applicationUser.IsDeleted = true;
                    this.dataBase.UserManager.Update(applicationUser);
                    var profile = this.dataBase.ProfileManager.Get(applicationUser.ManageProfile.ProfileId);
                    profile.Moders.Remove(applicationUser);
                    this.dataBase.ProfileManager.Update(profile);
                    return new OperationDetails(true, string.Empty, string.Empty);
                }
                else
                {
                    return new OperationDetails(false, "Moderator not found", string.Empty);
                }
            }

            return new OperationDetails(true, string.Empty, string.Empty);
        }

        /// <summary>
        /// Add new restaurant
        /// </summary>
        /// <param name="profile">Profile item</param>
        /// <returns>Operation Details</returns>
        public OperationDetails AddRestaurant(ProfileAddModel profile)
        {
            lock (this.dataBase)
            {
                var prof = this.dataBase.ProfileManager.GetAll().Where(t => t.Name == profile.Name);
                if (prof.Count() == 0)
                {
                    try
                    {
                        Profile newProfile = new Profile { Name = profile.Name };
                        var company = this.dataBase.CompanyManager.Get(profile.CompanyId);
                        newProfile.Company = company;                   
                        Location location = new Location { XCoord = profile.XCoord, YCoord = profile.YCoord };
                        Address address = new Address { Country = profile.Country, City = profile.City, Street = profile.Street, Number = profile.Number, Location = location };
                        newProfile.Address = address;
                        var type = this.dataBase.ProfileTypeManager.Find(t => t.Name == profile.ProfileType.ToString()).FirstOrDefault();
                        newProfile.Type = type;         
                        this.dataBase.ProfileManager.Create(newProfile);
                        return new OperationDetails(true, string.Empty, string.Empty);
                    }
                    catch (Exception exc)
                    {
                        return new OperationDetails(false, exc.ToString(), string.Empty);
                    }
                }
                else
                {
                    return new OperationDetails(false, "Profile name is alredy exist", string.Empty);
                }
            }            
        }

        /// <summary>
        /// Edit profile
        /// </summary>
        /// <param name="profile">Profile item</param>
        /// <returns>Operation Details</returns>       
        public OperationDetails EditProfile(ProfileEditModel profile)
        {
            lock (this.dataBase)
            {
                Profile localProfile = this.dataBase.ProfileManager.Get(profile.Id);
                localProfile.Name = profile.Name;
                localProfile.Address.Country = profile.Country;
                localProfile.Address.City = profile.City;
                localProfile.Address.Number = profile.Number;
                localProfile.Address.Street = profile.Street;
                localProfile.Address.Location.XCoord = profile.XCoord;
                localProfile.Address.Location.YCoord = profile.YCoord;
                this.dataBase.ProfileManager.Update(localProfile);
                return new OperationDetails(true, "Profile changed", string.Empty);
            }        
        }



        /// <summary>
        /// gets all profiles from this admin
        /// </summary>
        /// <param name="userId">id of admin</param>
        /// <returns>ICollection<Profile></returns>
        public ICollection<Profile> GetAllOwnProfiles(string userId)
        {
            lock (this.dataBase)
            {
                string roleName = this.dataBase.UserManager.GetRoles(userId).First();
                if (roleName == UserRoleEnum.Admin.ToString())
                {
                    return this.dataBase.UserManager.FindById(userId).Company.Profiles.Where(x => x.IsDeleted == false).ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// gets all moders for all prodiles from this admin
        /// </summary>
        /// <param name="userId">id of admin</param>
        /// <returns>ICollection<ApplicationUser></returns>
        public ICollection<ApplicationUser> GetAllModersToAllProfiles(string userId)
        {
            lock (this.dataBase)
            {
                string roleName = this.dataBase.UserManager.GetRoles(userId).First();
                if (roleName == UserRoleEnum.Admin.ToString())
                {
                    List<ApplicationUser> moders = new List<ApplicationUser>();
                    List<Profile> allprofiles = new List<Profile>();
                    foreach (Profile x in allprofiles)
                    {
                        moders.AddRange(x.Moders);
                    }
                    return moders.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Delete profile
        /// </summary>
        /// <param name="id">Profile id</param>
        /// <returns>OperationDetails</returns>
        public OperationDetails DeleteProfile(int id)
        {
            lock (this.dataBase)
            {
                var profile = this.dataBase.ProfileManager.Get(id);
                if (profile != null)
                {
                    profile.IsDeleted = true;
                    this.dataBase.ProfileManager.Update(profile);
                    return new OperationDetails(true, "Profile deleted", string.Empty);
                }
                else
                {
                    return new OperationDetails(false, "Profile not found", string.Empty);
                }               
            }
        }

    }
}
