using System;

namespace Tejo_File_A
{
    public enum NumberValue
    {
        One = 1,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        Eight,
        Nine,
        Ten
    }

    class Program
    {
        static void Main()
        {
            const int SIZE = 10;
            int[] numbers = new int[SIZE];
            Random random = new Random();

            for (int i = 0; i < SIZE; i++)
            {
                numbers[i] = random.Next(1, 11);
            }

            Console.WriteLine("Generated Numbers:");
            foreach (int num in numbers)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine("\n\nRepeated Numbers:");

            int[] counts = new int[11];

            foreach (int num in numbers)
            {
                counts[num]++;
            }

            bool hasRepeats = false;

            for (int i = 1; i <= 10; i++)
            {
                if (counts[i] > 1)
                {
                    NumberValue numberName = (NumberValue)i;
                    Console.WriteLine($"{numberName} appears {counts[i]} times");
                    hasRepeats = true;
                }
            }

            if (!hasRepeats)
            {
                Console.WriteLine("No repeated numbers.");
            }
        }
    }
}