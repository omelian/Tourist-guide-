using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.INF.Enums;
using TouristGuide.INF.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net.Mail;
using Microsoft.AspNet.Identity.Owin;

namespace TouristGuide.BLL.Services
{
    /// <summary>
    /// Class that implements Moderator business logic
    /// </summary>
    public class UserBL : GuestBL, IUserBL
    {
        /// <summary>
        /// reference to data base controller
        /// </summary>
        private IDataBaseManager dataBase;

        /// <summary>
        /// Initializes a new instance of the UserBL class.
        /// </summary>
        public UserBL(IDataBaseManager db)
            : base(db)
        {
            this.dataBase = db;
        }




        /// <summary>
        /// Get information of user
        /// </summary>
        /// <param name="userid">User id</param>
        /// <returns>UserManageModel</returns>
        public UserManageModel GetUserInfo(string userid)
        {
            lock (this.dataBase)
            {
                UserManageModel model = new UserManageModel();
                try
                {
                    ApplicationUser applicationUser = this.dataBase.UserManager.FindById(userid);
                    model.DateBirth = applicationUser.DateBirth;
                    model.UserName = applicationUser.FirstName;
                    model.LastName = applicationUser.LastName;
                    if (applicationUser.Gender == "Male")
                    {
                        model.Gender = GenderEnum.Male;
                    }
                    else
                    {
                        model.Gender = GenderEnum.Female;
                    }
                    if (string.IsNullOrEmpty(applicationUser.Photo))
                    {
                        applicationUser.Photo = (string)@"http://donatered-asset.s3.amazonaws.com/assets/default/default_user-884fcb1a70325256218e78500533affb.jpg";
                        this.dataBase.UserManager.Update(applicationUser);
                    }

                    model.PhotoUrl = applicationUser.Photo;
                }
                catch (Exception)
                { }
                return model;
            }
        }

        /// <summary>
        /// Get favorites profiles of user
        /// </summary>
        /// <param name="userid">User id</param>
        /// <returns>UserManageModel</returns>
        public List<Profile> GetUserFavorites(string userid)
        {
            List<Profile> model = new List<Profile>();
            lock (this.dataBase)
            {
                model = this.dataBase.UserManager.FindById(userid).FavoriteProfiles.ToList();
            }
            return model;
        }

        /// <summary>
        /// Get coments of user
        /// </summary>
        /// <param name="userid">User id</param>
        /// <returns>UserManageModel</returns>
        public List<Comment> GetUserComments(string userid)
        {
            List<Comment> model = new List<Comment>();
            lock (this.dataBase)
            {
                model = this.dataBase.UserManager.FindById(userid).GivenComments.Reverse().ToList();
            }
            return model;
        }

