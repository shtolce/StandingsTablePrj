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
        Player get(int id);
        IEnumerable<T> find(Func<T, Boolean> predicate);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
