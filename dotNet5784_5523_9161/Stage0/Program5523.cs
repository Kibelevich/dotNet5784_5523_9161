using System;
namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome5523();
            Welcome9161();
            Console.ReadKey();
        }

        private static void Welcome5523()
        {
            Console.WriteLine("Enter your name:");
            string name = Console.ReadLine();
            Console.WriteLine("{0}, welcome to my first console application");
        }
        static partial void Welcome9161();

    }
}