        /// <summary>
        /// Get user's rates
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of user's rates</returns>
        public List<Rate> GetUserRates(string userId)
        {
            lock (this.dataBase)
            {
                return this.dataBase.UserManager.FindById(userId).GivenRates.ToList();
            }
        }

        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="user">User item</param>
        /// <returns>Operation details</returns>
        public OperationDetails Create(UserRegisterModel user)
        {
            lock (this.dataBase)
            {
                ApplicationUser applicationUser = this.dataBase.UserManager.FindByEmail(user.Email);
                if (applicationUser == null)
                {
                    string role = user.Role.ToString();

                    if (user.Role.ToString() == UserRoleEnum.Admin.ToString())
                    {
                        if (this.dataBase.CompanyManager.Find(x => x.Name == user.Company).Count() == 0)
                        {
                            applicationUser = new ApplicationUser
                            {
                                Email = user.Email,
                                UserName = user.Email,
                                FirstName = user.UserName,
                                LastName = user.LastName
                            };
                            applicationUser.Photo = (string)@"http://donatered-asset.s3.amazonaws.com/assets/default/default_user-884fcb1a70325256218e78500533affb.jpg";
                            this.dataBase.UserManager.Create(applicationUser, user.Password);
                            if (this.dataBase.UserManager.FindById(applicationUser.Id) != null)
                            {
                                this.dataBase.UserManager.AddToRole(applicationUser.Id, role);
                            }
                            Company company = new Company();
                            company.Name = user.Company;
                            company.User = applicationUser;
                            this.dataBase.CompanyManager.Create(company);
                        }
                        else
                        {
                            return new OperationDetails(false, "This Company name is alredy used.", "Company");
                        }


                        return new OperationDetails(true, "Registration complete.", string.Empty);
                    }
                    else
                    {
                        applicationUser = new ApplicationUser
                    {
                        Email = user.Email,
                        UserName = user.Email,
                        FirstName = user.UserName,
                        LastName = user.LastName
                    };
                        applicationUser.Photo = (string)@"http://donatered-asset.s3.amazonaws.com/assets/default/default_user-884fcb1a70325256218e78500533affb.jpg";
                        this.dataBase.UserManager.Create(applicationUser, user.Password);
                        if (this.dataBase.UserManager.FindById(applicationUser.Id) != null)
                        {
                            this.dataBase.UserManager.AddToRole(applicationUser.Id, role);
                        }

                        return new OperationDetails(true, "Registration complete.", string.Empty);
                    }
                }
                else
                {
                    return new OperationDetails(false, "This Email is alredy used.", "Email");
                }
            }
        }

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userLoginModel">User to Authenticate</param>
        /// <returns>Claims identity</returns>
        public ClaimsIdentity Authenticate(UserLoginModel userLoginModel)
        {
            lock (this.dataBase)
            {
                string message = String.Empty;
                ClaimsIdentity claim = null;
                ApplicationUser user = null;
                ApplicationUser userTemp = this.dataBase.UserManager.FindByEmail(userLoginModel.Email);
                if (userTemp != null)
                {
                    user = this.dataBase.UserManager.Find(userTemp.UserName, userLoginModel.Password);
                }
                if (user != null)
                {
                    var list = this.dataBase.UserManager.GetRoles(user.Id);
                    string roleName = list.First();
                    claim = this.dataBase.UserManager.CreateIdentity(user, "AppCookie");

                    if (user.IsDeleted == true)
                    {
                        message = "Your accont is deleted";
                    }
                    else if (user.IsBanned != null)
                    {
                        if (user.IsBanned.IsBanned == true)
                        {
                            message = "You are banned " + Environment.NewLine;
                            message += "Reason:" + user.IsBanned.Reason;
                        }
                    }
                    else
                        if (roleName == UserRoleEnum.Admin.ToString())
                        {
                            if (user.Company.RequestState == RequestStateEnum.InProcess.ToString() ||
                         String.IsNullOrEmpty(user.Company.RequestState)
                         )
                            {
                                message = "Sorry, but your request is in process now";
                            }
                            else if (user.Company.RequestState == RequestStateEnum.Rejected.ToString())
                            {
                                message = user.Company.Description;
                            }
                        }
                    claim.AddClaim(new Claim(ClaimValueTypes.String, message));
                    claim.AddClaim(new Claim(ClaimValueTypes.KeyInfo, roleName));

                }
                return claim;
            }
        }

        /// <summary>
        /// Set rate to profile
        /// </summary>
        /// <param name="profile">Profile to set rate</param>
        /// <returns>Operation details</returns>
        public OperationDetails SetRate(ProfileRate rate)
        {
            lock (this.dataBase)
            {
                Rate l2Rate = null;
                var localRateCollection = this.dataBase.RateManager.Find(p => p.Profile.ProfileId == rate.ProfileId);
                List<Rate> rates = localRateCollection.Where(p => p.User != null && p.User.Id == rate.UserId).ToList();
                if (rates.Count != 0)
                {
                    l2Rate = rates.First();
                    l2Rate.Mark = rate.Mark;
                    this.dataBase.RateManager.Update(l2Rate);
                }
                else
                {
                    Profile profile = this.GetProfileById(rate.ProfileId);
                    ApplicationUser user = this.dataBase.UserManager.FindById(rate.UserId);
                    l2Rate = new Rate()
                    {
                        Mark = rate.Mark,
                        Profile = profile,
                        User = user
                    };
                    this.dataBase.RateManager.Create(l2Rate);

                }
                return new OperationDetails(true, "Rate has been successfully setted!", "Set rate");
            }
        }

