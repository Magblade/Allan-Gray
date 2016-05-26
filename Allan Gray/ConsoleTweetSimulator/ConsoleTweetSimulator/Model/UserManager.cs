using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ConsoleTweetSimulator.Helpers;
using ConsoleTweetSimulator.Utils;
using System.Reflection;

namespace ConsoleTweetSimulator.Model
{
    public class UserManager
    {

        // Set local path for user.txt
        private string path = @"../../Data/user.txt";

        //*****************************************************************
        // Public method: GetTweetFollowersFromStream()
        // Arguments: None
        // Returns: List<Followers>
        // Description: Retrive a generic list of followers from a text file
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        public List<Followers> GetTweetFollowersFromStream()
        {
            // Initilize List to store Followsers
            List<Followers> followerList = null;
            // Initilize instance of Followers
            Followers follower = null;
            // Initilize instance of StringUtils
            StringUtils util = null;  
          
            try
            {
                // Use using StreamReader for disposing.
                using (StreamReader read = new StreamReader(path))
                {
                    followerList = new List<Followers>();
                    util = new StringUtils();
                    
                    // read user.txt line by line until end of file
                    string line;
                    while ((line = read.ReadLine()) != null)
                    {
                        // Extact users from line
                        string[] userNames = util.ExtracFollowersFromString(line);
                        // Extract follows from line
                        string[] followerNames = userNames.Skip(1).ToArray();

                        foreach (string followerName in followerNames)
                        {
                            if (!followerList.Exists(item => item.Follower == followerName && item.Owner == userNames[0]))
                            {
                                // "user" add new owner and follower to followerList
                                follower = new Followers();
                                follower.Owner = userNames[0];
                                follower.Follower = followerName;
                                followerList.Add(follower);
                            }
                        }
                    }

                    // Sort the list of followers alphabetically 
                    followerList = followerList.OrderBy(x => x.Owner).ToList();
                }
            }
            catch (IOException ex) // IO exception ecountered
            {
                Console.WriteLine("HelpLink = {0}", ex.HelpLink);
                Console.WriteLine("Message = {0}", ex.Message);
                Console.WriteLine("Source = {0}", ex.Source);
                Console.WriteLine("StackTrace = {0}", ex.StackTrace);
                Console.WriteLine("TargetSite = {0}", ex.TargetSite);
            }

            // Return all followers stored in followerList
            return followerList;
        }

        //*****************************************************************
        // Public method: GetTweetOwnersFromStream()
        // Arguments: None
        // Returns: List<Owners>
        // Description: Retrive a generic list of owners from a text file
        // Revision: 1.01a
        // Revision date: 2016/05/25 
        // ******************************************************************
        public List<Users> GetTweetOwnersFromStream()
        {
            // Declare new List to store owners.
            List<Users> userList = null;
            // initilize instance of Users
            Users user = null;
            // Initilize instance of StringUtils
            StringUtils util = null;

            try
            {
                // Use using StreamReader for disposing.
                using (StreamReader read = new StreamReader(path))
                {
                    userList = new List<Users>();
                    util = new StringUtils();

                    // read user.txt line by line until end of file
                    string line;
                    while ((line = read.ReadLine()) != null)
                    {
                        // extact users from line
                        string[] userNames = util.ExtractUserFromString(line);
                        foreach (string userName in userNames)
                        {
                            if (!userList.Exists(item => item.Owner == userName))
                            {
                                // "user" add new owner to userList
                                user = new Users();
                                user.Owner = userName;
                                userList.Add(user);
                            }
                        }
                    }

                    // Sort the list of users alphabetically 
                    userList = userList.OrderBy(x => x.Owner).ToList();
                }
            }
            catch (IOException ex) // IO exception ecountered
            {
                Console.WriteLine("HelpLink = {0}", ex.HelpLink);
                Console.WriteLine("Message = {0}", ex.Message);
                Console.WriteLine("Source = {0}", ex.Source);
                Console.WriteLine("StackTrace = {0}", ex.StackTrace);
                Console.WriteLine("TargetSite = {0}", ex.TargetSite);
            }

            // Return all users stored in userList
            return userList;
        }     

    }
}
