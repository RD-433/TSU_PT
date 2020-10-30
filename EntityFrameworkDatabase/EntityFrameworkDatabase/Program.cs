using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace EntityFrameworkDatabase
{
    class Program
    {

        static List<string> studentNames = new List<string> { "Archil", "Ana", "Eka", "Giorgi", "Malxaz",
            "Nikoloz", "Ana", "Vaxtang", "Mari", "Rakanishu" };
        static readonly int studentNamesCount = 10;

        static void Main(string[] args)
        {
            Console.WriteLine("Hello Entity!");

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

                for (int j = 0; j < randomGenerator.Next(2, 4); j++)
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

    }


}
