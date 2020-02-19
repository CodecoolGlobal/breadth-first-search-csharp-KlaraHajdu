using System;
using System.Collections.Generic;
using BFS_c_sharp.Model;

namespace BFS_c_sharp
{
    public class RandomDataGenerator
    {
        private Random rng = new Random(1234); // ez teljesen determinisztikussá teszi?!?!?
        //private Random rng = new Random();
        private String[] firstNames = {
            "Inez", "Emery", "Virginia", "Charissa", "Tyrone", "Ayanna", "Jena", "Ora",
            "Cooper", "Gareth", "Karleigh", "Aladdin", "Arden", "Pearl", "Mariko", "Hadley",
            "Tanya", "Madeline", "Naomi", "Maggie", "Kerry", "Elmo", "Wylie", "Alec",
            "Axel", "Belle", "Cally", "Theodore", "Emmanuel", "Christopher", "Olympia"};

        private String[] lastNames =  {
            "Winifred", "Tanner", "Rajah", "Cedric", "Tyler", "Nicholas", "Abra", "Aurora",
            "Bryar", "Kibo", "Myles", "Hillary", "Lydia", "Dolan", "Lucian", "Prescott"
        };

        public List<UserNode> Generate()
        {
            List<UserNode> users = new List<UserNode>();
            UserNode firstUser = GenerateNewUser();
            users.Add(firstUser);
            // first generate and connect users in a star shaped tree
            GenerateTree(firstUser, users, 4);

            for (int i = 0; i < users.Count - 30; i++)
            {
                if (i % 5 == 0)
                {
                    var friendUser = users[i + 30];
                    users[i].AddFriend(friendUser);
                }
            }

            return users;
        }

        private void GenerateTree(UserNode user, List<UserNode> allUsers, int depth)
        {
            if (depth == 0)
            {
                return;
            }
            for (int i = 0; i < depth; i++)
            {
                UserNode newUser = GenerateNewUser();
                user.AddFriend(newUser);
                allUsers.Add(newUser);
                GenerateTree(newUser, allUsers, depth - 1);
            }
        }

        private UserNode GenerateNewUser()
        {
            return new UserNode(GetRandomElement(firstNames), GetRandomElement(lastNames));
        }

        private string GetRandomElement(string[] array)
        {
            return array[rng.Next(array.Length)];
        }        
    }
}