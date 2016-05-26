using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Reflection;

using ConsoleTweetSimulator.Helpers;
using ConsoleTweetSimulator.Utils;


namespace ConsoleTweetSimulator.Model
{
    public class TweetManager
    {
        // Set local path for tweet.txt
        private string path = @"../../Data/tweet.txt";

        //*****************************************************************
        // Public method: GetTweetsByOwnerFromStream()
        // Arguments: None
        // Returns: List<Tweets>
        // Description: Retrive a generic list of followers from a text file
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        public List<Tweets> GetTweetsByOwnerFromStream()
        {
            // Declare new List to store tweets from followers.
            List<Tweets> tweetList = null;
            // initilize instance of Tweets
            Tweets tweet = null;
            // Initilize instance of StringUtils
            StringUtils util = null;

            try
            {
                // Use using StreamReader for disposing.
                using (StreamReader read = new StreamReader(path))
                {
                    tweetList = new List<Tweets>();
                    util = new StringUtils();

                    // read user.txt line by line until end of file
                    string line;
                    while ((line = read.ReadLine()) != null)
                    {
                        // extact users from line
                        string[] ownerNames = util.ExtractTweetFromString(line);
                        string[] tweetMessage = ownerNames.Skip(1).ToArray();

                        foreach (string theTweet in tweetMessage)
                        {
                            // Check that the legth of the tweet is less that 141 bytes
                            if (theTweet.Length > 140)
                            {
                                // Rais a custom exception
                                throw new Exception("Tweet length excceeds 140 charaters");
                            }
                            else
                            {
                                // "user" add new owner and follower to followerList
                                tweet = new Tweets();
                                tweet.Owner = ownerNames[0];
                                tweet.Tweet = theTweet;
                                tweetList.Add(tweet);
                            }
                        }
                    }

                }
            }
            catch (IOException ex) // IO error ecountered
            {
                Console.WriteLine("HelpLink = {0}", ex.HelpLink);
                Console.WriteLine("Message = {0}", ex.Message);
                Console.WriteLine("Source = {0}", ex.Source);
                Console.WriteLine("StackTrace = {0}", ex.StackTrace);
                Console.WriteLine("TargetSite = {0}", ex.TargetSite);
            }

            return tweetList;
        }
    }
}
