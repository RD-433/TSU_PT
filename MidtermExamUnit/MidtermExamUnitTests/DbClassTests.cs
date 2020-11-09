using Microsoft.VisualStudio.TestTools.UnitTesting;
using MidtermExamUnit;
using System;
using System.Collections.Generic;
using System.Text;

namespace MidtermExamUnit.Tests
{
    [TestClass()]
    public class DbClassTests
    {
        [TestMethod()]
        public void CreateStudentsTest()
        {
            List<Student> studentsTest = new List<Student>(DbClass.CreateStudents());

            Assert.IsTrue(studentsTest.Count == 50, "Students are not 50");

            foreach (var stud in studentsTest)
            {
                Assert.IsNotNull(stud.StudentName, "Student's name is NULL");
                Assert.IsNotNull(stud.Subjects, "Student's subjects are NULL");
            }
        }

        [TestMethod()]
        public void ReadCSVTest()
        {
            string path = @"data.csv";

            IEnumerable<string> data = DbClass.ReadCSV(path);

            Assert.IsNotNull(data);
        }

        [TestMethod()]
        public void SplitStringTest()
        {
            int[] expected = { 1, 2, 3 };

            string dataString = "1,2,3";

            int[] answer = DbClass.SplitString(dataString);

            for (int i = 0; i < 3; i++)
            {
                Assert.AreEqual(expected[i], answer[i]);
            }
            
        }




    }
}