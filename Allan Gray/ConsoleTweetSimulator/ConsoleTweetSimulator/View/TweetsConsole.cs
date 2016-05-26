using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using ConsoleTweetSimulator.Helpers;
using ConsoleTweetSimulator.Control;

namespace ConsoleTweetSimulator.View
{
    class TweetsConsole
    {
        //****************************************************************************************
        // Public method: ShowAllTweets()
        // Arguments: None
        // Returns: None
        // Description: Display a list to the console of all tweets send tweets of followed tweets 
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ***************************************************************************************
        public void ShowAllTweets()
        {
            // Initilize instance of AllTweets
            List<AllTweets> allTweets = new List<AllTweets>();
            // Initilize instance of TweetHandler
            TweetHandler tweetHandler = new TweetHandler();

            // Get a List of all the tweets
            allTweets = tweetHandler.GetAllTweets();

            try
            {
                // Iterate through all the tweets stored in AllTweets
                foreach (AllTweets tweets in allTweets)
                {
                    // Output the user name to the console
                    Console.WriteLine(tweets.Owner);
                    Console.WriteLine();

                    // Check if the any tweets for this user
                    if (tweets.Tweet.Count > 0)
                    {
                        // Output all tweets associated with this user
                        for (int tweetCount = 0; tweetCount < tweets.Tweet.Count; tweetCount++)
                        {
                            Console.WriteLine(tweets.Tweet.ElementAt(tweetCount));
                            Console.WriteLine();
                        }
                    }
                }
            }
            catch (Exception ex) // Exception ecountered
            {
                Console.WriteLine("HelpLink = {0}", ex.HelpLink);
                Console.WriteLine("Message = {0}", ex.Message);
                Console.WriteLine("Source = {0}", ex.Source);
                Console.WriteLine("StackTrace = {0}", ex.StackTrace);
                Console.WriteLine("TargetSite = {0}", ex.TargetSite);
            }

            // After outputing the tweets ask user to press a key to exit console application
            Console.WriteLine();
            Console.WriteLine("Press any key to exit");
            Console.ReadKey();
        }

    }
}
