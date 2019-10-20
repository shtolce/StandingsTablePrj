using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StandingTables.DAL.Models;

namespace StandingTables.DAL.Repositories
{
    //1111
    class DapperUnitOfWork : IUnitOfWork
    {
        private DapperCategoryRepository categories;
        private DapperCityRepository cities;
        private DapperClubRepository clubes;
        private DapperPlayerRepository players;
        private DapperStandingsRawRepository standingsRaws;


        public DapperUnitOfWork()
        {

        }

        public IRepository<Category> Categories
        {
            get
            {
                if (categories == null)
                    return new DapperCategoryRepository();
                else
                    return categories;
            }

        }
        public IRepository<City> Cities
        {
            get
            {
                if (cities == null)
                    return new DapperCityRepository();
                else
                    return cities;
            }

        }
        public IRepository<Club> Clubes
        {
            get
            {
                if (clubes == null)
                    return new DapperClubRepository();
                else
                    return clubes;
            }

        }
        public IRepository<Player> Players
        {
            get
            {
                if (players == null)
                    return new DapperPlayerRepository();
                else
                    return players;
            }

        }
        public IRepository<StandingsRaw> StandingRaws
        {
            get
            {
                if (standingsRaws == null)
                    return new DapperStandingsRawRepository();
                else
                    return standingsRaws;
            }

        }
        public void Dispose()
        {
        }
    }
}
