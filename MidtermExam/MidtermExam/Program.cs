using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace MidtermExam
{
    class Program
    {

        static List<string> studentNames = new List<string> { "Archil", "Ana", "Eka", "Giorgi", "Malxaz",
            "Nikoloz", "Ana", "Vaxtang", "Mari", "Rakanishu" };
        static readonly int studentNamesCount = 10;

        static void Main(string[] args)
        {
            HasData();

            string path = @"data.csv";
            IEnumerable<string> data;

            try
            {
                data = File.ReadLines(path);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("File \"data.csv\" not found!");
                return;
            }

            int countCurrent = 0, countGood = 0, countBad = 0;

            using (BloggingDbContext db = new BloggingDbContext())
            {
                foreach (var item in data)
                {
                    countCurrent++;

                    if ((item == "StudentId,SubjectId,Point") || (item == ""))
                    {
                        continue;
                    }

                    int studentId, subjectId, point;
                    string[] splitted = item.Split(',');

                    if (splitted.Length != 3)
                    {
                        //Console.WriteLine("Error!\n" +
                        //    "Parametres must be 3.\n" +
                        //    "Check line " + Convert.ToString(countCurrent) + '\n' + item);
                        return;
                    }

                    try
                    {
                        studentId = Convert.ToInt32(splitted[0]);
                        subjectId = Convert.ToInt32(splitted[1]);
                        point = Convert.ToInt32(splitted[2]);

                        if (!(point >= 0 && point <= 100))
                        {
                            //Console.WriteLine("Ignored!\t" +
                            //    "Point is " + point + ", is it real?");
                            countBad++;
                            continue;
                        }

                        SubjectStudent subjectStudent = new SubjectStudent();
                        subjectStudent = (from st in db.SubjectStudents
                                          where st.StudentId == studentId
                                          where st.SubjectId == subjectId
                                          select st).FirstOrDefault();

                        if (subjectStudent == null)
                        {
                            //Console.WriteLine("Ignored!\t" +
                            //    "Can't find student with ID " + studentId + " and subject ID " + subjectId);
                            countBad++;
                            continue;
                        }

                        subjectStudent.Point = point;
                        db.SaveChanges();
                        countGood++;
                    }
                    catch (FormatException)
                    {
                        //Console.WriteLine("Error!\n" +
                        //    "Can't understand format.\n" +
                        //    "Check line " + Convert.ToString(countCurrent) + '\n' + item);
                        return;
                    }

                }
            }

            Console.WriteLine("\nImported " + countGood + " items.\n" +
                "Failed to import " + countBad + " items.");

        }


        static void HasData()
        {
            List<Subject> subjects = new List<Subject>()
            {
                new Subject() { SubjectName = "Operating Systems" },
                new Subject() { SubjectName = "Security" },
                new Subject() { SubjectName = "Databases" },
                new Subject() { SubjectName = ".NET" },
                new Subject() { SubjectName = "Web" },
            };

            Random randomGenerator = new Random();

            List<Student> students = new List<Student>();

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

            using (BloggingDbContext db = new BloggingDbContext())
            {
                foreach (var student in students)
                {
                    db.Students.Add(student);
                }

                db.SaveChanges();
            }
        }


    }


    public class BloggingDbContext : DbContext
    {
        public DbSet<Student> Students { set; get; }

        public DbSet<SubjectStudent> SubjectStudents { set; get; }

        public DbSet<Subject> Subjects { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=RGN7_64-PC\\SQLEXPRESS;database=TSU_DB_local;Integrated security=true;");
        }
    }

    public class Student
    {
        public int StudentId { set; get; }

        public string StudentName { set; get; }

        public List<SubjectStudent> Subjects { set; get; }

    }

    public class Subject
    {
        public int SubjectId { set; get; }

        public string SubjectName { set; get; }

        public List<SubjectStudent> Students { set; get; }

    }

    public class SubjectStudent
    {
        public int SubjectStudentId { set; get; }

        public int StudentId { set; get; }

        public virtual Student Student { set; get; }

        public int SubjectId { set; get; }

        public virtual Subject Subject { set; get; }

        public int? Point { set; get; }

    }


}
