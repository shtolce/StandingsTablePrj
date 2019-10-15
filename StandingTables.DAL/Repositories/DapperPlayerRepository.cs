using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using StandingTables.DAL.Models;
//using System.Data.
namespace StandingTables.DAL.Repositories
{
    public class DapperPlayerRepository : IRepository<Player>
    {
        private string connectionString = Settings.Default.connectionString;
        public DapperPlayerRepository()
        {
            string pathLoc = AppDomain.CurrentDomain.BaseDirectory;

            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                var s = db.State;
                string sqlQuery =   "CREATE TABLE IF NOT EXISTS Player(" +
                                    "Id       INTEGER Primary Key,"      +
                                    "Fio      TEXT,"                     +
                                    "BornDate TEXT,"                     +
                                    "Height   REAL,"                     +
                                    "Weight   REAL,"                     +
                                    "Gender   TEXT,"                     +
                                    "ClubId   INTEGER,"                  +
                                    "Units    TEXT,"                     +
                                    "Foreign Key(ClubId) references Club(id)" +
                                    ")";
                db.Execute(sqlQuery);
                sqlQuery = "CREATE TABLE IF NOT EXISTS Club("         +
                            "Id             INTEGER Primary Key,"     +
                            "Name           TEXT ,"                   +
                            "CityId         INTEGER,"                 +
                            "Foreign Key(CityId) references City(id)" +
                            ")";
                db.Execute(sqlQuery);
                sqlQuery = "CREATE TABLE IF NOT EXISTS City(" +
                           "Id       INTEGER Primary Key,"    +
                           "Name     TEXT "                   +
                           ")";
                db.Execute(sqlQuery);
                sqlQuery = "CREATE TABLE IF NOT EXISTS Category("   +
                           "Id                INTEGER Primary Key," +
                           "Age               INTEGER ,"            +
                           "CategoryValue     INTEGER "             +
                           ")";
                db.Execute(sqlQuery);
                sqlQuery = "CREATE TABLE IF NOT EXISTS StandingRaw(" +
                           "Id                INTEGER Primary Key,"  +
                           "PlayerId          INTEGER ,"             +
                           "CategoryId        INTEGER ,"             +
                           "Level             INTEGER ,"             +
                           "PairNum           INTEGER ,"             +
                           "Gender            TEXT    ,"             +
                           "Foreign Key(PlayerId) references Player(id),"     +
                           "Foreign Key(CategoryId) references Category(id)"  +
                           ")";
                db.Execute(sqlQuery);
                db.Close();
            }

        }

        public void Create(Player item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();




            }


        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> find(Func<Player, bool> predicate)
        {
            throw new NotImplementedException();
        }

        public Player get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Player> getAll()
        {
            throw new NotImplementedException();
        }

        public void Update(Player item)
        {
            throw new NotImplementedException();
        }
    }
}
