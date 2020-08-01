using CoreTweet;

namespace Vensha.Handlers
{
    public class TwitterHandler
    {
        private Tokens client;

        public TwitterHandler(Types.Configuration config)
        {
            client = Tokens.Create(
                config.twitter.consumer_key,
                config.twitter.consumer_secret,
                config.twitter.access_token,
                config.twitter.access_token_secret
            );
        }
    }
}