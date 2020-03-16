using StudentManagement.Core.DomainModel;
using StudentManagement.Core.Repository;


namespace StudentManagement.Core.UnitOfWork
{
    public interface IUnitOfWork
    {
        IStudentRepository Students { get; }
    }
}
