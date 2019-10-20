using StandingTables.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.DAL.Repositories
{
    public interface IRepository<T> where T:class
    {
        IEnumerable<T> getAll();
        T get(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
