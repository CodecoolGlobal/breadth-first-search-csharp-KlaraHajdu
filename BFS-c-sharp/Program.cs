using BFS_c_sharp.Model;
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
            Console.ReadKey();
        }
    }
}
