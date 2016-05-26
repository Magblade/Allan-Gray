using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace ConsoleTweetSimulator.Utils
{
    public class StringUtils
    {
        //*******************************************************************
        // Public method: ExtractUserFromString()
        // Arguments: string
        // Returns: string[]
        // Description: Retrive a string array of users from a text file
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        public string[] ExtractUserFromString(string line)
        {
            string[] users;
            users = GetWords(RemoveWord(line));

            return users;
        }

        //*******************************************************************
        // Public method: ExtracFollowersFromString()
        // Arguments: string
        // Returns: string[]
        // Description: Retrive a string array of followers from a text file
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        public string[] ExtracFollowersFromString(string line)
        {
            string[] followers;
            followers = GetWords(RemoveWord(line));

            return followers;
        }
        //*******************************************************************************
        // Public method: ExtractTweetFromString()
        // Arguments: string
        // Returns: string[]
        // Description: Retrive a string array of followers and tweets from a text file
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************************
        public string[] ExtractTweetFromString(string line)
        {
            string[] tweet = new string[2];
            tweet[0] = GetFollower(line);
            tweet[1] = GetTweet(line);

            return tweet;
        }

        //*******************************************************************
        // Public method: GetWords()
        // Arguments: string
        // Returns: string[]
        // Description: Retrive a string array words in a string
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        static string[] GetWords(string input)
        {
            MatchCollection matches = Regex.Matches(input, @"\b[\w']*\b");

            var words = from m in matches.Cast<Match>()
                        where !string.IsNullOrEmpty(m.Value)
                        select TrimSuffix(m.Value);

            return words.ToArray();
        }

        //*******************************************************************
        // Public method: TrimSuffix()
        // Arguments: string
        // Returns: string
        // Description: Remove suffix from word
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        static string TrimSuffix(string word)
        {
            int apostropheLocation = word.IndexOf('\'');
            if (apostropheLocation != -1)
            {
                word = word.Substring(0, apostropheLocation);
            }

            return word;
        }

        //*******************************************************************
        // Public method: RemoveWord()
        // Arguments: string
        // Returns: string
        // Description: Remove the word follows from string
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        static string RemoveWord(string input)
        {
            return input.Replace("follows", "");
        }

        //*******************************************************************
        // Public method: GetFollower()
        // Arguments: string
        // Returns: string
        // Description: Return the sender of the tweet from a string
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        static string GetFollower(string line)
        {
            int apostropheLocation = line.IndexOf('>');
            if (apostropheLocation != -1)
            {
                line = line.Substring(0, apostropheLocation);
            }

            return line;
        }

        //*******************************************************************
        // Public method: GetTweet()
        // Arguments: string
        // Returns: string
        // Description: Return the tweet from a string
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        static string GetTweet(string line)
        {
            int startTweet = line.IndexOf(' ');
            int endTweetTweet = line.IndexOf('.');
            if (startTweet != -1 && endTweetTweet != -1)
            {
                line = line.Substring(startTweet, (endTweetTweet - startTweet)+1);
            }

            return line;

        }

    }
}
