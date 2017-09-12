namespace SchemaApi.Models
{

    /// <summary>
    /// item that describeof one test result on the server
    /// </summary>
    public class HeartbeatResponse
    {

        /// <summary>
        /// Gets or sets the test name.
        /// </summary>
        /// <value>
        /// The test.
        /// </value>
        public string Test { get; set; }

        /// <summary>
        /// Gets or sets the value of the test result.
        /// </summary>
        /// <value>
        /// The value.
        /// </value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets a value indicating the test <see cref="HeartbeatResponse"/> is succeded.
        /// </summary>
        /// <value>
        ///   <c>true</c> if succeded; otherwise, <c>false</c>.
        /// </value>
        public bool Succeded { get; set; }

    }
}