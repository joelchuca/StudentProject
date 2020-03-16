using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StudentManagement.Core.DomainModel;
using StudentManagement.Core.UnitOfWork;
using MyUnitofWork = StudentManagement.MyData.UnitOfWork;

namespace StudentManagement.MyData.Test
{
    [TestClass]
    public class UnitOfWorkTest
    {
        [TestMethod]
        public void GetStudentRepositoryTest()
        {
            IUnitOfWork unitOfWork = new MyUnitofWork.UnitOfWork();
            Assert.AreEqual(0, unitOfWork.Students.GetAll().Count());
        }


        // Many thread are going to run in parallel in order to simulate concurrency inserts
        // Generate N threads where each thread is going to insert M students.
        [TestMethod]
        public void ConcurrencyInsertTest()
        {
            Task[] tasks = new Task[5];
            for (int i = 0; i < tasks.Length; i++)
            {
                Console.WriteLine("Create task");

                object taskArg = i;
                var task = new TaskFactory().StartNew(new Action<object>((arg) =>
                {
                    IUnitOfWork unitofWork = new MyUnitofWork.UnitOfWork();
                    for (int j = 0; j < 10; j++)
                    {
                        Console.WriteLine(string.Format("NameTest-{0}-{1}", arg, j));
                        var newStudent = new Student()
                        {
                            Id = Guid.NewGuid(),
                            Name = string.Format("NameTest-{0}-{1}", arg, j),
                            Gender = "M",
                            LastUpdate = DateTime.Now,
                            Type = "Elementary"
                        };
                        unitofWork.Students.SaveOrUpdate(newStudent);

                    }
                    Console.WriteLine(unitofWork.Students.GetAll().Count());
                }), taskArg);

                tasks[i] = task;
            }

            Task.WaitAll(tasks);

            IUnitOfWork unitofWork2 = new MyUnitofWork.UnitOfWork();
            Assert.AreEqual(50,unitofWork2.Students.GetAll().Count());
            Assert.AreEqual(10, unitofWork2.Students.SearchByName("NameTest-0").Count());
        }

    }
}
