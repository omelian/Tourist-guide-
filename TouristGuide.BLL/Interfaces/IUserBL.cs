using System.Collections.Generic;
using System.Security.Claims;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.INF.Enums;
using TouristGuide.INF.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;

namespace TouristGuide.BLL.Interfaces
{
    /// <summary>
    /// Defines methods for User business logic
    /// </summary>
    public interface IUserBL : IGuestBL
    {
        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="user">User item</param>
        /// <returns>Operation details</returns>
        OperationDetails Create(UserRegisterModel user);

        /// <summary>
        /// Authenticate user
        /// </summary>
        /// <param name="userLoginModel">User to Authenticate</param>
        /// <returns>Claims identity</returns>
        ClaimsIdentity Authenticate(UserLoginModel userLoginModel);

        /// <summary>
        /// Set rate to profile
        /// </summary>
        /// <param name="profile">Profile to set rate</param>
        /// <returns>Operation details</returns>
        OperationDetails SetRate(ProfileRate rate);

        /// <summary>
        /// Add comment to profile
        /// </summary>
        /// <param name="comment">Comment to add</param>
        /// <returns>Operation details</returns>
        OperationDetails AddComment(Comment comment);


        /// <summary>
        /// Add favorite profile to user list
        /// </summary>
        /// <param name="IdProfile">Id of Profile</param>
        ///<param name="UserId">Id of User</param>
        /// <returns>Operation details</returns>
        OperationDetails AddFavorite(int IdProfile, string UserId);

        /// <summary>
        /// Get information of user
        /// </summary>
        /// <param name="userid">User id</param>
        /// <returns>UserManageModel</returns>
        UserManageModel GetUserInfo(string userid);

        /// <summary>
        /// Get coments of user
        /// </summary>
        /// <param name="userid">User id</param>
        /// <returns>UserManageModel</returns>
        List<Comment> GetUserComments(string userid);

        /// <summary>
        /// Get user's rates
        /// </summary>
        /// <param name="userId">User id</param>
        /// <returns>List of user's rates</returns>
        List<Rate> GetUserRates(string userId);

        /// <summary>
        /// Get favorites profiles of user
        /// </summary>
        /// <param name="userid">User id</param>
        /// <returns>UserManageModel</returns>
        List<Profile> GetUserFavorites(string userid);

        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name="model">UserManageModel</param>
        /// <param name="userid">User id</param>
        /// <returns>Operation details</returns>
        OperationDetails UpdateUserInfo(UserManageModel model, string userId);
        /// <summary>
        /// Update user data
        /// </summary>
        /// <param name="photoUrl">photo Url</param>
        /// <param name="userid">User id</param>
        /// <returns>Operation details</returns>
        OperationDetails UpdateUserPhoto(string photoUrl, string userId);
        
        /// <summary>
        /// Delete comment of profile
        /// </summary>
        /// <param name="commentId">Comment ID to delete</param>
        /// <returns>Operation details</returns>
        OperationDetails DeleteComment(int commentId);

        /// <summary>
        /// Delete favorite of user
        /// </summary>
        /// <param name="FavoriteId">Profile ID to delete</param>
        /// /// <param name="UserId">User from which delete</param>
        /// <returns>Operation details</returns>
        OperationDetails DeleteFavorite(int FavoriteId, string UserId);

        /// <summary>
        /// Edit comment of profile
        /// </summary>
        /// <param name="comment">Comment to edit</param>
        /// <returns>Operation details</returns>
        OperationDetails EditComment(Comment comment);

        /// <summary>
        /// Add reservation to profile
        /// </summary>
        /// <param name="reservation">Reservation to add</param>
        /// <returns>Operation details</returns>
        OperationDetails AddReservation(Reservation reservation);

        /// <summary>
        /// Delete reservation from profile
        /// </summary>
        /// <param name="reservation">Reservation to delete</param>
        /// <returns>Operation details</returns>
        OperationDetails DeleteReservation(int reservationId);

        /// <summary>
        /// Get reservation by userName profile
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <returns>Reservation collection</returns>
        IEnumerable<Reservation> GetReservationsByUserName(string userName);

        /// <summary>
        /// Edit reservation 
        /// </summary>
        /// <param name="reservation">Reservation to update</param>
        /// <returns>Operation details</returns>
        OperationDetails EditReservation(Reservation reservation);

