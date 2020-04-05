using System;

namespace Server
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "Dimension Tension server";
            Server.Start(2, 26950);
            Console.ReadKey();
        }
    }
}
