namespace MsOpenIA.Infrastructure.Configuration
{
    /// <summary>
    /// Represents the settings required for configuring OpenAI services.
    /// </summary>
    public class OpenIaSettings
    {
        /// <summary>
        /// Gets or sets the base URL for the OpenAI API.
        /// </summary>
        public string BaseUrl { get; set; }

        /// <summary>
        /// Gets or sets the API key for authenticating with OpenAI.
        /// </summary>
        public string ApiKey { get; set; }

        /// <summary>
        /// Gets or sets the model type to be used for OpenAI services.
        /// </summary>
        public string Type_Model { get; set; }
    }
}
