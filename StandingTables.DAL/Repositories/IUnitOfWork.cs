using StandingTables.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.DAL.Repositories
{
    public interface IUnitOfWork:IDisposable
    {
        IRepository<Category> Categories { get; }
        IRepository<City> Cities { get; }
        IRepository<Club> Clubes { get; }
        IRepository<Player> Players { get; }
        IRepository<StandingsRaw> StandingRaws { get; }
    }
}