        /// <summary>
        /// Get list of reservation for user
        /// </summary>
        /// <param name="userid">id of user</param>
        /// <returns>ICollection<Reservation></returns>
        ICollection<Reservation> GetReservationsByUserId(string userid);

        /// <summary>
        /// Check if profile is in favoretes
        /// </summary>
        /// <param name="userid">id of user</param>
        /// /// <param name="idprofile">id of profile</param>
        /// <returns>bool<Reservation></returns>
        bool HaveInFavorites(int idprofile, string userid);

        /// <summary>
       /// Edit restaurant reservation menu
       /// </summary>
       /// <param name="reservationId">Id of reservation</param>
       /// <param name="reservation">Menu to update</param>
       /// <returns>Operation details</returns>
        OperationDetails UpdateRestaurantReservationMenu(int reservationId,  ICollection<RestaurantReservationMenuItem> menu);

       /// <summary>
       /// Gets all menu items for restaurant reservation
       /// </summary>
       /// <param name="reservationId">Id of restaurant reservation</param>
        /// <returns>Collection of RestaurantReservationMenuItem</returns>
        ICollection<RestaurantReservationMenuModel> GetMenuForRestaurantReservation(int reservationId);

        /// <summary>
        /// gets user role
        /// </summary>
        /// <param name="userId">User to find</param>
        /// <returns>UserRoleEnum</returns>
        UserRoleEnum GetUserRole(string userId);

        

        /// <summary>
        /// Get EventSubscription by userName profile
        /// </summary>
        /// <param name="userName">User Name</param>
        /// <param name="profileId"> Profile id</param>
        /// <returns>EventSubscription collection</returns>
        IEnumerable<EventSubscription> GetEventSubscriptionsByUserName(string userName);

        /// <summary>
        /// Get EventSubscription by userName profile
        /// </summary>
        /// <param name="eventId"> Event id</param>
        /// <returns>EventSubscription</returns>
        IEnumerable<EventSubscription> GetEventSubscriptionsByEventId(int eventId);

         /// <summary>
        /// Get EventSubscription by userName profile
        /// </summary>
        /// <param name="eventId"> event Id</param>
        /// <returns>Event </returns>
        Event GetEventById(int eventId);

        /// <summary>
        /// Add eventSubscription to profile
        /// </summary>
        /// <param name="reservation">EventSubscription to add</param>
        /// <returns>Operation details</returns>
        OperationDetails AddEventSubscription(EventSubscription eventSubscription);

        /// <summary>
        /// Delete Subscription from profile
        /// </summary>
        /// <param name="reservation">Subscription to delete</param>
        /// <returns>Operation details</returns>
        OperationDetails DeleteEventSubscription(int subscriptionId);

        /// <summary>
        /// check if in DB user exist
        /// </summary>
        /// <param name="email">email user to find</param>
        /// <returns>bool</returns>
        OperationDetails IsUserInDataBase(string email);

        /// <summary>
        /// set code to reset password
        /// </summary>
        /// <param name="code">code user</param>
        /// /// <param name="userId">Id user</param>
        /// <returns>bool</returns>
        void SetCodeForEmail(string code, string userId);

        /// <summary>
        /// verify code
        /// </summary>
        /// <param name="code">code user</param>
        ///  <param name="email">email user</param>
        /// <returns>bool</returns>
        OperationDetails IsCodeValid(string code, string email);

        /// <summary>
       /// reset password
       /// </summary>
       /// <param name="newPassword">newPassword user</param>
       /// <param name="email">email user</param>
       /// <returns>bool</returns>
        void ResetPasswordByEmail(string email, string newPassword);

        /// <summary>
        /// reset password
        /// </summary>
        /// <param name="newPassword">newPassword user</param>
        /// <param name="userid">id user</param>
        /// <returns>bool</returns>
        void ResetPasswordByUserId(string userid, string newPassword);

        /// <summary>
        /// Create user for external login
        /// </summary>
        /// <param name="mail">user email</param>
        /// <returns>application user</returns>
        ApplicationUser CreateExternal(string mail, ExternalLoginInfo info);  
      
        /// <summary>
        /// Authenticate User External
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>Claims</returns>
        ClaimsIdentity AuthenticateExternal(string userId);

        /// <summary>
        /// Get User Email
        /// </summary>
        /// <param name="userId">user Id</param>
        /// <returns>string</returns>
        string GetUserEmail(string userid);

    }
}
