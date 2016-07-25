using System;
namespace TouristGuide.UI.Models
{
    /// <summary>
    /// Comment View Model
    /// </summary>
    public class ProfileCommentViewModel
    {
        /// <summary>
        ///  Gets or sets id of comment
        /// </summary>
        public string CommentId { get; set; }

        /// <summary>
        ///  Gets or sets user id
        /// </summary>
        public string UserId { get; set; }

        /// <summary>
        ///  Gets or sets name of user
        /// </summary>
        public string User { get; set; }

        /// <summary>
        ///  Gets or sets url of user photo
        /// </summary>
        public string UserPhotoUrl { get; set; }

        /// <summary>
        ///  Gets or sets comment Text
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        ///  Gets or sets DateTime of comment
        /// </summary>
        public string DateTime { get; set; }

        /// <summary>
        ///  Gets or sets Profile id
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        ///  Gets or sets name of Profile
        /// </summary>
        public string ProfileName { get; set; }

        /// <summary>
        /// Gets or sets type profile
        /// </summary>
        public string Type { get; set; }

    }
}