        /// <summary>
        /// Add comment to profile
        /// </summary>
        /// <param name="comment">Comment to add</param>
        /// <returns>Operation details</returns>
        public OperationDetails AddComment(Comment comment)
        {
            lock (this.dataBase)
            {
                Profile profile = this.GetProfileById(comment.Profile.ProfileId);
                if (profile != null)
                {
                    comment.Profile = profile;
                    ApplicationUser user = this.dataBase.UserManager.FindById(comment.User.Id);
                    if (user != null)
                    {
                        comment.User = user;
                    }
                    else
                    {
                        return new OperationDetails(false, "User doesn`t exists!", "Comments");
                    }

                    this.dataBase.CommentManager.Create(comment);
                    return new OperationDetails(true, "Comment successfully created!", "Comments");
                }
                else
                {
                    return new OperationDetails(false, "Profile doesn`t exists!", "Comments");
                }
            }
        }

        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name="model">UserManageModel</param>
        /// <param name="userid">User id</param>
        /// <returns>Operation details</returns>
        public OperationDetails UpdateUserInfo(UserManageModel model, string userId)
        {
            lock (this.dataBase)
            {
                ApplicationUser applicationUser = this.dataBase.UserManager.FindById(userId);
                applicationUser.Gender = model.Gender.ToString();
                applicationUser.FirstName = model.UserName;
                applicationUser.LastName = model.LastName;
                applicationUser.DateBirth = model.DateBirth;
                this.dataBase.UserManager.Update(applicationUser);
                return new OperationDetails(true, "Saving complete.", string.Empty);
            }
        }


        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name="model">UserManageModel</param>
        /// <param name="userid">User id</param>
        /// <returns>Operation details</returns>
        public OperationDetails UpdateUserPhoto(string photoUrl, string userId)
        {
            lock (this.dataBase)
            {
                ApplicationUser applicationUser = this.dataBase.UserManager.FindById(userId);
                if (!string.IsNullOrEmpty(photoUrl))
                {
                    applicationUser.Photo = photoUrl;
                }

                this.dataBase.UserManager.Update(applicationUser);
                return new OperationDetails(true, "Saving complete.", string.Empty);
            }
        }


        /// <summary>
        /// 
        /// Add reserve to profile
        /// </summary>
        /// <param name="reservation">Reserve to add</param>
        /// <returns>Operation details</returns>
        public OperationDetails AddReservation(Reservation reservation)
        {
            lock (this.dataBase)
            {
                Profile profile = this.GetProfileById(reservation.Profile.ProfileId);
                if (profile != null)
                {
                    ApplicationUser user = this.dataBase.UserManager.FindByName(reservation.User.UserName);
                    if (user != null)
                    {
                        reservation.User = user;
                    }
                    else
                    {
                        return new OperationDetails(false, "User doesn`t exists!", "Reservations");
                    }
                    reservation.Profile = profile;
                    this.dataBase.ReservationManager.Create(reservation);
                    return new OperationDetails(true, "Reservation successfully created!", "Reservations");
                }
                else
                {
                    return new OperationDetails(false, "Profile doesn`t exists!", "Reservations");
                }
            }
        }

        /// <summary>
        /// not ready
        /// </summary>
        /// <param name="reservation"> reserve for delete</param>
        /// <returns>Operation details for delete reserve</returns>
        public OperationDetails DeleteReservation(int reservationId)
        {
            lock (this.dataBase)
            {
                var reservationItems = this.dataBase.RestaurantReservationMenuItemsManager.Find(p => p.ReservationId == reservationId);
                foreach (var item in reservationItems)
                {
                    this.dataBase.RestaurantReservationMenuItemsManager.Delete(item.RestaurantReservationMenuItemId);
                }

                this.dataBase.ReservationManager.Delete(reservationId);
                return new OperationDetails(true, "Reservation item successfully deleted!", "Reservation");
            }
        }

