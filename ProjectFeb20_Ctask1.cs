using System;

namespace Tejo_file
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            string givenName;
            while (running)
            {
                try
                {
                    for (int i = 0; i < 5; i++)
                    {
                        Console.WriteLine("Enter a name:");
                        givenName = Console.ReadLine();

                        string path = "Names.txt";
                        using (StreamWriter writer = new StreamWriter(path, true))
                        {
                            writer.Write($"{givenName}, ");
                            running = false;
                        }
                    }
                }
                catch
                {
                    Console.WriteLine("Invalid character, input Name");
                }
            }
        }
    }
}