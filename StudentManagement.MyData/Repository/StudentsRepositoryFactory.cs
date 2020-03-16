using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.MyData.DataSource;
using StudentManagement.Core.DomainModel;
using StudentManagement.Core.Repository;



namespace StudentManagement.MyData.Repository
{
    public class StudentsRepositoryFactory 
    {
        public static IStudentRepository CreateStudentRepository()
        {
            return new StudentRepository(DataSource<Student>.Instance);
        }
    }
}
