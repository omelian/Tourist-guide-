using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuide.BLL.Interfaces;
using TouristGuide.BLL.Services;
using TouristGuide.INF.DataTransferObject;
using TouristGuide.UI.Models;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Specialized;
using System.Configuration;
using System.Threading;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.BLL;
using TouristGuide.INF.Models;
using TouristGuide.INF.Enums;
using System.Globalization;
using System.Security.Claims;
using Microsoft.Owin.Security;
using NLog;

namespace TouristGuide.UI.Controllers
{
    public class ManageController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        /// <summary>
        /// Reference to ModeratorBL
        /// </summary>
        private IModeratorBL moderatorBL = null;

        /// <summary>
        /// Reference to ModeratorBL
        /// </summary>
        private IAdminBL adminBL = null;

        /// <summary>
        /// User FotoUrl
        /// </summary>
        private static string UserFotoUrl { get; set; }

        /// <summary>
        /// User Role
        /// </summary>
        private static UserRoleEnum UserRole { get; set; }

        /// <summary>
        /// Logger
        /// </summary>
        private static Logger logger = LogManager.GetCurrentClassLogger();

        /// <summary>
        /// Instantiate the class that holds the user logic via dependency injection
        /// </summary>
        /// <param name="this.unitOfWork.UserBL">user logic interface</param>
        public ManageController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        /// <summary>
        /// Generate datesrting for datetimepicker
        /// </summary>
        /// <param name="time">DateTime?</param>
        /// <returns>string for datetimepicker</returns>
        public string getDateString(DateTime? time)
        {
            string date;
            if (time == null)
            {
                date = "" ;
            }
            else
            {
                date = DateTime.Parse(time.ToString()).ToString("yyyy-MM-dd");
            }
            return date;
        }

        /// <summary>
        /// Generate datesrting for datetimepicker
        /// </summary>
        /// <param name="time">DateTime?</param>
        /// <returns>string for datetimepicker</returns>
        public string geLatestDateString()
        {
            string date;
                DateTime x = new DateTime();
                x = DateTime.Now.Subtract(TimeSpan.FromDays(365 * 16));
                date = DateTime.Parse(x.ToString()).ToString("yyyy-MM-dd"); ;
            return date;
        }

        /// <summary>
        /// Index page
        /// </summary>
        /// <returns>Show user his information</returns>
        public ActionResult Index()
        {
            var userId = User.Identity.GetUserId();
            UserManageModel model = this.unitOfWork.UserBL.GetUserInfo(userId);
            ViewBag.UserType = this.unitOfWork.UserBL.GetUserRole(userId);
            if (model.DateBirth != DateTime.Parse("01/01/0001"))
            {
                ViewBag.curday = getDateString(model.DateBirth);
            }
            else
            {
                ViewBag.curday = getDateString(null);
            }
            ViewBag.enddate = geLatestDateString();
            return View(model);
        }


        /// <summary>
        /// Sav changes of user data
        /// </summary>
        /// <param name="model">UserManageModel</param>
        /// <returns>Show user his new information </returns>
        /// 
        [HttpPost]
        public ActionResult Index(UserManageModel model)
        {

            string userId = User.Identity.GetUserId();
            if (UserFotoUrl != null)
            {
                model.PhotoUrl = UserFotoUrl;
            }
            this.unitOfWork.UserBL.UpdateUserInfo(model, userId);
            model = this.unitOfWork.UserBL.GetUserInfo(userId);
            ViewBag.UserType = this.unitOfWork.UserBL.GetUserRole(userId);
            ViewBag.curday = getDateString(model.DateBirth);
            ViewBag.enddate = geLatestDateString();
            return View(model);
        }

