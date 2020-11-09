using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace MidtermExamUnit
{
    public class DbClass
    {
        static readonly List<string> studentNames = new List<string> { "Archil", "Ana", "Eka", "Giorgi", "Malxaz",
            "Nikoloz", "Ana", "Vaxtang", "Mari", "Rakanishu" };
        static readonly int studentNamesCount = 10;
        static readonly List<Subject> subjects = new List<Subject>()
            {
                new Subject() { SubjectName = "Operating Systems" },
                new Subject() { SubjectName = "Security" },
                new Subject() { SubjectName = "Databases" },
                new Subject() { SubjectName = ".NET" },
                new Subject() { SubjectName = "Web" },
            };
        public static List<Student> CreateStudents()
        {
            List<Student> students = new List<Student>();

            Random randomGenerator = new Random();

            for (int i = 0; i < 50; i++)
            {
                Student student = new Student()
                {
                    StudentName = studentNames[randomGenerator.Next(studentNamesCount)]
                };

                List<SubjectStudent> subjectStudent = new List<SubjectStudent>();

                for (int j = 0; j < 2; j++)
                {
                    int subjectId = randomGenerator.Next(5);
                    for (int k = 0; k < j; k++)
                    {
                        while (subjects[subjectId].SubjectName == subjectStudent[k].Subject.SubjectName)
                        {
                            subjectId = randomGenerator.Next(5);
                            k = 0;
                        }
                    }
                    subjectStudent.Add(new SubjectStudent() { Student = student, Subject = subjects[subjectId] });
                }

                student.Subjects = subjectStudent;
                students.Add(student);
            }

            return students;
        }


        public static IEnumerable<string> ReadCSV(string path)
        {
            IEnumerable<string> data = null;

            try
            {
                data = File.ReadLines(path);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File \"data.csv\" not found!");
                return null;
            }

            return data;
        }


        public static int[] SplitString(string dataIn)
        {
            int[] buffer = { 0, 0, 0 };

            string[] splitted = dataIn.Split(',');
            if (splitted.Length!=3)
            {
                return null;
            }

            try
            {
                buffer[0] = Convert.ToInt32(splitted[0]);
                buffer[1] = Convert.ToInt32(splitted[1]);
                buffer[2] = Convert.ToInt32(splitted[2]);
            }
            catch (FormatException)
            {
                return null;
            }

            return buffer;
        }

    }
}
