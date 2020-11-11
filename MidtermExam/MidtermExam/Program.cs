using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace MidtermExam
{
    public class StaticClass
    {
        public static readonly List<string> studentNames = new List<string> { "Archil", "Ana", "Eka", "Giorgi", "Malxaz",
            "Nikoloz", "Ana", "Vaxtang", "Mari", "Rakanishu" };
        public static readonly int studentNamesCount = 10;
        public static readonly List<Subject> subjects = new List<Subject>()
            {
                new Subject() { SubjectId = 1, SubjectName = "Operating Systems" },
                new Subject() { SubjectId = 2, SubjectName = "Security" },
                new Subject() { SubjectId = 3, SubjectName = "Databases" },
                new Subject() { SubjectId = 4, SubjectName = ".NET" },
                new Subject() { SubjectId = 5, SubjectName = "Web" },
            };
    }

    class Program
    {
        static void Main(string[] args)
        {
            //HasData();

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


        //static void HasData()
        //{


        //    Random randomGenerator = new Random();

        //    List<Student> students = new List<Student>();

        //    for (int i = 0; i < 50; i++)
        //    {
        //        Student student = new Student()
        //        {
        //            StudentName = StaticClass.studentNames[randomGenerator.Next(StaticClass.studentNamesCount)]
        //        };

        //        List<SubjectStudent> subjectStudent = new List<SubjectStudent>();

        //        for (int j = 0; j < 2; j++)
        //        {
        //            int subjectId = randomGenerator.Next(5);
        //            for (int k = 0; k < j; k++)
        //            {
        //                while (StaticClass.subjects[subjectId].SubjectName == subjectStudent[k].Subject.SubjectName)
        //                {
        //                    subjectId = randomGenerator.Next(5);
        //                    k = 0;
        //                }
        //            }
        //            subjectStudent.Add(new SubjectStudent() { Student = student, Subject = StaticClass.subjects[subjectId] });
        //        }

        //        student.Subjects = subjectStudent;
        //        students.Add(student);
        //    }

        //    using (BloggingDbContext db = new BloggingDbContext())
        //    {
        //        foreach (var student in students)
        //        {
        //            db.Students.Add(student);
        //        }

        //        db.SaveChanges();
        //    }
        //}


    }


    public class BloggingDbContext : DbContext
    {
        public DbSet<Student> Students { set; get; }

        public DbSet<SubjectStudent> SubjectStudents { set; get; }

        public DbSet<Subject> Subjects { set; get; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("server=RGN7_64-PC\\SQLEXPRESS;database=TSU_DB_local;Integrated security=true;");
            optionsBuilder.EnableSensitiveDataLogging(true);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Random randomGenerator = new Random();

            List<Student> students = new List<Student>();

            List<SubjectStudent> subjectStudentsTotal = new List<SubjectStudent>();

            for (int i = 0; i < 50; i++)
            {
                Student student = new Student()
                {
                    StudentId = i + 1,
                    StudentName = StaticClass.studentNames[randomGenerator.Next(StaticClass.studentNamesCount)]
                };

                List<SubjectStudent> subjectStudent = new List<SubjectStudent>();

                for (int j = 0; j < 2; j++)
                {
                    int subjectId = randomGenerator.Next(5);
                    for (int k = 0; k < j; k++)
                    {
                        while (StaticClass.subjects[subjectId].SubjectName == StaticClass.subjects[subjectStudent[k].SubjectId - 1].SubjectName)
                        {
                            subjectId = randomGenerator.Next(5);
                            k = 0;
                        }
                    }
                    subjectStudent.Add(new SubjectStudent()
                    {
                        SubjectStudentId = (i * 2) + 1 + j,
                        StudentId = student.StudentId,
                        SubjectId = subjectId + 1,
                        Point = null
                    });
                    subjectStudentsTotal.Add(subjectStudent[j]);
                }
                students.Add(student);
            }

            modelBuilder.Entity<Subject>().HasData(StaticClass.subjects);
            modelBuilder.Entity<Student>().HasData(students);
            modelBuilder.Entity<SubjectStudent>().HasData(subjectStudentsTotal);
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

        //public virtual Student Student { set; get; }

        public int SubjectId { set; get; }

        //public virtual Subject Subject { set; get; }

        public int? Point { set; get; }

    }


}
