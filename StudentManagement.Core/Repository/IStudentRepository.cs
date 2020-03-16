using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Core.DomainModel;


namespace StudentManagement.Core.Repository
{
    public interface IStudentRepository : IRepositoryWithTypedId<Student, Guid>
    {
        IEnumerable<Student> SearchByName(string name);
        IEnumerable<Student> SearchByStudentType(string studentType);
        IEnumerable<Student> SearchByGenderAndType(string gender, string studentType);
    }
}
