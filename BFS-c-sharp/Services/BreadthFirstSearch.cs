using BFS_c_sharp.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BFS_c_sharp.Services
{
    class BreadthFirstSearch
    {


        public int FindMinimumDistance(UserNode user1, UserNode user2)
        {
            List<int> distances = new List<int>();

            int distance = 0;
            bool pathFound = false;
            HashSet<UserNode> usersToCheck = user1.Friends;

            while (!pathFound)
            {
                pathFound = CheckNextLevelFriends(usersToCheck, user2);
                distance += 1;
                HashSet<UserNode> tempUsersToCheck = new HashSet<UserNode>();
                foreach (UserNode user in usersToCheck)
                {
                    tempUsersToCheck.UnionWith(user.Friends);
                }
                usersToCheck = tempUsersToCheck;
            }

            return distance;
        }

        public bool CheckNextLevelFriends(HashSet<UserNode> usersToCheck, UserNode user2)
        {

            foreach (UserNode friend in usersToCheck)
            {

                if (friend == user2)
                {
                    return true;
                }
            }
            return false;
        }

        public HashSet<UserNode> ListFriendsOfFriends(UserNode user, int distance)
        {
            HashSet<UserNode> listOfFriendsOfFriends = new HashSet<UserNode>();

            HashSet<UserNode> userListToCheck = new HashSet<UserNode>();

            foreach (UserNode friendOfOriginal in user.Friends)
            {
                userListToCheck.Add(friendOfOriginal);
            }

            while (distance > 0)
            {
                HashSet<UserNode> nextLevelFriends = new HashSet<UserNode>();
                foreach (UserNode usertoCheck in userListToCheck)
                {
                    AddFriendsOfFriends(usertoCheck, ref nextLevelFriends, user);
                }
                distance -= 1;
                userListToCheck.Clear();
                foreach (UserNode userChecked in nextLevelFriends)
                {
                    userListToCheck.UnionWith(userChecked.Friends);
                }

                listOfFriendsOfFriends.UnionWith(nextLevelFriends);

            }
            return listOfFriendsOfFriends;
        }

        private void AddFriendsOfFriends(UserNode friend, ref HashSet<UserNode> nextLevelFriends, UserNode originalUser)
        {
            foreach (UserNode remoteFriend in friend.Friends)
            {
                if (remoteFriend != originalUser && !originalUser.Friends.Contains(remoteFriend)) 
                {
                    nextLevelFriends.Add(remoteFriend);
                }
            }
        }

        public List<List<UserNode>> ListShortestPaths(UserNode user1, UserNode user2)
        {
            List<List<UserNode>> listShortestPaths = new List<List<UserNode>>();

            int minimumDistance = FindMinimumDistance(user1, user2);

            List<List<UserNode>> growingPaths = new List<List<UserNode>>();
            
            foreach(UserNode friend in user1.Friends)
            {
                List<UserNode> tempPath =  new List<UserNode> { user1, friend };
                growingPaths.Add(tempPath);
            }


            while (minimumDistance>1)
            {

                List<List<UserNode>> longerPathList = new List<List<UserNode>>();
                foreach(List<UserNode> path in growingPaths)
                {
                longerPathList.AddRange(ListNextLevelPaths(path));
                }
                growingPaths.Clear();
                growingPaths.AddRange(longerPathList);

                minimumDistance -= 1;
            }

            listShortestPaths = selectRelevantPaths(user1, user2, growingPaths);
            return listShortestPaths;
        }

        private List<List<UserNode>> selectRelevantPaths(UserNode user1, UserNode user2, List<List<UserNode>> allPaths)
        {
            List<List<UserNode>> listShortestPaths = new List<List<UserNode>>();
            foreach (List<UserNode> path in allPaths)
            {
                if (path.Contains(user1) && path.Contains(user2))
                {
                    listShortestPaths.Add(path);
                }
            }
            return listShortestPaths;
        }

        public List<List<UserNode>> ListNextLevelPaths(List<UserNode> userPath)
        {
            List<List<UserNode>> paths = new List<List<UserNode>>();
            List<UserNode> friendList = userPath[userPath.Count - 1].Friends.ToList();
            foreach (UserNode friend in friendList)
            {
                List<UserNode> longerPath = new List<UserNode>();
                longerPath.AddRange(userPath);
                longerPath.Add(friend);
                paths.Add(longerPath);
            }
            return paths;
        }
    
    }

}
