using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using ConsoleTweetSimulator.View;

namespace ConsoleTweetSimulator
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initilize instance of TweetsConsole
            TweetsConsole console = new TweetsConsole();

            // Display all tweets from user and their associated followers
            console.ShowAllTweets();
        }    

    }
}
