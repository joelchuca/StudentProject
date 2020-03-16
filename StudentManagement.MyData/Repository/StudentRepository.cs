using System;
using System.Collections.Generic;
using System.Linq;
using StudentManagement.Core.DomainModel;
using StudentManagement.Core.Repository;
using StudentManagement.MyData.DataSource;

namespace StudentManagement.MyData.Repository
{
    public class StudentRepository : IStudentRepository
    {
        protected DataSource<Student> StudentsSource { get; }

        public StudentRepository(DataSource<Student> datasource)
        {
            StudentsSource = datasource ?? throw new ArgumentNullException(nameof(datasource));
        }

        public IEnumerable<Student> SearchByName(string name)
        {
            return this.StudentsSource.Select(x => x.Value).Where(x => x.Name.ToUpper().Contains(name.ToUpper())).OrderBy(x => x.Name);
        }

        public IEnumerable<Student> SearchByStudentType(string studentType)
        {
            return this.StudentsSource.Select(x => x.Value).Where(x => x.Type.ToUpper() == studentType.ToUpper()).OrderByDescending(x => x.LastUpdate);
        }

        public IEnumerable<Student> SearchByGenderAndType(string gender, string studentType)
        {
            return this.StudentsSource.Select(x => x.Value).Where(x => x.Gender.ToUpper() == gender.ToUpper() && x.Type.ToUpper() == studentType.ToUpper())
                .OrderByDescending(x => x.LastUpdate);
        }

        public void Delete(Guid id)
        {
            StudentsSource.Delete(id);
        }

        public Student Get(Guid id)
        {
            return this.StudentsSource[id];
        }

        public IEnumerable<Student> GetAll()
        {
            return this.StudentsSource.Values;
        }

        public Student SaveOrUpdate(Student entity)
        {
            if(this.StudentsSource.ContainsKey(entity.Id))
            {
                this.StudentsSource[entity.Id] = entity;
            }
            else
            {
                this.StudentsSource.Insert(entity);
            }

            return entity;
        }
    }
}
