using System;
using System.Collections.Generic;
using System.IO;

namespace Tejo_file
{
    public enum MenuOption
    {
        TaskC1 = 1,
        TaskC2,
        TaskC3
    }

    public class Student
    {
        public string Name { get; set; }
        public int Grade { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;
            List<Student> students = new List<Student>();

            Console.WriteLine("\n++++++++++++++++\n" +
                             "1. Add Student  \n" +
                             "2. Grade Student\n" +
                             "3. Save Grades  \n" +
                             "++++++++++++++++\n");
            Console.WriteLine("Enter Choice:");

            int input = int.Parse(Console.ReadLine());
            MenuOption choice = (MenuOption)input;

            while (running)
            switch (choice)
                {
                    case MenuOption.TaskC1:
                        Console.WriteLine("Chose Add student");
                        ProgramTaskC1 task1 = new ProgramTaskC1();
                        task1.Execute();
                        break;
                    case MenuOption.TaskC2:
                        Console.WriteLine("Chose Grade Student");
                        ProgramTaskC2 task2 = new ProgramTaskC2();
                        students = task2.Execute();
                        break;
                    case MenuOption.TaskC3:
                        Console.WriteLine("Chose Save Grades");
                        ProgramTaskC3 task3 = new ProgramTaskC3();
                        task3.Execute(students);
                        running = false;
                        break;
                }
        }
    }

    public class ProgramTaskC1
    {
        public void Execute()
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
                Console.Write($"Enter grade for {name}: ");

                int grade;
                while (!int.TryParse(Console.ReadLine(), out grade))
                {
                    Console.Write("Invalid grade. Enter a number: ");
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

            using (StreamWriter writer = new StreamWriter(path, false))
            {
                foreach (var student in students)
                {
                    writer.WriteLine($"{student.Name},{student.Grade}");
                }
            }

            Console.WriteLine("File created successfully.");
        }
    }
}