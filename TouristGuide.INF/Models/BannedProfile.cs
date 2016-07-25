using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TouristGuide.INF.Models
{
    /// <summary>
    /// Banned profile model
    /// </summary>
    public class BannedProfile
    {
        /// <summary>
        /// Gets or sets id
        /// </summary>
        [Key, ForeignKey("Profile")]
        public int BannedProfileId { get; set; }

        /// <summary>
        /// Gets or sets reference to profile
        /// </summary>
        public virtual Profile Profile { get; set; }

        /// <summary>
        /// Gets or sets date when profile was banned
        /// </summary>
        [Column(TypeName = "DateTime2")]
        public virtual DateTime BannedProfileDateTime { get; set; }

        /// <summary>
        /// Gets or sets reason why it was banned
        /// </summary>
        public string Reason { get; set; }
    }
}
