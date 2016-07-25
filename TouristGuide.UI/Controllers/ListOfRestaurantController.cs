using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using TouristGuide.BLL.Infrastructure;
using TouristGuide.BLL.Interfaces;
using TouristGuide.BLL;
using TouristGuide.INF.Models;
using TouristGuide.INF.Enums;
using TouristGuide.UI.Models;
using Amazon.S3;
using Amazon.S3.Model;
using System.Collections.Specialized;
using System.Configuration;
using Amazon;
using System.Globalization;

namespace TouristGuide.UI.Controllers
{
    /// <summary>
    /// Profiles manage class
    /// </summary>
    public class ListOfRestaurantController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

        /// <summary>
        /// Initializes  GuestBL and UserBL classes
        /// </summary>
        /// <param name="guest">Guest Interface</param>
        /// <param name="user">User Interface</param>
        /// <see cref="ListOfRestaurantController(IGuestBL guest,IUserBL user)"/>
        public ListOfRestaurantController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets Restaurants from list for example
        /// </summary>
        /// <returns>JSON Result with example profiles</returns>
        public JsonResult GetRestaurants()
        {
            List<ProfileListViewModel> datas = new List<ProfileListViewModel>();
            datas.Add(new ProfileListViewModel { ProfileId = 0, Name = "Chas Poisty", City = "Lviv", Street = "Bandery St", Number = "3b" });
            datas.Add(new ProfileListViewModel { ProfileId = 1, Name = "Lol", City = "London", Street = "Bandery St", Number = "4d" });
            datas.Add(new ProfileListViewModel { ProfileId = 2, Name = "Chas chanky", City = "Kyiv", Street = "Bandery St", Number = "8b" });
            datas.Add(new ProfileListViewModel { ProfileId = 3, Name = "Colobok", City = "Lviv", Street = "Bandery St", Number = "3b" });
            datas.Add(new ProfileListViewModel { ProfileId = 4, Name = "Xarkiv", City = "Xarkiv", Street = "Bandery St", Number = "3b" });
            datas.Add(new ProfileListViewModel { ProfileId = 5, Name = "Kok", City = "Lviv", Street = "Bandery St", Number = "3b" });
            datas.Add(new ProfileListViewModel { ProfileId = 6, Name = "Kir", City = "Lviv", Street = "Bandery St", Number = "3b" });
            datas.Add(new ProfileListViewModel { ProfileId = 777, Name = "Efoi", City = "Lviv", Street = "Bandery St", Number = "3b" });
            datas.Add(new ProfileListViewModel { ProfileId = 8, Name = "Korn", City = "Lviv", Street = "Bandery St", Number = "3b" });
            datas.Add(new ProfileListViewModel { ProfileId = 99, Name = "Lis", City = "Lviv", Street = "Bandery St", Number = "3b" });
            datas.Add(new ProfileListViewModel { ProfileId = 12, Name = "Lol", City = "Lviv", Street = "Bandery St", Number = "3b" });

            return this.Json(datas, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets Restaurants from data Base
        /// </summary>
        /// <returns>JSON Result All Profiles</returns>
        public JsonResult GetAllRestaurants()
        {
            IEnumerable<ProfileListViewModel> restaurantList = null;
            if (User.IsInRole(UserRoleEnum.Admin.ToString()))
            {
                restaurantList = this.unitOfWork.GuestBL.GetAllRestaurants()
                .Where(p => p.Company.User.Id == User.Identity.GetUserId() && p.IsBanned == null && p.IsDeleted == false).ToList()
                .Select(profile => (
               new ProfileListViewModel
               {
                   ProfileId = profile.ProfileId,
                   Name = profile.Name,
                   City = profile.Address.City,
                   Street = profile.Address.Street,
                   Number = profile.Address.Number,
                   MainPhotoUrl = this.unitOfWork.GuestBL.GetMainPhotoOfProfileById(profile.MainPhoto),
                   XCoord = profile.Address.Location.XCoord,
                   YCoord = profile.Address.Location.YCoord,
                   Rate = CalculateAvgProfileRate(profile.Rates)
               }));
            }
            else
            {
                if (User.IsInRole(UserRoleEnum.Moderator.ToString()))
                {
                    restaurantList = this.unitOfWork.GuestBL.GetAllRestaurants()
                     .Where(profile => profile.IsBanned == null && profile.IsDeleted == false)
                    .Select(profile => (
              new ProfileListViewModel
              {
                  ProfileId = profile.ProfileId,
                  Name = profile.Name,
                  City = profile.Address.City,
                  Street = profile.Address.Street,
                  Number = profile.Address.Number,
                  MainPhotoUrl = this.unitOfWork.GuestBL.GetMainPhotoOfProfileById(profile.MainPhoto),
                  Rate = CalculateAvgProfileRate(profile.Rates),
                  IsShowed = profile.IsShowed
              }));
                }
                else
                {
                    restaurantList = this.unitOfWork.GuestBL.GetAllRestaurants()
                         .Where(profile => profile.IsBanned == null && profile.IsDeleted == false && profile.IsShowed == true)
                        .Select(profile => (
                  new ProfileListViewModel
                  {
                      ProfileId = profile.ProfileId,
                      Name = profile.Name,
                      City = profile.Address.City,
                      Street = profile.Address.Street,
                      Number = profile.Address.Number,
                      MainPhotoUrl = this.unitOfWork.GuestBL.GetMainPhotoOfProfileById(profile.MainPhoto),
                      Rate = CalculateAvgProfileRate(profile.Rates),
                      IsShowed = profile.IsShowed
                  }));
                }

            }





            return this.Json(restaurantList, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Calculate avg rate
        /// </summary>
        /// <param name="rates">Collection of rates</param>
        /// <returns>Avg Rate</returns>
        public static double CalculateAvgProfileRate(ICollection<Rate> rates)
        {
            double result = 0;
            var profileMarksSum = 0;
            foreach (var item in rates)
            {
                profileMarksSum += item.Mark;
            }

            if (rates.Count > 0)
            {
                result = profileMarksSum / rates.Count;
            }

            return result;
        }

        /// <summary>
        /// Gets Restaurant data for restaurant page
        /// </summary>
        /// <param name="restaurantId"> Profile Id</param>
        /// <returns>JSON Result with Profile by Id</returns>
        [Route("/{RestaurantId}")]
        public JsonResult GetRestaurantByIdDemo(int restaurantId)
        {
            List<ProfilePageViewModel> pages = new List<ProfilePageViewModel>();
            pages.Add(new ProfilePageViewModel
            {
                Name = "Chas Poisty",
                City = "Lviv",
                Street = "Bandery St",
                Number = "3b"
            });

            pages.Add(new ProfilePageViewModel
            {
                Name = "Lol",
                City = "London",
                Street = "Bandery St",
                Number = "4d"
            });

            return this.Json(pages[restaurantId], JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets Restaurant data for restaurant page
        /// </summary>
        ///  <param name="restaurantId"> Profile Id</param>
        /// <returns>JSON Result with Profile by Id</returns>
        public JsonResult GetRestaurantById(int restaurantId)
        {
            Profile profile = this.unitOfWork.GuestBL.GetProfileById(restaurantId);
            ProfilePageViewModel restaurant = new ProfilePageViewModel
            {
                Name = profile.Name,
                City = profile.Address.City,
                Street = profile.Address.Street,
                Number = profile.Address.Number,
                MainPhoto = profile.MainPhoto,
                IsShowed = profile.IsShowed
            };
            var moders = profile.Moders;
            List<string> moderators = new List<string>();
            if (moderators != null)
            {
                foreach (var item in moders)
                {
                    moderators.Add(item.Id);
                }
            }
            restaurant.Moderators = moderators;
            return this.Json(restaurant, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets Restaurant news data for restaurant page
        /// </summary>
        /// <param name="restaurantId"> Profile Id</param>
        /// <returns>JSON Result with Profile News</returns>
        public JsonResult GetNewsByRestaurantId(int restaurantId)
        {
            var news = this.unitOfWork.GuestBL.GetProfileById(restaurantId).News;
            List<ProfileNewsViewModel> restaurantNews = new List<ProfileNewsViewModel>();
            foreach (var item in news)
            {
                ProfileNewsViewModel restaurantNewsModel = new ProfileNewsViewModel();
                restaurantNewsModel.NewsId = item.NewsId;
                restaurantNewsModel.DateTime = item.NewsDate.ToString("dd'/'MM'/'yyyy HH:mm");
                restaurantNewsModel.Title = item.Title;
                restaurantNewsModel.NewsImageUrl = item.NewsImageUrl;
                restaurantNewsModel.TextContent = item.TextContent;
                restaurantNews.Add(restaurantNewsModel);
            }

            return this.Json(restaurantNews, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets Restaurant comments data for restaurant page
        /// </summary>
        /// <param name="restaurantId"> Profile Id</param>
        /// <returns>JSON Result with Profile Comments</returns>
        public JsonResult GetCommentsByRestaurantId(int restaurantId)
        {
            var comments = this.unitOfWork.GuestBL.GetProfileById(restaurantId).Comments;
             List<ProfileCommentViewModel> restaurantComments = new  List<ProfileCommentViewModel> ();
             foreach (var item in comments)
             {
                 ProfileCommentViewModel restaurantComment = new ProfileCommentViewModel();
                 restaurantComment.CommentId = item.CommentId.ToString();
                 restaurantComment.Text = item.Text;
                 restaurantComment.DateTime = item.CommentDateTime.ToString("dd'/'MM'/'yyyy HH:mm");
                 restaurantComment.User = item.User.LastName + " " + item.User.FirstName;
                 if (!string.IsNullOrEmpty(item.User.Photo))
                 {
                     restaurantComment.UserPhotoUrl = item.User.Photo;
                 }
                 else
                 {
                     restaurantComment.UserPhotoUrl = this.unitOfWork.UserBL.GetUserInfo(item.User.Id).PhotoUrl;
                 }

                 restaurantComment.UserId = item.User.Id;
                 restaurantComments.Add(restaurantComment);
             }

             return this.Json(restaurantComments, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Gets Restaurant photos for page
        /// </summary>
        /// <param name="restaurantId"> Profile Id</param>
        /// <returns>JSON Result with Profile Comments</returns>
        public JsonResult GetPhotosById(int profileId)
        {
            var profilePhotos = this.unitOfWork.GuestBL.GetProfileById(profileId).Photos;
            List<ProfilePhotoViewModel> profilePhotoViewModels = new List<ProfilePhotoViewModel>();
            foreach (var item in profilePhotos)
            {
                ProfilePhotoViewModel profilePhotoViewModel = new ProfilePhotoViewModel();
                profilePhotoViewModel.Id = item.ProfilePhotoId;
                profilePhotoViewModel.Url = item.Url;
                profilePhotoViewModel.Descripton = item.Descripton;
                profilePhotoViewModels.Add(profilePhotoViewModel);
            }

            return this.Json(profilePhotoViewModels, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add news to profile by id
        /// </summary>
        /// <param name="news">News to add</param>
        /// <param name="date">Date of news</param>
        [HttpPost]
        public void AddNews(ProfileNewsViewModel news, int restaurantId, string date)
        {
            News profileNews = new News();
            profileNews.Profile = new Profile();
            profileNews.Profile.ProfileId = restaurantId;
            profileNews.NewsDate = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            profileNews.TextContent = news.TextContent;
            profileNews.Title = news.Title;
            profileNews.NewsImageUrl = news.NewsImageUrl;
            this.unitOfWork.ModeratorBL.AddNews(profileNews);
        }

        /// <summary>
        /// Delete news  from restaurant
        /// </summary>
        /// <param name="newsId">ID of news</param>
        /// <param name="restaurantId">ID of restaurant</param>
        [HttpPost]
        public void DeleteNewsFromRestaurant(int newsId, int restaurantId)
        {
            this.unitOfWork.ModeratorBL.DeleteNews(newsId);
        }

        /// <summary>
        /// Update news news from restaurant
        /// </summary>
        /// <param name="newsId">News to edit</param>
        /// <param name="restaurantId">ID of restaurant</param>
        [HttpPost]
        public void UpdateRestaurantNews(News news, int restaurantId, string date)
        {
            news.NewsDate = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            this.unitOfWork.ModeratorBL.EditNews(news);
        }

        /// <summary>
        /// Add comments to profile
        /// </summary>
        /// <param name="comment">Comment to add</param>
        /// <param name="restaurantId">ID of restaurant</param>
        /// <param name="date">Date of comment</param>
        [HttpPost]
        public void AddComment(Comment comment, int restaurantId, string date)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                comment.User = new ApplicationUser() { Id = User.Identity.GetUserId() };
            }

            comment.Profile = new Profile() { ProfileId = restaurantId };
            comment.CommentDateTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            this.unitOfWork.UserBL.AddComment(comment);
        }


        /// <summary>
        /// Add favorite 
        /// </summary>    
        /// <returns>Json with rconfirm</returns>
        [HttpPost]
        public ActionResult AddFavorite(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.unitOfWork.UserBL.AddFavorite(id, User.Identity.GetUserId());
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Remove favorite 
        /// </summary>    
        /// <returns>Json with rconfirm</returns>
        [HttpPost]
        public ActionResult RemoveFavorite(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                this.unitOfWork.UserBL.DeleteFavorite(id, User.Identity.GetUserId());
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
        }

        /// <summary>
        /// Delete comment from restaurant
        /// </summary>
        /// <param name="commentId">ID of comment</param>
        [HttpPost]
        public void DeleteCommentFromRestaurant(int commentId)
        {
            this.unitOfWork.UserBL.DeleteComment(commentId);
        }

        /// <summary>
        /// Delete photo from restaurant
        /// </summary>
        /// <param name="photoId">ID of photo</param>
        [HttpPost]
        public void DeletePhotoById(int photoId)
        {
            this.unitOfWork.ModeratorBL.DeletePhoto(photoId);
        }

        /// <summary>
        ///  Set main photo of profile
        /// </summary>
        /// <param name="photoId">ID of photo</param>
        /// <param name="profileId">ID of profile</param>
        /// <returns>Operation result string</returns>
        [HttpPost]
        public string SetMainPhoto(int photoId, int profileId)
        {
            return this.unitOfWork.ModeratorBL.SetMainPhotoOfProfile(photoId, profileId).Message;
        }

        /// <summary>
        /// Update comment from restaurant
        /// </summary>
        /// <param name="comment">Comment to edit</param>
        /// <param name="restaurantId">ID of restaurant</param>
        [HttpPost]
        public void UpdateRestaurantComment(Comment comment, int restaurantId, string date)
        {
            comment.CommentDateTime = DateTime.ParseExact(date, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture);
            comment.User = new ApplicationUser();
            comment.User.UserName = this.User.Identity.Name;
            comment.Profile = new Profile();
            comment.Profile.ProfileId = restaurantId;
            this.unitOfWork.UserBL.EditComment(comment);
        }

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
        /// Uploads photo to amazon
        /// </summary>
        /// <param name="uploadedFile">Photo to upload</param>
        /// <returns>Ulr of photo on amazaon</returns>
        public string UploadPhoto(HttpPostedFileBase uploadedFile)
        {
            HttpPostedFileBase file = uploadedFile;
            string filekey = file.FileName;
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
            expiryUrlRequest.Expires = DateTime.Now.AddDays(7);
            return client.GetPreSignedURL(expiryUrlRequest);
        }

        /// <summary>
        /// Add photo to profile
        /// </summary>
        /// <param name="uploadedFile">Photo to upload</param>
        /// <param name="descripton">Descripton of photo</param>
        /// <param name="profileId">ID od profile</param>
        public void AddPhotoToProfile(HttpPostedFileBase uploadedFile, string descripton, int profileId)
        {
            string url = UploadPhoto(uploadedFile);
            ProfilePhoto photo = new ProfilePhoto();
            photo.Descripton = descripton;
            photo.Profile = new Profile();
            photo.Profile.ProfileId = profileId;
            photo.Url = url;

            this.unitOfWork.ModeratorBL.AddPhoto(photo);

        }

        /// <summary>
        /// Check if in favorites
        /// </summary>
        /// <param name="id">ID of profile</param>
        public ActionResult IsInFavorites(int id)
        {
            if (this.User.Identity.IsAuthenticated)
            {
                if (unitOfWork.UserBL.HaveInFavorites(id, User.Identity.GetUserId()))
                {
                    return Json(true, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}