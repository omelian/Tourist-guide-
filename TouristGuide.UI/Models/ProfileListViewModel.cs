using TouristGuide.INF.Models;

namespace TouristGuide.UI.Models
{
    /// <summary>
    /// List View Model
    /// </summary>
    public class ProfileListViewModel
    {
        /// <summary>
        ///  Gets or sets profile id
        /// </summary>
        public int ProfileId { get; set; }

        /// <summary>
        ///  Gets or sets profile name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets profile city
        /// </summary>
        public string City { get; set; }

        /// <summary>
        ///  Gets or sets profile street
        /// </summary>
        public string Street { get; set; }

        /// <summary>
        ///  Gets or sets profile building number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Gets or sets profile location x
        /// </summary>
        public double XCoord { get; set; }

        /// <summary>
        /// Gets or sets profile location x
        /// </summary>
        public double YCoord { get; set; }

        /// <summary>
        ///  Gets or sets url of main photo
        /// </summary>
        public string MainPhotoUrl { get; set; }

        /// <summary>
        ///  Gets or sets rate of profile
        /// </summary>
        public double Rate { get; set; }

        /// <summary> 
        /// Gets or sets profile property if is showed for all users or not 
        /// </summary> 
        public bool IsShowed { get; set; }
    }
}