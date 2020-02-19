using BFS_c_sharp.Model;
using BFS_c_sharp.Services;
using System;
using System.Collections.Generic;

namespace BFS_c_sharp
{
    class Program
    {
        static void Main(string[] args)
        {
            RandomDataGenerator generator = new RandomDataGenerator();
            List<UserNode> users = generator.Generate();

            foreach (var user in users)
            {
                Console.WriteLine(user);
            }

            Console.WriteLine("Done");

            BreadthFirstSearch search = new BreadthFirstSearch();
            UserNode test1 = users[0];
            UserNode test2 = users[2];
            UserNode test3 = users[6];
            UserNode test4 = users[9];
            int minDistance = search.FindMinimumDistance(test3, test4);
            Console.WriteLine("The mininmum distance between " + test3 + " and " + test4 + " is " + minDistance);
            int selectedDistance = 3;
            Console.WriteLine("The user " + test1 + " has the following friends of friends in " + selectedDistance + " level distance.");
            HashSet<UserNode> friendsOfFriends = search.ListFriendsOfFriends(test1, selectedDistance);
            foreach (var user in friendsOfFriends)
            {
                Console.WriteLine(user);
            }
            Console.WriteLine("The " + test3 + " and " + test4 + "has the following shortest paths: ");
            List<List<UserNode>> shortestPaths = search.ListShortestPaths(test3, test4);
            foreach(var path in shortestPaths)
            {
                foreach(UserNode node in path)
                {
                    Console.Write(node.ToString());
                }
                Console.WriteLine("End of path");
            }
            Console.WriteLine();
            Console.ReadKey();
        }
    }
}