        /// <summary>
        /// Create IAmazonS3 Client
        /// </summary>    
        /// <returns>IAmazonS3 to load a photo</returns>
        public static IAmazonS3 GetS3Client()
        {
            NameValueCollection appConfig = ConfigurationManager.AppSettings;

            IAmazonS3 s3Client = AWSClientFactory.CreateAmazonS3Client(
                    appConfig["AWSAccessKey"],
                    appConfig["AWSSecretKey"],
                    Amazon.RegionEndpoint.EUCentral1
                    );
            return s3Client;
        }
        /// <summary>
        /// Create random String
        /// </summary>    
        /// <returns>string as filename for amazon</returns>
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }


        /// <summary>
        /// Upload image to Amazon
        /// </summary>    
        /// <returns>Json with URL photo</returns>
        [HttpPost]
        public ActionResult UploadHomeReport()
        {
            try
            {
                HttpPostedFileBase file = Request.Files[0];
                string filekey = RandomString(20) + ".jpg";
                IAmazonS3 client = GetS3Client();

                PutObjectRequest request = new PutObjectRequest();
                request.BucketName = "touristimages";
                request.Key = filekey;
                request.InputStream = file.InputStream;
                request.ContentType = file.ContentType;
                request.CannedACL = S3CannedACL.Private;
                client.PutObject(request);

                var expiryUrlRequest = new GetPreSignedUrlRequest();
                expiryUrlRequest.BucketName = "touristimages";
                expiryUrlRequest.Key = filekey;
                expiryUrlRequest.Expires = DateTime.Now.AddDays(6);

                string url = client.GetPreSignedURL(expiryUrlRequest);
                ViewBag.URL = url;
                UserFotoUrl = url;

                string userId = User.Identity.GetUserId();
                if (UserFotoUrl != null)
                {
                    this.unitOfWork.UserBL.UpdateUserPhoto(url, userId);
                }
            }
            catch (Exception e)
            {
                logger.Error(e);
                ViewBag.URL = (string)(@"https://upload.wikimedia.org/wikipedia/commons/thumb/5/55/Question_Mark.svg/2000px-Question_Mark.svg.png");
            }
            return Json(new { success = true, responseText = ViewBag.URL }, JsonRequestBehavior.AllowGet);
        }


        /// <summary>
        /// Delete favorite 
        /// </summary>    
        /// <returns>Json with rconfirm</returns>
        [HttpPost]
        public ActionResult DaleteFavorite(int profileId)
        {
            this.unitOfWork.UserBL.DeleteFavorite(profileId, User.Identity.GetUserId());
            return Json( true , JsonRequestBehavior.AllowGet);
        }






        /// <summary>
        /// Gets Comments from data Base
        /// </summary>    
        /// <returns>JSON Result All Comments</returns>
        public JsonResult GetAllComments()
        {
            Comment[] comment = this.unitOfWork.UserBL.GetUserComments(User.Identity.GetUserId()).ToArray();
            ProfileCommentViewModel[] userComments = new ProfileCommentViewModel[comment.Length];
            for (int i = 0; i < comment.Length; i++)
            {
                userComments[i] = new ProfileCommentViewModel();
                userComments[i].CommentId = comment[i].CommentId.ToString();
                userComments[i].Text = comment[i].Text;
                userComments[i].DateTime = comment[i].CommentDateTime.ToString("dd'/'MM'/'yyyy HH:mm");
                userComments[i].User = comment[i].User.LastName + " " + comment[i].User.FirstName;
                userComments[i].ProfileId = comment[i].Profile.ProfileId;
                userComments[i].ProfileName = comment[i].Profile.Name;
                userComments[i].Type = comment[i].Profile.Type.Name.ToLower();
                if (!string.IsNullOrEmpty(comment[i].User.Photo))
                {
                    userComments[i].UserPhotoUrl = comment[i].User.Photo;
        }
                else
                {
                    userComments[i].UserPhotoUrl = this.unitOfWork.UserBL.GetUserInfo(comment[i].User.Id).PhotoUrl;
                }

                userComments[i].UserId = comment[i].User.Id;
            }
            return this.Json(userComments, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Delete comment from restaurant
        /// </summary>
        /// <param name="commentId">ID of comment</param>
        [HttpPost]
        public void DeleteComment(int commentId)
        {
            this.unitOfWork.UserBL.DeleteComment(commentId);
        }

        /// <summary>
        /// Update comment from restaurant
        /// </summary>
        /// <param name="comment">Comment to edit</param>
        /// <param name="restaurantId">ID of restaurant</param>
        [HttpPost]
        public void UpdateComment(Comment comment, int restaurantId, string date)
        {
            comment.CommentDateTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            comment.User = new ApplicationUser();
            comment.User.UserName = this.User.Identity.Name;
            comment.Profile = new Profile();
            comment.Profile.ProfileId = restaurantId;
            this.unitOfWork.UserBL.EditComment(comment);
        }


        /// <summary>
        /// Gets Restaurants from data Base
        /// </summary>
        /// <returns>JSON Result All Profiles</returns>
        public JsonResult GetAllFavorites()
        {
            string userId = User.Identity.GetUserId();
            List<Profile> favorites = this.unitOfWork.UserBL.GetUserFavorites(userId);
            UserFavoriteViewModel[] userFavorites = new UserFavoriteViewModel[favorites.Count];
            for (int i = 0; i < favorites.Count; i++)
            {
                userFavorites[i] = new UserFavoriteViewModel();

                userFavorites[i].MainPhoto = this.unitOfWork.UserBL.GetMainPhotoOfProfileById(favorites[i].MainPhoto);
                userFavorites[i].Name = favorites[i].Name;
                userFavorites[i].ProfileId = favorites[i].ProfileId;
                userFavorites[i].Type = favorites[i].Type.Name.ToLower();
            }
            return this.Json(userFavorites, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets Reservations from data Base
        /// </summary>
        /// <returns>JSON Result All Reservations</returns>
        public JsonResult GetAllReservations()
        {
            string userId = User.Identity.GetUserId();
            List<Reservation> reservations = this.unitOfWork.UserBL.GetReservationsByUserId(userId).ToList();
            UserReservationViewModel[] userReservations = new UserReservationViewModel[reservations.Count];
            for (int i = 0; i < reservations.Count; i++)
            {
                userReservations[i] = new UserReservationViewModel();

                userReservations[i].ReservationId = reservations[i].ReservationId;
                userReservations[i].NumberOfPersons = reservations[i].NumberOfPersons;
                userReservations[i].ReservationDate = reservations[i].ReservationDate.ToString();
                userReservations[i].ProfileId = reservations[i].Profile.ProfileId;
                userReservations[i].ProfileName = reservations[i].Profile.Name;
                userReservations[i].Type = reservations[i].Profile.Type.Name.ToLower();
                userReservations[i].MainPhoto = this.unitOfWork.UserBL.GetMainPhotoOfProfileById(reservations[i].Profile.MainPhoto);
            }
            return this.Json(userReservations, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets Comments from data Base
        /// </summary>
        /// <returns>JSON Result All Comments</returns>\
        
        public JsonResult GetAllCommentsForModerator()
        {
            string userId = User.Identity.GetUserId();
            if (this.unitOfWork.UserBL.GetUserRole(userId) == UserRoleEnum.Moderator)
            {
                Profile moderprofile = this.unitOfWork.ModeratorBL.GetModerProfile(userId);
            Comment[] comment = this.unitOfWork.UserBL.GetProfileById(moderprofile.ProfileId).Comments.ToArray();
            ModerCommentViewModel[] profileComments = new ModerCommentViewModel[comment.Length];
            for (int i = 0; i < comment.Length; i++)
            {
                profileComments[i] = new ModerCommentViewModel();
                profileComments[i].CommentId = comment[i].CommentId;
                profileComments[i].Text = comment[i].Text;
                profileComments[i].CommentDateTime = comment[i].CommentDateTime.ToString("dd'/'MM'/'yyyy HH:mm");
                profileComments[i].User = comment[i].User.LastName + " " + comment[i].User.FirstName;
                profileComments[i].ProfileId = comment[i].Profile.ProfileId;
                profileComments[i].Type = comment[i].Profile.Type.Name.ToLower();
                profileComments[i].ProfileName = comment[i].Profile.Name;
                profileComments[i].UserId = comment[i].User.Id;

                if (!string.IsNullOrEmpty(comment[i].User.Photo))
                {
                    profileComments[i].UserPhotoUrl = comment[i].User.Photo;
                }
                else
                {
                    profileComments[i].UserPhotoUrl = this.unitOfWork.UserBL.GetUserInfo(comment[i].User.Id).PhotoUrl;
                }

                profileComments[i].UserId = comment[i].User.Id;   
            }
            return this.Json(profileComments, JsonRequestBehavior.AllowGet);
            }
            else return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets Reservations from data Base
        /// </summary>
        /// <returns>JSON Result All Reservations in profile</returns>
        public JsonResult GetAllReservationsForModerator()
        {
            string userId = User.Identity.GetUserId();
            if (this.unitOfWork.UserBL.GetUserRole(userId) == UserRoleEnum.Moderator)
            {
                Profile moderprofile = this.unitOfWork.ModeratorBL.GetModerProfile(userId);
                List<Reservation> reservations = this.unitOfWork.ModeratorBL.GetReservationsByProfileId(moderprofile.ProfileId).ToList();
                ModerReservationViewModel[] profileReservations = new ModerReservationViewModel[reservations.Count];
                for (int i = 0; i < reservations.Count; i++)
              {
                    profileReservations[i] = new ModerReservationViewModel();
                    profileReservations[i].ReservationId = reservations[i].ReservationId;
                    profileReservations[i].NumberOfPersons = reservations[i].NumberOfPersons;
                    profileReservations[i].ReservationDate = reservations[i].ReservationDate.ToString();
                    profileReservations[i].ProfileId = reservations[i].Profile.ProfileId;
                    profileReservations[i].ProfileName = reservations[i].Profile.Name;
                    profileReservations[i].Type = reservations[i].Profile.Type.Name.ToLower();
                    profileReservations[i].Meals = this.unitOfWork.ModeratorBL.GetMealsByReservationId(reservations[i].ReservationId);
                    profileReservations[i].UserName = reservations[i].User.FirstName + " " + reservations[i].User.LastName;
                }
                return this.Json(profileReservations, JsonRequestBehavior.AllowGet);
            }
            else return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets Reservations from data Base
        /// </summary>
        /// <returns>JSON Result All Reservations in profile</returns>
        public JsonResult GetProfilesForAdmin()
        {
            string userId = User.Identity.GetUserId();
            if (this.unitOfWork.UserBL.GetUserRole(userId) == UserRoleEnum.Admin)
            {
                List<Profile> profiles = this.unitOfWork.AdminBL.GetAllOwnProfiles(userId).ToList();

                AdminProfileViewModel[] profilelist = new AdminProfileViewModel[profiles.Count];
                for (int i = 0; i < profiles.Count; i++)
                {
                    profilelist[i] = new AdminProfileViewModel();
                    profilelist[i].ProfileId = profiles[i].ProfileId;
                    profilelist[i].Name = profiles[i].Name;
                    profilelist[i].MainPhoto = this.unitOfWork.UserBL.GetMainPhotoOfProfileById(profiles[i].MainPhoto);
                    profilelist[i].Company = profiles[i].Company.Name;
                    profilelist[i].Address = profiles[i].Address.City+", "+ profiles[i].Address.Street+" "+profiles[i].Address.Number;
                    profilelist[i].Type = profiles[i].Type.Name.ToLower();

    }
                return this.Json(profilelist, JsonRequestBehavior.AllowGet);
            }
            else return this.Json(null, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets info about moderator from data Base
        /// </summary>
        /// <returns>JSON Result All Comments</returns>\

        public JsonResult GetModerator()
        {
            string userId = User.Identity.GetUserId();
            ModerViewModel model = new ModerViewModel();
            if (this.unitOfWork.UserBL.GetUserRole(userId) == UserRoleEnum.Moderator)
            {
                Profile moderprofile = this.unitOfWork.ModeratorBL.GetModerProfile(userId);
                
                model.ProfileId = moderprofile.ProfileId;
                model.ProfileName = moderprofile.Name;
                model.Type = moderprofile.Type.Name.ToLower();
                return this.Json(model, JsonRequestBehavior.AllowGet);
            }
            else 
           {
            return this.Json(null, JsonRequestBehavior.AllowGet);
           }
        }


        public ActionResult ResetPassword()
        {
            ResetPasswordViewModel model = new ResetPasswordViewModel();
            return View(model);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult ResetPassword(ResetPasswordViewModel model)
        {
            string userId = User.Identity.GetUserId();

           
            if (User.Identity.IsAuthenticated)
            {
                ClaimsIdentity claim = null;
                model.Email = userId;
                this.unitOfWork.UserBL.ResetPasswordByUserId(model.Email, model.Password);
                UserLoginModel loginModel = new UserLoginModel();

                loginModel.Email = this.unitOfWork.UserBL.GetUserEmail(userId);
                loginModel.Password = model.Password;
                claim = this.unitOfWork.UserBL.Authenticate(loginModel);         
                    this.AuthenticationManager.SignOut();
                    this.AuthenticationManager.SignIn(new AuthenticationProperties
                    {
                        IsPersistent = true
                    }, claim);

                    return RedirectToAction("Index", "Home");
            }
            else
            {
                logger.Error("Invalid reset password operation, guest is not signed in.");
                ModelState.AddModelError("", "You're not signed in!");
            }

            return View(model);
        }
    }
}