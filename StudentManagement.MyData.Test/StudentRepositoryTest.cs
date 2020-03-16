using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManagement.Core.DomainModel;
using StudentManagement.Core.Repository;
using StudentManagement.MyData.Repository;
using StudentManagement.MyData.DataSource;


namespace StudentManagement.MyData.Test
{
    [TestClass]
    public class StudentRepositoryTest
    {
        
        [TestMethod]
        public void InsertTest()
        {
            var newStudent = new Student()
            {
                Id = Guid.NewGuid(),
                Name = "NameTest",
                Gender = "M",
                LastUpdate = DateTime.Now,
                Type = "Elementary"
            };

            IStudentRepository studentRepo = StudentsRepositoryFactory.CreateStudentRepository();
            studentRepo.SaveOrUpdate(newStudent);

            Assert.AreEqual(1 , DataSource<Student>.Instance.Count);
            DataSource<Student>.Instance.Clear();
        }

        [TestMethod]
        public void GetTest()
        {
            var newStudent = new Student()
            {
                Id = Guid.NewGuid(),
                Name = "NameTest",
                Gender = "M",
                LastUpdate = DateTime.Now,
                Type = "Elementary"
            };

            IStudentRepository studentRepo = StudentsRepositoryFactory.CreateStudentRepository();
            studentRepo.SaveOrUpdate(newStudent);

            Student student = studentRepo.Get(newStudent.Id);

            Assert.IsNotNull(student);
            DataSource<Student>.Instance.Clear();
        }

        [TestMethod]
        public void SearchByNameTest()
        {
            List<Student> students = GenerateStudents();
            var expectedStudent = new Student()
            {
                Id = Guid.NewGuid(),
                Name = "Juan",
                Gender = "M",
                LastUpdate = DateTime.Now,
                Type = "Kinder"
            };
            students.Add(expectedStudent);

            foreach (var student in students)
            {
                DataSource<Student>.Instance.Insert(student);
            }

            IStudentRepository studentRepo = StudentsRepositoryFactory.CreateStudentRepository();
            var result = studentRepo.SearchByName("Juan");
            
            Assert.AreEqual(expectedStudent.Name, result.First().Name );
            DataSource<Student>.Instance.Clear();
        }

        [TestMethod]
        public void SearchByName_NameNull_Test()
        {
            IStudentRepository studentRepo = StudentsRepositoryFactory.CreateStudentRepository();
            var result = studentRepo.SearchByName(null);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SearchByStudentTypeTest()
        {
            List<Student> students = GenerateStudents();
            var expectedStudent = new Student()
            {
                Id = Guid.NewGuid(),
                Name = "Juan",
                Gender = "M",
                LastUpdate = DateTime.Now,
                Type = "Elementary"
            };
            students.Add(expectedStudent);

            foreach (var student in students)
            {
                DataSource<Student>.Instance.Insert(student);
            }

            IStudentRepository studentRepo = StudentsRepositoryFactory.CreateStudentRepository();
            var result = studentRepo.SearchByStudentType("Elementary");

            Assert.AreEqual(expectedStudent.LastUpdate, result.First().LastUpdate);
            DataSource<Student>.Instance.Clear();
        }

        [TestMethod]
        public void SearchByGenderAndTypeTest()
        {
            List<Student> students = GenerateStudents();
            var expectedStudent = new Student()
            {
                Id = Guid.NewGuid(),
                Name = "Maria",
                Gender = "F",
                LastUpdate = DateTime.Now,
                Type = "Elementary"
            };
            students.Add(expectedStudent);

            foreach (var student in students)
            {
                DataSource<Student>.Instance.Insert(student);
            }

            IStudentRepository studentRepo = new StudentRepository(DataSource<Student>.Instance);
            var result = studentRepo.SearchByGenderAndType("F","Elementary");

            Assert.AreEqual(expectedStudent.LastUpdate, result.First().LastUpdate);
            DataSource<Student>.Instance.Clear();
        }

        private List<Student> GenerateStudents()
        {
            var list = new List<Student>
            {
                new Student()
                {
                    Id = Guid.NewGuid(),
                    Name = "Juanito",
                    Gender = "M",
                    LastUpdate = new DateTime(2019,12,1,8,0,0),
                    Type = "Elementary"
                },

                new Student()
                {
                    Id = Guid.NewGuid(),
                    Name = "Marcelo",
                    Gender = "M",
                    LastUpdate = new DateTime(2020,3,5,10,0,0),
                    Type = "School"
                },

                new Student()
                {
                    Id = Guid.NewGuid(),
                    Name = "Juana",
                    Gender = "F",
                    LastUpdate = new DateTime(2020,1,20,8,0,0),
                    Type = "Elementary"
                },

                new Student()
                {
                    Id = Guid.NewGuid(),
                    Name = "Pablo",
                    Gender = "M",
                    LastUpdate = new DateTime(2020,2,1,8,0,0),
                    Type = "School"
                },
            };

            return list;
        }
    }
}
