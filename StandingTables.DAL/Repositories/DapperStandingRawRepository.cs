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
namespace StandingTables.DAL.Repositories
{
    public class DapperStandingsRawRepository : IRepository<StandingsRaw>
    {
        private string connectionString = Settings.Default.connectionString;
        public DapperStandingsRawRepository()
        {
            string pathLoc = AppDomain.CurrentDomain.BaseDirectory;
            DapperDbInitializator.DbInit();
        }
        public void Create(StandingsRaw item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Insert into StandingRaw(PlayerId,CategoryId,StandingRawLevel,StandingRawPairNum,StandingRawGender)" +
                    "values" +
                    "(@PlayerId,@CategoryId,@StandingsRawLevel,@StandingsRawPairNum,@StandingsRawGender);" +
                    "SELECT CAST(last_insert_rowid() as int)";

                var _item = new
                {
                    item.Category.CategoryId,
                    item.StandingsRawGender,
                    item.StandingsRawLevel,
                    item.StandingsRawPairNum,
                    item.Player.PlayerId
                };


                int? id =  db.Query<int>(sqlQuery, _item).FirstOrDefault();
                if (id!=null) item.StandingsRawId = (int)id;
                db.Close();
            }
        }

        public void Delete(int id)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Delete from StandingRaw Where (StandingRawId = @Id)";
                db.Execute(sqlQuery, new { Id = id });
                db.Close();

            }
        }

        public StandingsRaw get(int id)
        {
            var p = new DynamicParameters();
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Select * " +
                    "from StandingsRaw sr " +
                    "join Player pl on " +
                    "sr.ClubId = pl.ClubId " +
                    "join Category cat on sr.CategoryId = cat.CategoryId " +
                    "where (Id=@Id) ";


                p.Add("@Id", id);
                var StandingsRaw = db.Query<StandingsRaw,Player,Category, StandingsRaw>(sqlQuery,(st,pl,cat)=> 
                {
                    st.Player = pl;
                    st.Category = cat;
                    return st;
                },
                p,splitOn:"PlayerId,CategoryId").FirstOrDefault();
                db.Close();
                return StandingsRaw;
            }
        }

        public IEnumerable<StandingsRaw> getAll()
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Select * " +
                    "from StandingsRaw sr " +
                    "join Player pl on " +
                    "sr.ClubId = pl.ClubId " +
                    "join Category cat on sr.CategoryId = cat.CategoryId ";

                var StandingsRaws = db.Query<StandingsRaw, Player, Category, StandingsRaw>(sqlQuery, (st, pl, cat) =>
                {
                    st.Player = pl;
                    st.Category = cat;
                    return st;
                },
                splitOn: "PlayerId,CategoryId");
                db.Close();
                return StandingsRaws;
            }
        }

        public void Update(StandingsRaw item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "UPDATE StandingsRaw " +
                                    "SET " +
                                    "PlayerId = @PlayerId, " +
                                    "CategoryId = @CategoryId, " +
                                    "StandingRawLevel = @StandingRawLevel," +
                                    "StandingRawPairNum = @StandingRawPairNum, " +
                                    "StandingRawGender = @StandingRawGender " +
                                    "WHERE (ClubId=@ClubId)";

                var _item = new
                {
                    item.Category.CategoryId,
                    item.StandingsRawGender,
                    item.StandingsRawLevel,
                    item.StandingsRawPairNum,
                    item.Player.PlayerId,
                };
                db.Execute(sqlQuery, _item);
                db.Close();
            }
        }
    }
}
