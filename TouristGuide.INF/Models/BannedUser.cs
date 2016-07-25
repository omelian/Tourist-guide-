using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// BannedUser model
    /// </summary>
    public class BannedUser
    {
        /// <summary>
        /// Gets or sets id
        /// </summary>
        [Key, ForeignKey("User")]
        public string BannedUserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// Gets or sets count of banning user
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Gets or sets reference for ApplicationUser
        /// </summary>
        public virtual ApplicationUser User { get; set; }

        /// <summary>
        /// Gets or sets date when user was banned at last
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public virtual DateTime UserDateTime { get; set; }

        /// <summary>
        /// Gets or sets reason why user was banned
        /// </summary>
        public string Reason { get; set; }
    }
}
