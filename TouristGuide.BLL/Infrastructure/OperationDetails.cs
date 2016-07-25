namespace TouristGuide.BLL.Infrastructure
{
    /// <summary>
    /// Details of data access operation
    /// </summary>
    public class OperationDetails
    {
        /// <summary>
        /// Initializes a new instance of the OperationDetails class.
        /// </summary>
        /// <param name="success">Value that indicates if Operation is success</param>
        /// <param name="message">Result message</param>
        /// <param name="property">Property of Operation</param>
        public OperationDetails(bool success, string message, string property)
        {
            this.Successfully = success;
            this.Message = message;
            this.Property = property;
        }

        /// <summary>
        /// Gets a value indicating whether operation is executed successfully
        /// </summary>
        public bool Successfully { get; private set; }

        /// <summary>
        /// Gets result Message of operation
        /// </summary>
        public string Message { get; private set; }

        /// <summary>
        /// Gets a Property of operation
        /// </summary>
        public string Property { get; private set; }
    }
}
