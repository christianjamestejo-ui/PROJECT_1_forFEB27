using System;

namespace Tejo_File_B
{
    public enum AverageResult
    {
        Failed,   // 50–74
        Passed    // 75–100
    }

    class Program
    {
        static void Main()
        {
            const int SIZE = 10;
            int[] numbers = new int[SIZE];
            int sum = 0;

            Console.WriteLine("Enter 10 numbers between 50 and 100:");

            for (int i = 0; i < SIZE; i++)
            {
                while (true)
                {
                    Console.Write($"Number {i + 1}: ");
                    if (int.TryParse(Console.ReadLine(), out int input))
                    {
                        if (input >= 50 && input <= 100)
                        {
                            numbers[i] = input;
                            sum += input;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Value must be between 50 and 100.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter a number.");
                    }
                }
            }

            double average = (double)sum / SIZE;

            AverageResult result;

            if (average >= 75)
                result = AverageResult.Passed;
            else
                result = AverageResult.Failed;

            Console.WriteLine("\nNumbers Entered:");
            foreach (int num in numbers)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine($"\n\nAverage: {average:F2}");
            Console.WriteLine($"Result: {result}");
        }
    }
}