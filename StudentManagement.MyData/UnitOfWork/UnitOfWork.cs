using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Core.DomainModel;
using StudentManagement.Core.Repository;
using StudentManagement.Core.UnitOfWork;
using StudentManagement.MyData.Repository;
using StudentManagement.MyData.DataSource;


namespace StudentManagement.MyData.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudentRepository _students;

        public IStudentRepository Students => _students ?? new StudentRepository(DataSource<Student>.Instance);
    }
}