        /// <summary>
        /// Delete comment of profile
        /// </summary>
        /// <param name="commentId">Comment ID to delete</param>
        /// <returns>Operation details</returns>
        public OperationDetails DeleteComment(int commentId)
        {
            lock (this.dataBase)
            {
                this.dataBase.CommentManager.Delete(commentId);
                return new OperationDetails(true, "Comment item successfully deleted!", "Comment");
            }
        }

        /// <summary>
        /// Add favorite profile to user list
        /// </summary>
        /// <param name="IdProfile">Id of Profile</param>
        ///<param name="UserId">Id of User</param>
        /// <returns>Operation details</returns>
        public OperationDetails AddFavorite(int IdProfile, string UserId)
        {
            lock (this.dataBase)
            {
                this.dataBase.UserManager.FindById(UserId).FavoriteProfiles.Add(this.dataBase.ProfileManager.Get(IdProfile));
                return new OperationDetails(true, "Profile successfully deleted!", "Profile");
            }
        }

        /// <summary>
        /// Delete favorite of user
        /// </summary>
        /// <param name="FavoriteId">Profile ID to delete</param>
        /// /// <param name="UserId">User from which delete</param>
        /// <returns>Operation details</returns>
        public OperationDetails DeleteFavorite(int FavoriteId, string UserId)
        {
            lock (this.dataBase)
            {
                this.dataBase.UserManager.FindById(UserId).FavoriteProfiles.Remove(this.dataBase.ProfileManager.Get(FavoriteId));
                return new OperationDetails(true, "Profile successfully deleted!", "Profile");
            }
        }


        /// <summary>
        /// Edit comment of profile
        /// </summary>
        /// <param name="comment">Comment to edit</param>
        /// <returns>Operation details</returns>
        public OperationDetails EditComment(Comment comment)
        {
            lock (this.dataBase)
            {
                Profile profile = this.GetProfileById(comment.Profile.ProfileId);
                if (profile != null)
                {
                    comment.Profile = profile;
                    ApplicationUser user = this.dataBase.UserManager.FindByName(comment.User.UserName);
                    if (user != null)
                    {
                        comment.User = user;
                    }
                    else
                    {
                        return new OperationDetails(false, "User doesn`t exists!", "Comments");
                    }

                    this.dataBase.CommentManager.Update(comment);
                }
                else
                {
                    return new OperationDetails(true, "Profile item successfully updated!", "Comment");
                }

                return new OperationDetails(true, "Comment item successfully updated!", "Comment");
            }
        }

