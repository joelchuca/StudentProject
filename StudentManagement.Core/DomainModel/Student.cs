using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentManagement.Core.DomainModel
{
    public class Student : EntityBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public DateTime LastUpdate { get; set; }

    }
}
