using CoreTweet;
using System;
using Newtonsoft.Json;
using System.IO;

namespace Vensha.Handlers
{
    public class TwitterHandler
    {
        private Tokens client;

        public TwitterHandler(Types.Configuration config)
        {

            if (String.IsNullOrEmpty(config.twitter.access_token))
            {
                var session = OAuth.Authorize(config.twitter.consumer_key, config.twitter.consumer_secret);

                Console.WriteLine($"Looks like this is your first time using this application! Pleae open the following link and authorise, then paste the obtained token here: {session.AuthorizeUri.AbsoluteUri}");
                string token = Console.ReadLine();

                try
                {

                    client = OAuth.GetTokens(session, token);
                    config.twitter.access_token = client.AccessToken;
                    config.twitter.access_token_secret = client.AccessTokenSecret;
                    System.IO.File.WriteAllText(Directory.GetCurrentDirectory() + "/config.json", JsonConvert.SerializeObject(config, Formatting.Indented));
                }
                catch
                {
                    Console.WriteLine("That was not a valid token!");
                    Environment.Exit(1);
                }
            }
            else
            {
                client = Tokens.Create(config.twitter.consumer_key, config.twitter.consumer_secret, config.twitter.access_token, config.twitter.access_token_secret);
            }

            client.Statuses.Update(new { status = "Test" + new Random().Next(1, 100) });
        }
    }
}