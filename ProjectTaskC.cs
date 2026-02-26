using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Tejo_file
{
    /// <summary>
    /// Represents the options to menu.
    /// </summary>
    public enum MenuOption
    {
        /// <summary>
        /// Indicates first option.
        /// </summary>
        A,

        /// <summary>
        /// Indicates second option.
        /// </summary>
        B,

        /// <summary>
        /// Indicates third option.
        /// </summary>
        C
    }

    /// <summary>
    /// Represents a student's name and grade.
    /// </summary>
    public class Student
    {
        /// <summary>
        /// Gets or sets the inputted name of a student.
        /// </summary> 
        /// <value>
        /// A string that contains a student's Name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the inputted grade of a student.
        /// </summary> 
        /// <value>
        /// A floating-point value that represents a student's Grade.
        /// </value>
        public float Grade { get; set; }  
    }

    /// <summary>
    /// Entry point of the program.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that runs the student management menu loop.
        /// </summary>
        /// <remarks>
        /// Uses a boolean variable to control looping program execution,
        /// a list of <see cref="Student"/> objects to store student data,
        /// and parses user input into <see cref="MenuOption"/> values.
        /// </remarks>
        /// <param name="args">
        /// Command-line arguments.
        /// </param>
        static void Main(string[] args)
        {
            bool running = true;
            List<Student> students = new List<Student>();

            while (running)
            {
                Console.WriteLine("\n++++++++++++++++");
                Console.WriteLine("A. Add Student");
                Console.WriteLine("B. Grade Student");
                Console.WriteLine("C. Save Grades");
                Console.WriteLine("++++++++++++++++");
                Console.Write("Enter Choice (A/B/C): ");

                string input = Console.ReadLine().ToUpper();

                if (!Enum.TryParse(input, out MenuOption choice))
                {
                    Console.WriteLine("Invalid choice.");
                    continue;
                }

                switch (choice)
                {
                    case MenuOption.A:
                        Console.WriteLine("Chose Add Student");
                        new ProgramTaskC1().Execute();
                        break;

                    case MenuOption.B:
                        Console.WriteLine("Chose Grade Student");
                        students = new ProgramTaskC2().Execute();
                        break;

                    case MenuOption.C:
                        Console.WriteLine("Chose Save Grades");
                        new ProgramTaskC3().Execute(students);
                        running = false;
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Represents the task executed when user selects
    /// <see cref="MenuOption.A"/> from the menu.
    /// </summary>
    public class ProgramTaskC1
    {
        /// <summary>
        /// Prompts the user to enter five student names, validates the input,
        /// and writes the names to a comma-separated text file.
        /// </summary>
        /// <remarks>
        /// Each name must:
        /// - Be between 1 and 32 characters in length.
        /// - Not be null, empty, or whitespace.
        /// - Contain only letters, spaces, periods, or commas.
        /// 
        /// The file is overwritten if it already exists.
        /// </remarks>
        /// <exception cref="IOException">
        /// Thrown if an I/O error occurs while writing to the file.
        /// </exception>
        public void Execute()
        {
            string path = "Names.txt";

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                for (int i = 0; i < 5; i++)
                {
                    string name;

                    while (true)
                    {
                        Console.Write("Enter a name: ");
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
            Console.WriteLine("Names saved successfully to Names.txt.");
        }
    }

    /// <summary>
    /// Represents the task executed when user selects
    /// <see cref="MenuOption.B"/> from the menu.
    /// </summary>
    public class ProgramTaskC2
    {
        /// <summary>
        /// Reads student names from a text file, prompts the user to enter
        /// grades for each student, validates the input, and returns a sorted list
        /// of students with their assigned grades.
        /// </summary>
        /// <remarks>
        /// The method:
        /// - Reads names from "Names.txt".
        /// - Splits comma-separated values into individual names.
        /// - Sorts the names alphabetically.
        /// - Prompts the user to enter a grade between 50.00 and 100.00.
        /// - Accepts only valid numeric input.
        /// 
        /// Grades are rounded to two decimal places.
        /// </remarks>
        /// <exception cref="IOException">
        /// Thrown if an error occurs while reading the file.
        /// </exception>
        public List<Student> Execute()
        {
            string path = "Names.txt";
            List<Student> students = new List<Student>();

            if (!File.Exists(path))
            {
                Console.WriteLine("File not found.");
                return students;
            }

            string content = File.ReadAllText(path);

            string[] names = content
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(n => n.Trim())
                .ToArray();

            Array.Sort(names);

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

                students.Add(new Student
                {
                    Name = name,
                    Grade = grade
                });
            }
            return students;
        }
    }
   
    /// <summary>
    /// Represents the task executed when user selects
    /// <see cref="MenuOption.C"/> from the menu.
    /// </summary>
    public class ProgramTaskC3
    {
        /// <summary>
        /// Writes the provided student names and grades to a text file,
        /// overwriting any existing file.
        /// </summary>
        /// <remarks>
        /// The method validates that the provided <see cref="Student"/> list
        /// is not null or empty before writing.
        /// 
        /// Each student's name and grade are written in comma-separated format.
        /// </remarks>
        /// <param name="students">
        /// A <see cref="List{T}"/> of <see cref="Student"/> objects containing
        /// student names and their corresponding grades.
        /// </param>
        /// <exception cref="IOException">
        /// Thrown if an I/O error occurs while writing to the file.
        /// </exception>
        public void Execute(List<Student> students)
        {
            string path = "Names.txt"; 

            if (students == null || students.Count == 0)
            {
                Console.WriteLine("No grades to save. Please run Task B first.");
                return;
            }

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.Name},{student.Grade:F2}");
                }
            }
            Console.WriteLine("Grades saved successfully to Names.txt.");
        }
    }
}