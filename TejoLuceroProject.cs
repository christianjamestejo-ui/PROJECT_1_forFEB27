using System;
using System.Collections.Generic;
using System.Globalization;

namespace TejoLuceroProject
{
    /// <summary>
    /// Enumeration representing menu options for the program.
    /// </summary>
    public enum MenuOption
    {
        A,
        B,
        C
    }

    class Program
    {

        static void Main(string[] args)
        {
            bool running = true;
            List<Student> students = new List<Student>();
            string filePath = "Names.txt";

            while (running)
            {
                Console.WriteLine("\n++++++++++++++++\n");
                Console.WriteLine("A. Task A - Add Data\n");
                Console.WriteLine("B. Task B - Grade Numbers\n");
                Console.WriteLine("C. Task C - Student Management\n");
                Console.WriteLine("Q. Quit\n");
                Console.WriteLine("++++++++++++++++\n");
                Console.Write("Enter Choice (A/B/C/Q): ");

                string input = Console.ReadLine().ToUpper();

                switch (input)
                {
                    case "A":
                        Console.WriteLine("Executing Task A...");
                        new ProgramTaskA().Execute(); // Assuming ProgramTaskA has parameterless Execute()
                        break;

                    case "B":
                        Console.WriteLine("Executing Task B...");
                        new ProgramTaskB().Execute(); // ProgramTaskB.Execute is static
                        break;

                    case "C":
                        Console.WriteLine("Executing Task C...");
                        new ProgramTaskC(filePath).Execute(); // Pass file path
                        break;

                    case "Q":
                        running = false;
                        Console.WriteLine("Exiting program.");
                        break;

                    default:
                        Console.WriteLine("Invalid choice. Please enter A, B, C, or Q.");
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Program that generates random numbers and displays repeated values.
    /// </summary>
    class ProgramTaskA
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        public void Execute()
        {
            const int SIZE = 10;
            List<int> numbers = new List<int>();
            Random random = new Random();

            /// <remarks>
            /// Generates random numbers from 1 to 10 and stores them in a list.
            /// </remarks>
            for (int i = 0; i < SIZE; i++)
            {
                numbers.Add(random.Next(1, 11));
            }

            Console.WriteLine("Generated Numbers:");
            foreach (int num in numbers)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine("\n\nRepeated Numbers:");

            List<int> counts = new List<int>();

            for (int i = 0; i <= 10; i++)
            {
                counts.Add(0);
            }

            foreach (int num in numbers)
            {
                counts[num]++;
            }

            bool hasRepeats = false;

            for (int i = 1; i <= 10; i++)
            {
                if (counts[i] > 1)
                {
                    Console.WriteLine($"{(NumberValue)i} appears {counts[i]} times");
                    hasRepeats = true;
                }
            }

            if (!hasRepeats)
            {
                Console.WriteLine("No repeated numbers.");
            }
        }
    }

    /// <summary>
    /// Enumeration representing number values from One to Ten.
    /// </summary>
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
}

    /// <summary>
    /// Program that accepts numerical input, computes the average,
    /// and determines whether the result is Passed or Failed.
    /// </summary>
    class ProgramTaskB
    {
        /// <summary>
        /// Entry point of the program.
        /// </summary>
        /// <param name="args">
        /// Command-line arguments.
        /// </param>
        /// <remarks>
        /// Prompts the user to enter ten numbers between 50 and 100,
        /// calculates their average, and evaluates the result based
        /// on the defined passing criteria.
        /// </remarks>
        public void Execute()
        {
            const int SIZE = 10;
            List<int> numbers = new List<int>();
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
                            numbers.Add(input);
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
            AverageResult result = average >= 75 ? AverageResult.Passed : AverageResult.Failed;

            Console.WriteLine("\nNumbers Entered:");
            foreach (int num in numbers)
            {
                Console.Write(num + " ");
            }

            Console.WriteLine($"\n\nAverage: {average:F2}");
            Console.WriteLine($"Result: {result}");
        }
    }

    /// <summary>
    /// Represents the possible results of the average computation.
    /// </summary>
    public enum AverageResult
    {
        Failed,   // 50–74
        Passed    // 75–100
    }



    /// <summary>
    /// Represents a student with a name and a grade.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets the student's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the student's grade.
        /// </summary>
        public float Grade { get; set; }
    }

    /// <summary>
    /// Handles the process of adding student names, recording their grades,
    /// and saving the information to a text file.
    /// </summary>
    public class ProgramTaskC
    {
        private readonly string _filePath;

        /// <summary>
        /// Initializes a new instance of <see cref="ProgramTaskC"/> with the specified file path.
        /// </summary>
        /// <param name="filePath">The path of the file to store student names and grades.</param>
        public ProgramTaskC(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Executes the student management workflow:
        /// adds names, records grades, and saves to the file.
        /// </summary>
        /// <exception cref="IOException">Thrown when reading or writing the file fails.</exception>
        public void Execute()
        {
            List<Student> students = new List<Student>();

            using (StreamWriter writer = new StreamWriter(_filePath, false))
            {
                for (int i = 0; i < 5; i++)
                {
                    string name;
                    while (true)
                    {
                        Console.Write($"Enter name {i + 1}: ");
                        name = Console.ReadLine();

                        if (string.IsNullOrWhiteSpace(name))
                        {
                            Console.WriteLine("Name cannot be empty.");
                            continue;
                        }

                        if (name.Length < 1 || name.Length > 32)
                        {
                            Console.WriteLine("Name must be between 1 and 32 characters.");
                            continue;
                        }

                        if (!name.All(c => char.IsLetter(c) || c == ' ' || c == '.' || c == ','))
                        {
                            Console.WriteLine("Invalid input. Only letters, spaces, periods, and commas are allowed.");
                            continue;
                        }
                        break;
                    }

                    writer.Write($"{name},");
                }
            }

            Console.WriteLine("\nNames saved successfully:");
            Console.WriteLine(File.ReadAllText(_filePath));

            string content = File.ReadAllText(_filePath);
            List<string> names = content
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim())
                .ToList();

            names.Sort();

            Console.WriteLine("\nEnter grades for each student:");
            foreach (string name in names)
            {
                float grade;
                while (true)
                {
                    Console.Write($"Enter grade for {name} (50.00 - 100.00): ");
                    string input = Console.ReadLine();

                    if (float.TryParse(input, NumberStyles.Float, CultureInfo.InvariantCulture, out grade))
                    {
                        grade = (float)Math.Round(grade, 2);

                        if (grade >= 50 && grade <= 100)
                            break;
                        else
                            Console.WriteLine("Invalid grade. Must be between 50.00 and 100.00.");
                    }
                    else
                    {
                        Console.WriteLine("Invalid input. Enter a numeric grade.");
                    }
                }

                students.Add(new Student { Name = name, Grade = grade });
            }

            Console.WriteLine("\nStudents with grades:");
            foreach (var student in students)
            {
                Console.WriteLine($"{student.Name}, {student.Grade:F2}");
            }

            using (StreamWriter writer = new StreamWriter(_filePath, false))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.Name}, {student.Grade:F2}");
                }
            }

            Console.WriteLine("\nGrades saved successfully to Names.txt.");
        }
    }
}
