namespace Cardcraft.Microservice.aCore
{
    public class APP_SETTINGS
    {
        public string AWS_COGNITOS_CLIENT_ID { get; set; }
        public string AWS_COGNITOS_REGION_ENDPOINT { get; set; }
        public string AWS_SECRET_KEY { get; set; }
        public string AWS_ACCESS_KEY_ID { get; set; }
        public string AWS_COGNITOS_USERPOOL_ID { get; set; }
    }

    public static class KEY
    {
        public const string AWS_COGNITOS_REGION_ENDPOINT = "AWS_COGNITOS_REGION_ENDPOINT";
        public const string AWS_COGNITOS_CLIENT_ID = "AWS_COGNITOS_CLIENT_ID";
        public const string AWS_SECRET_KEY = "AWS_SECRET_KEY";
        public const string AWS_ACCESS_KEY_ID = "AWS_ACCESS_KEY_ID";
        public const string AWS_COGNITOS_USERPOOL_ID = "AWS_COGNITOS_USERPOOL_ID";
        public const string DEFAULT_ERROR_MESSAGE = "DEFAULT_ERROR_MESSAGE";
        public const string AWS_PROFILE = "AWS_PROFILE";
        public const string AWS_REGION = "AWS_REGION";
    }
}
