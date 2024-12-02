namespace MsOpenIA.DTO
{
    using System.Net;

    public class ResponseDto
    {
        private const string DocsErrors = "https://github.com/openai/openai-dotnet?tab=readme-ov-file#getting-started";

        public ResponseDto()
        {
            Status = HttpStatusCode.NoContent;
            Message = string.Empty;
            Data = null;
            Info = DocsErrors;
        }

        public ResponseDto(HttpStatusCode status, string message, object data)
        {
            Status = status;
            Message = message;
            Data = data;
            Info = DocsErrors;
        }

        public ResponseDto(HttpStatusCode _status, string _message, string _info)
        {
            Status = _status;
            Message = _message;
            Data = null;
            Info = _info;
        }

        public HttpStatusCode Status { get; set; }

        public string Message { get; set; }

        public dynamic? Data { get; set; }

        public string Info { get; set; }

        public class OpenAIError
        {
            public int StatusCode { get; set; }

            public string ErrorCode { get; set; }

            public string Message { get; set; }

            public OpenAIError(int statusCode, string errorCode, string message)
            {
                StatusCode = statusCode;
                ErrorCode = errorCode;
                Message = message;
            }

            public static class OpenAIErrorCodes
            {
                public static OpenAIError InvalidAuthentication = new(401, "invalid_authentication", "Invalid Authentication");

                public static OpenAIError IncorrectApiKey = new(401, "incorrect_api_key", "Incorrect API key provided");

                public static OpenAIError NotMemberOfOrganization = new(401, "not_member_of_organization", "You must be a member of an organization to use the API");

                public static OpenAIError UnsupportedRegion = new(403, "unsupported_region", "Country, region, or territory not supported");

                public static OpenAIError RateLimitReached = new(429, "rate_limit_reached", "Rate limit reached for requests");

                public static OpenAIError QuotaExceeded = new(429, "quota_exceeded", "You exceeded your current quota, please check your plan and billing details");

                public static OpenAIError ServerError = new(500, "server_error", "The server had an error while processing your request");

                public static OpenAIError EngineOverloaded = new(503, "engine_overloaded", "The engine is currently overloaded, please try again later");
            }
        }
    }
}
