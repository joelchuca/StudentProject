using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StudentManagement.Core.DomainModel;

namespace StudentManagement.MyData.DataSource
{
    public class DataSource<TO>: ConcurrentDictionary<Guid,TO> where TO : EntityBase
    {
        private static DataSource<TO> _instance = null;
        private static readonly object Padlock = new object();

        private DataSource()
        {
        }

        public static DataSource<TO> Instance
        {
            get
            {
                lock (Padlock)
                {
                    return _instance ?? (_instance = new DataSource<TO>());
                }
            }
        }

        public int Insert(TO entity)
        {
            if (TryAdd(entity.Id, entity))
            {
                return 1;
            }

            return 0;
        }

        public bool Delete(Guid id)
        {
            TO studetOut;
            return TryRemove(id, out studetOut);

        }
    }
}
