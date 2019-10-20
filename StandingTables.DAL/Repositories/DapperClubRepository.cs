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
    public class DapperClubRepository : IRepository<Club>
    {
        private string connectionString = Settings.Default.connectionString;
        public DapperClubRepository()
        {
            string pathLoc = AppDomain.CurrentDomain.BaseDirectory;
            DapperDbInitializator.DbInit();
        }

        public void Create(Club item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
               db.Open();
               string sqlQuery = "Select ClubId From Club Where (ClubName = @ClubName and CityId = @CityId)";
               int? id = db.Query<int?>(sqlQuery, new { item.ClubName, CityId = item.City.CityId }).FirstOrDefault();
               if (id != null) return;

               sqlQuery = "Insert into Club(ClubName,CityId)" +
                    "values" +
                    "(@ClubName,@CityId);" +
                    "SELECT CAST(last_insert_rowid() as int)";
               id =  db.Query<int>(sqlQuery, new { item.ClubName, CityId = item.City.CityId }).FirstOrDefault();
               if (id!=null) item.ClubId = (int)id;
               db.Close();
            }
        }

        public void Delete(int id)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Delete from Club Where (ClubId = @ClubId)";
                db.Execute(sqlQuery, new { ClubId = id });
                db.Close();

            }
        }

        public IEnumerable<Club> FindByName(string name)
        {
            var p = new DynamicParameters();
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                var sqlQuery = "Select * " +
                    "From Club cl join City ct on " +
                    "ct.CityId = cl.CityId " +
                    "where (cl.ClubName=@ClubName)";

                p.Add("@ClubName", name);
                var clubes = db.Query<Club, City, Club>(sqlQuery, (cl, city) => {
                    cl.City = city;
                    return cl;
                }, p, splitOn: "CityId");
                db.Close();
                return clubes;
            }
        }

        public Club get(int id)
        {
            var p = new DynamicParameters();
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                var sqlQuery = "Select * " +
                    "From Club cl join City ct on " +
                    "ct.CityId = cl.CityId " +
                    "where (cl.ClubId=@Id)";

                p.Add("@Id", id);
                var club = db.Query<Club, City, Club>(sqlQuery, (cl, city) => {
                    cl.City = city;
                    return cl;
                }, p,splitOn: "CityId").FirstOrDefault();
                db.Close();
                return club;
            }
        }

        public IEnumerable<Club> getAll()
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                var sqlQuery = "Select * " +
                    "From Club cl join City ct on " +
                    "ct.CityId = cl.CityId ";
                var clubes = db.Query<Club,City,Club>(sqlQuery,(club,city)=> {
                    club.City = city;
                    return club;
                },splitOn:"CityId");
                db.Close();
                return clubes;
            }
        }

        public void Update(Club item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "UPDATE Club " +
                                    "SET " +
                                    "ClubName = @ClubName " +
                                    "WHERE (ClubId=@ClubId)";
                db.Execute(sqlQuery, item);
                db.Close();
            }
        }
    }
}
