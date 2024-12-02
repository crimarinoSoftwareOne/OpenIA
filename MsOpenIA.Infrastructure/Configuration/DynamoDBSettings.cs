namespace MsOpenIA.Infrastructure.Configuration
{
    public class DynamoDbSettings
    {
        public string TableName { get; set; }
    }

    public class AWSSettings
    {
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }
        public string Region { get; set; }
    }
}
