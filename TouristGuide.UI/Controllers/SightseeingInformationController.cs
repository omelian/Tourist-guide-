using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TouristGuide.BLL.Interfaces;
using TouristGuide.INF.Models;
using TouristGuide.UI.Models;

namespace TouristGuide.UI.Controllers
{
    /// <summary>
    /// Attraction Information
    /// </summary>
    public class SightseeingInformationController : Controller
    {
        /// <summary>
        /// UnitOfWork reference
        /// </summary>
        private IUnitOfWork unitOfWork = null;

         /// <summary>
        /// Initializes  GuestBL  classes
        /// </summary>
        public SightseeingInformationController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        /// <summary>
        /// Gets Restaurant comments data for restaurant page
        /// </summary>
        /// <param name="restaurantId"> Profile Id</param>
        /// <returns>JSON Result with Profile Comments</returns>
        public JsonResult GetInformationBySightseeingId(int sightseeingId)
        {
            IEnumerable<InformationViewModel> article = this.unitOfWork.GuestBL.GetArticleByProfileId(sightseeingId).Select(
                inf => (new InformationViewModel {
                    ArticleId = inf.ArticleId,
                    Text = inf.Text,
                    Title = inf.Title,
                    PictureUrl = inf.PictureUrl,
                    Email = inf.Email,
                    PhoneNumber = inf.PhoneNumber,
                    FacebookReference = inf.FacebookReference,
                    WebSiteReference = inf.WebSiteReference
                })
                );
            return this.Json(article, JsonRequestBehavior.AllowGet);
        }

        /// <summary>
        /// Add information about sightseeing by id
        /// </summary>
        /// <param name="article">Information to add</param>
        /// <param name="sightseeingId">Sightseeing id value</param>
        [HttpPost]
        public void AddInformation(InformationViewModel article, int sightseeingId)
        {
            Article sightseeingInformation = new Article();
            sightseeingInformation.Profile = new Profile();
            sightseeingInformation.Profile.ProfileId = sightseeingId;
            sightseeingInformation.Title = article.Title;
            sightseeingInformation.Text = article.Text;
            sightseeingInformation.PictureUrl = article.PictureUrl;
            sightseeingInformation.Email = article.Email;
            sightseeingInformation.PhoneNumber = article.PhoneNumber;
            sightseeingInformation.FacebookReference = article.FacebookReference;
            this.unitOfWork.ModeratorBL.AddSightseeingArticle(sightseeingInformation);
        }

        /// <summary>
        /// Edit information about sightseeing 
        /// </summary>
        /// <param name="article">Information to update</param>
        [HttpPost]
        public void EditInformation(Article article)
        {
            this.unitOfWork.ModeratorBL.EditSightseeingArticle(article);
        }
    }
}