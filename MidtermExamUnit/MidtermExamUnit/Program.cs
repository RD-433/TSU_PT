using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace MidtermExamUnit
{
    class Program
    {

        static void Main(string[] args)
        {
            HasData();

            IEnumerable<string> data = DbClass.ReadCSV(@"data.csv");


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

                    int[] splitted = DbClass.SplitString(item);

                    if (splitted == null)
                    {
                        return;
                    }

                    int studentId, subjectId, point;
                    studentId = splitted[0];
                    subjectId = splitted[1];
                    point = splitted[2];


                    if (!(point >= 0 && point <= 100))
                    {
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
                        countBad++;
                        continue;
                    }

                    subjectStudent.Point = point;
                    db.SaveChanges();
                    countGood++;


                }
            }

            Console.WriteLine("\nImported " + countGood + " items.\n" +
                "Failed to import " + countBad + " items.");

        }


        static void HasData()
        {
            List<Student> students = new List<Student>(DbClass.CreateStudents());

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
