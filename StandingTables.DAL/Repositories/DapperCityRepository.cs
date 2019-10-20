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
    public class DapperCityRepository : IRepository<City>
    {
        private string connectionString = Settings.Default.connectionString;
        public DapperCityRepository()
        {
            string pathLoc = AppDomain.CurrentDomain.BaseDirectory;
            DapperDbInitializator.DbInit();
        }

        public void Create(City item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
               db.Open();
                string sqlQuery = "Select CityId From City Where (CityName = @CityName)";
                int? id = db.Query<int?>(sqlQuery, item).FirstOrDefault();
                if (id != null) return;

                sqlQuery = "Insert into City(CityName)" +
                    "values" +
                    "(@CityName);" +
                    "SELECT CAST(last_insert_rowid() as int)";
               id =  db.Query<int>(sqlQuery, item).FirstOrDefault();
               if (id!=null) item.CityId = (int)id;
               db.Close();
            }
        }

        public void Delete(int id)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Delete from City Where (CityId = @CityId)";
                db.Execute(sqlQuery,new { CityId = id });
                db.Close();

            }
        }

        public City get(int id)
        {
            var p = new DynamicParameters();
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Select * From City where (CityId=@CityId)";
                p.Add("@CityId", id);
                var city = db.Query<City>(sqlQuery,p).FirstOrDefault();
                db.Close();
                return city;
            }
        }

        public IEnumerable<City> getAll()
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Select * From City";
                var cities = db.Query<City>(sqlQuery);
                db.Close();
                return cities;
            }
        }

        public void Update(City item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery  =  "UPDATE City " +
                                    "SET " +
                                    "CityName = @CityName " +
                                    "WHERE (CityId=@CityId)"; 
                db.Execute(sqlQuery, item);
                db.Close();
            }
        }
    }
}
