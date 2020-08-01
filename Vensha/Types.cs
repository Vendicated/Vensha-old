namespace Vensha.Types
{
    public class Configuration
    {
        public string token { get; set; }
        public string prefix { get; set; }
        public TwitterConfig twitter { get; set; }
    }
    public class TwitterConfig
    {
        public string consumer_key { get; set; }
        public string consumer_secret { get; set; }
        public string access_token { get; set; }
        public string access_token_secret { get; set; }
    }
}