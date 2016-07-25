namespace TouristGuide.INF.DataTransferObject
{
    /// <summary>
    /// Encapsulate data, and send it from one subsystem of an application to another.
    /// </summary>
    public class UserLoginModel
    {
        /// <summary>
        /// Gets or sets email for login model
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets password for login model
        /// </summary>
        public string Password { get; set; }
    }
}
