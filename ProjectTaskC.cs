using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Globalization;

namespace Tejo_file
{
    public enum MenuOption
    {
        A,
        B,
        C
    }

    public class Student
    {
        public string Name { get; set; }
        public float Grade { get; set; }  
    }

    class Program
    {
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

    public class ProgramTaskC1
    {
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

                        if (name.Length < 1 || name.Length > 64)
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

    public class ProgramTaskC2
    {
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

    public class ProgramTaskC3
    {
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