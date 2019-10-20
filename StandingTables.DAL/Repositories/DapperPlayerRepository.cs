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
            DapperDbInitializator.DbInit();
        }

        public void Create(Player item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Insert into Player(PlayerFio,PlayerBornDate,PlayerHeight,PlayerWeight,PlayerGender,ClubId)" +
                    "values" +
                    "(@PlayerFio,@PlayerBornDate,@PlayerHeight,@PlayerWeight,@PlayerGender,@ClubId);" +
                    "SELECT CAST(last_insert_rowid() as int)";

                var _item = new
                {
                    PlayerFio = item.PlayerFio,
                    PlayerBornDate = item.PlayerBornDate,
                    PlayerHeight = item.PlayerHeight,
                    PlayerWeight = item.PlayerWeight,
                    PlayerGender = item.PlayerGender,
                    ClubId = item.Club.ClubId
                };
                int? id =  db.Query<int>(sqlQuery, _item).FirstOrDefault();
                if (id!=null) item.PlayerId = (int)id;
                db.Close();
            }
        }

        public void Delete(int id)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Delete from Player Where (PlayerId = @Id)";
                db.Execute(sqlQuery, new { Id = id });
                db.Close();

            }
        }

        public Player get(int id)
        {
            var p = new DynamicParameters();
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Select * " +
                    "from Player pl join Club cl on " +
                    "pl.ClubId = cl.ClubId " +
                    "where (PlayerId=@Id) ";
                p.Add("@Id", id);
                var player = db.Query<Player,Club,Player>(sqlQuery, (pl,cl)=>
                {
                    pl.Club = cl;
                    return pl;
                },p,splitOn:"ClubId").FirstOrDefault();
                db.Close();
                return player;
            }
        }

        public IEnumerable<Player> getAll()
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Select * " +
                    "From Player pl join Club cl on " +
                    "pl.CLubId = cl.ClubId ";
                var players = db.Query<Player, Club, Player>(sqlQuery, (pl, cl) =>
                {
                    pl.Club = cl;
                    return pl;
                },splitOn: "ClubId");


                db.Close();
                return players;
            }
        }

        public void Update(Player item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "UPDATE Player " +
                                    "SET " +
                                    "PlayerFio = @PlayerFio, " +
                                    "PlayerBornDate = @PlayerBornDate, " +
                                    "PlayerHeight = @PlayerHeight," +
                                    "PlayerWeight = @PlayerWeight, " +
                                    "PlayerGender = @PlayerGender " +
                                    "WHERE (ClubId=@ClubId)";

                var _item = new
                {
                    PlayerFio = item.PlayerFio,
                    PlayerBornDate = item.PlayerBornDate,
                    PlayerHeight = item.PlayerHeight,
                    PlayerWeight = item.PlayerWeight,
                    PlayerGender = item.PlayerGender,
                    ClubId = item.Club.ClubId
                };
                db.Execute(sqlQuery, _item);
                db.Close();
            }
        }
    }
}