        /// <summary>
        /// Edit comment of profile
        /// </summary>
        /// <param name="comment">Comment to edit</param>
        /// <returns>Operation details</returns>
        public IEnumerable<Reservation> GetReservationsByUserName(string userName)
        {
            lock (this.dataBase)
            {
                ApplicationUser user = this.dataBase.UserManager.FindByName(userName);
                if (user != null)
                {
                    return user.MyReservations.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Edit reservation 
        /// </summary>
        /// <param name="reservation">Reservation to update</param>
        /// <returns>Operation details</returns>
        public OperationDetails EditReservation(Reservation reservation)
        {
            lock (this.dataBase)
            {
                this.dataBase.ReservationManager.Update(reservation);
                return new OperationDetails(true, "Reservation item successfully updated!", "Reservations");
            }
        }

        /// <summary>
        /// Edit restaurant reservation menu
        /// </summary>
        /// <param name="reservationId">Id of reservation</param>
        /// <param name="profileId">Id of profile</param>
        /// <param name="reservation">Menu to update</param>
        /// <returns>Operation details</returns>
        public OperationDetails UpdateRestaurantReservationMenu(int reservationId, ICollection<RestaurantReservationMenuItem> menu)
        {
            lock (this.dataBase)
            {
                Reservation reservation = this.dataBase.ReservationManager.Get(reservationId);
                if (reservation != null)
                {
                    foreach (var item in menu)
                    {
                        item.ReservationId = reservationId;
                        if (item.RestaurantReservationMenuItemId != 0)
                        {
                            if (item.Count == 0)
                            {
                                this.dataBase.RestaurantReservationMenuItemsManager.Delete(item.RestaurantReservationMenuItemId);
                            }
                            else
                            {
                                this.dataBase.RestaurantReservationMenuItemsManager.Update(item);
                            }
                        }
                        else
                        {
                            this.dataBase.RestaurantReservationMenuItemsManager.Create(item);
                        }
                    }

                    return new OperationDetails(true, "Reservation item successfully updated!", "Reservations");
                }
                else
                {
                    return new OperationDetails(false, "Reservation was not found!", "Reservations");
                }
            }
        }

        /// <summary>
        /// Gets all menu items for restaurant reservation
        /// </summary>
        /// <param name="reservationId">Id of restaurant reservation</param>
        /// <returns>Collection of RestaurantReservationMenuItem</returns>
        public ICollection<RestaurantReservationMenuModel> GetMenuForRestaurantReservation(int reservationId)
        {
            lock (this.dataBase)
            {
                List<RestaurantReservationMenuModel> Result = new List<RestaurantReservationMenuModel>();

                var restaurantReservationMenuItems = this.dataBase.RestaurantReservationMenuItemsManager.GetAll().Where(item => item.ReservationId == reservationId);
                foreach (var item in restaurantReservationMenuItems)
                {
                    RestaurantReservationMenuModel restaurantReservationMenuModel = new RestaurantReservationMenuModel();
                    RestaurantMenuItem restaurantMenuItem = this.dataBase.MenuManager.Get(item.MenuItemId);
                    if (restaurantMenuItem != null)
                    {
                        restaurantReservationMenuModel.RestaurantReservationMenuModelId = item.RestaurantReservationMenuItemId;
                        restaurantReservationMenuModel.Callories = restaurantMenuItem.Calories;
                        restaurantReservationMenuModel.Name = restaurantMenuItem.Name;
                        restaurantReservationMenuModel.Price = restaurantMenuItem.Price;
                        restaurantReservationMenuModel.Count = item.Count;
                        restaurantReservationMenuModel.MenuId = restaurantMenuItem.RestaurantMenuItemId;
                    }
                    Result.Add(restaurantReservationMenuModel);
                }

                return Result;
            }
        }

        /// <summary>
        /// Get list of reservation for user
        /// </summary>
        /// <param name="userid">id of user</param>
        /// <returns>ICollection<Reservation></returns>
        public ICollection<Reservation> GetReservationsByUserId(string userid)
        {
            lock (this.dataBase)
            {
                ApplicationUser user = this.dataBase.UserManager.FindById(userid);
                if (user != null)
                {
                    return user.MyReservations.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Check if profile is in favoretes
        /// </summary>
        /// <param name="userid">id of user</param>
        /// /// <param name="idprofile">id of profile</param>
        /// <returns>bool<Reservation></returns>
        public bool HaveInFavorites(int idprofile, string userid)
        {
            lock (this.dataBase)
            {
                ApplicationUser user = this.dataBase.UserManager.FindById(userid);
                if (user != null)
                {
                    return user.FavoriteProfiles.Contains(this.dataBase.ProfileManager.Get(idprofile));
                }
                else
                {
                    return false;
                }
            }
        }

        /// <summary>
        /// gets user role
        /// </summary>
        /// <param name="userId">User to find</param>
        /// <returns>UserRoleEnum</returns>
        public UserRoleEnum GetUserRole(string userId)
        {
            lock (this.dataBase)
            {
                string roleName = this.dataBase.UserManager.GetRoles(userId).First();
                if (roleName == UserRoleEnum.Moderator.ToString())
                {
                    return UserRoleEnum.Moderator;
                }
                else
                    if (roleName == UserRoleEnum.Admin.ToString())
                    {
                        return UserRoleEnum.Admin;
                    }
                    else
                    {
                        return UserRoleEnum.User;
                    }
            }
        }
        /// <summary>
        /// Get EventSubscription by userName profile
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="profileId"> Profile id</param>
        /// <returns>EventSubscription collection</returns>
        public IEnumerable<EventSubscription> GetEventSubscriptionsByUserName(string userName)
        {
            lock (this.dataBase)
            {
                ApplicationUser user = this.dataBase.UserManager.FindByName(userName);
                if (user != null)
                {
                    return user.MyEventSubscriptions.ToList();
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Get EventSubscription by userName profile
        /// </summary>
        /// <param name="subscriptionId"> subscription Id</param>
        /// <returns>EventSubscription </returns>
        public Event GetEventById(int eventId)
        {
            lock (this.dataBase)
            {
                return this.dataBase.EventManager.Get(eventId);
            }
        }

        /// <summary>
        /// Add eventSubscription to profile
        /// </summary>
        /// <param name="reservation">EventSubscription to add</param>
        /// <returns>Operation details</returns>
        public OperationDetails AddEventSubscription(EventSubscription eventSubscription)
        {
            lock (this.dataBase)
            {
                Event profileEvent = this.GetEventById(eventSubscription.Event.EventId);
                if (profileEvent != null)
                {
                    ApplicationUser user = this.dataBase.UserManager.FindByName(eventSubscription.User.UserName);
                    if (user != null)
                    {
                        eventSubscription.User = user;
                    }
                    else
                    {
                        return new OperationDetails(false, "User doesn`t exists!", "EventSubscriptions");
                    }
                    eventSubscription.Event = profileEvent;
                    this.dataBase.EventSubscriptionManager.Create(eventSubscription);
                    return new OperationDetails(true, "Event Subscription successfully created!", "EventSubscriptions");


                }
                else
                {
                    return new OperationDetails(false, "Event doesn`t exists!", "EventSubscriptions");
                }
            }
        }

        /// <summary>
        /// Get EventSubscription by userName profile
        /// </summary>
        /// <param name="eventId"> Event id</param>
        /// <returns>EventSubscription collection</returns>
        public IEnumerable<EventSubscription> GetEventSubscriptionsByEventId(int eventId)
        {
            lock (this.dataBase)
            {
                if (this.dataBase.EventSubscriptionManager.GetAll().Count() != 0)
                {
                    var eventSubscription = this.dataBase.EventSubscriptionManager.GetAll().ToList();
                    var eventSubscriptionList = eventSubscription.Where(subscription => subscription != null && subscription.Event != null && subscription.Event.EventId == eventId && subscription.User != null);
                    if (eventSubscriptionList != null)
                    {
                        return eventSubscriptionList;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    return null;
                }
            }
        }

        // <summary>
        /// Delete Subscription from profile
        /// </summary>
        /// <param name="reservation">Subscription to delete</param>
        /// <returns>Operation details</returns>
        public OperationDetails DeleteEventSubscription(int subscriptionId)
        {
            lock (this.dataBase)
            {
                this.dataBase.EventSubscriptionManager.Delete(subscriptionId);
                return new OperationDetails(true, "Event Subscription item successfully deleted!", "EventSubscription");
            }
        }

        /// <summary>
        /// Create user for external login
        /// </summary>
        /// <param name="mail">user email</param>
        /// <returns>application user</returns>
        public ApplicationUser CreateExternal(string mail, ExternalLoginInfo info)
        {
            lock (this.dataBase)
            {
                var user = this.dataBase.UserManager.FindByEmail(mail);
                if (user != null)
                {
                    var result = this.dataBase.UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Result.Succeeded)
                    {
                        return user;
                    }
                    else
                    {
                        return null;
                    }
                }
                else
                {
                    user = new ApplicationUser
                    {
                        UserName = mail,
                        Email = mail
                    };
                    var result = this.dataBase.UserManager.CreateAsync(user);
                    if (result.Result.Succeeded)
                    {
                        var role = UserRoleEnum.User.ToString();
                        this.dataBase.UserManager.AddToRole(user.Id, role);
                        result = this.dataBase.UserManager.AddLoginAsync(user.Id, info.Login);
                        if (result.Result.Succeeded)
                        {
                            return user;
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }
        }

        /// <summary>
        /// check if in DB user exist
        /// </summary>
        /// <param name="email">email user to find</param>
        /// <returns>bool</returns>
        public OperationDetails IsUserInDataBase(string email)
        {
            lock (this.dataBase)
            {
                if (this.dataBase.UserManager.FindByEmail(email) != null)
                { return new OperationDetails(true, "Good.", string.Empty); }
                else
                { return new OperationDetails(false, "This Email is not exist.", "Email"); }

            }
        }


        /// <summary>
        /// set code to reset password
        /// </summary>
        /// <param name="code">code user</param>
        /// /// <param name="userId">Id user</param>
        /// <returns>bool</returns>
        public void SetCodeForEmail(string code, string email)
        {
            lock (this.dataBase)
            {
                string userid = this.dataBase.UserManager.FindByEmail(email).Id;
                this.dataBase.UserManager.SetPhoneNumber(userid, "---");
                this.dataBase.UserManager.ChangePhoneNumber(userid, code, this.dataBase.UserManager.GenerateChangePhoneNumberToken(userid, code));
            }
        }


        /// <summary>
        /// verify code
        /// </summary>
        /// <param name="code">code user</param>
        ///  <param name="email">email user</param>
        /// <returns>bool</returns>
        public OperationDetails IsCodeValid(string code, string email)
        {
            lock (this.dataBase)
            {
                string userid = this.dataBase.UserManager.FindByEmail(email).Id;
                string usercode = this.dataBase.UserManager.GetPhoneNumber(userid);
                if (usercode == code)
                { return new OperationDetails(true, "Good.", string.Empty); }
                else
                { return new OperationDetails(false, "This Code is not valid.", "Code"); }
            }
        }

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="newPassword">newPassword user</param>
        /// <param name="email">email user</param>
        /// <returns>bool</returns>
        public void ResetPasswordByEmail(string email, string newPassword)
        {
            lock (this.dataBase)
            {
                ApplicationUser user = this.dataBase.UserManager.FindByEmail(email);
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>();
                String hashedNewPassword = this.dataBase.UserManager.PasswordHasher.HashPassword(newPassword);
                store.SetPasswordHashAsync(user, hashedNewPassword);
            }
        }


        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="newPassword">newPassword user</param>
        /// <param name="email">email user</param>
        /// <returns>bool</returns>
        public void ResetPasswordByUserId(string userid, string newPassword)
        {
            lock (this.dataBase)
            {
                ApplicationUser user = this.dataBase.UserManager.FindById(userid);
                UserStore<ApplicationUser> store = new UserStore<ApplicationUser>();
                String hashedNewPassword = this.dataBase.UserManager.PasswordHasher.HashPassword(newPassword);
                store.SetPasswordHashAsync(user, hashedNewPassword);
            }
        }

        /// <summary>
        /// Authenticate User External
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Claims</returns>
        public ClaimsIdentity AuthenticateExternal(string userId)
        {
            lock (this.dataBase)
            {
                string message = String.Empty;
                ClaimsIdentity claim = null;
                var user = this.dataBase.UserManager.FindById(userId);
                if (user != null)
                {
                    var list = this.dataBase.UserManager.GetRoles(user.Id);
                    string roleName = list.First();
                    claim = this.dataBase.UserManager.CreateIdentity(user, "AppCookie");

                    if (user.IsDeleted == true)
                    {
                        message = "Your accont is deleted";
                    }
                    else if (user.IsBanned != null)
                    {
                        if (user.IsBanned.IsBanned == true)
                        {
                            message = "You are banned " + Environment.NewLine;
                            message += "Reason:" + user.IsBanned.Reason;
                        }
                    }
                    else
                        if (roleName == UserRoleEnum.Admin.ToString())
                        {
                            if (user.Company.RequestState == RequestStateEnum.InProcess.ToString() ||
                                String.IsNullOrEmpty(user.Company.RequestState)
                                )
                            {
                                message = "Sorry, but your request is in process now";
                            }
                            else if (user.Company.RequestState == RequestStateEnum.Rejected.ToString())
                            {
                                message = user.Company.Description;
                            }
                        }
                    claim.AddClaim(new Claim(ClaimValueTypes.String, message));
                    claim.AddClaim(new Claim(ClaimValueTypes.KeyInfo, roleName));
                }
                return claim;
            }
        }
        /// <summary>
        /// Get User Email
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>string</returns>
        public string GetUserEmail(string userid)
        {
            lock (this.dataBase)
            {
                return this.dataBase.UserManager.FindById(userid).Email;
            }
        }
    }
}

