using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConsoleTweetSimulator.Helpers;
using ConsoleTweetSimulator.Model;

namespace ConsoleTweetSimulator.Control
{
    public class TweetHandler
    {
        //*******************************************************************
        // Public method: GetAllTweets()
        // Arguments: None
        // Returns: List<AllTweets>
        // Description: Retrive a generic list of all tweets from a text file
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        public List<AllTweets> GetAllTweets()
        {
            // Initilize List to store Users
            List<Users> usersList = new List<Users>();
            // Initilize List to store Followsers
            List<Followers> followersList = new List<Followers>();
            // Initilize List to store Tweets
            List<Tweets> tweetsList = new List<Tweets>();
            // Initilize List to store AllTweets
            List<AllTweets> allTweetsList = new List<AllTweets>();

            // Initilize instance of UserManager
            UserManager userManager = new UserManager();
            // Initilize instance of TweetManager
            TweetManager tweetManager = new TweetManager();
            // Initilize instance of AllTweets
            AllTweets allTweets = null;

            try
            {
                // Get a list of all users from user.txt file
                usersList = userManager.GetTweetOwnersFromStream();
                // Get a list of all followers from user.txt file
                followersList = userManager.GetTweetFollowersFromStream();
                // Get list of all tweets from tweet.txt file
                tweetsList = tweetManager.GetTweetsByOwnerFromStream();

                // Iterate through all user in usersList
                foreach (Users user in usersList)
                {
                    // Initilize instance of AllTweets
                    allTweets = new AllTweets();
                    // Store sender of tweet
                    allTweets.Owner = user.Owner;
                    // Initilize List to store all tweets from followers
                    allTweets.Tweet = new List<string>();

                    // Check if user has any followers
                    var hasFollowers = followersList.Find(item => item.Owner == user.Owner);

                    // Validate if follows is not null
                    if (hasFollowers != null)
                    {
                        // Check if user has any tweets from followers
                        var hasTweets = tweetsList.Find(item => item.Owner == hasFollowers.Owner);

                        // Validate if tweets is not null
                        if (hasTweets != null)
                        {

                            // Iterate through all the tweets stred in tweetsList
                            foreach (Tweets tweets in tweetsList)
                            {
                                // Check is there are any tweets from sender
                                if (user.Owner == tweets.Owner)
                                {
                                    // Add tweets from sender to allTweets List
                                    allTweets.Tweet.Add("\t@" + tweets.Owner + ": " + tweets.Tweet);
                                }// Check is there are any tweets from gollowers
                                else if (tweets.Owner == hasFollowers.Follower)
                                {
                                    // Add tweets from followers to allTweets List
                                    allTweets.Tweet.Add("\t@" + tweets.Owner + ": " + tweets.Tweet);
                                }

                            }
                        }

                    }

                    // Add tweet transation to allTweetsList
                    allTweetsList.Add(allTweets);

                }
            }catch(Exception ex)
            {
                Console.WriteLine("HelpLink = {0}", ex.HelpLink);
                Console.WriteLine("Message = {0}", ex.Message);
                Console.WriteLine("Source = {0}", ex.Source);
                Console.WriteLine("StackTrace = {0}", ex.StackTrace);
                Console.WriteLine("TargetSite = {0}", ex.TargetSite);
            }

            // Return all tweets stored in allTweetsList
            return allTweetsList;
        }
    }
}
