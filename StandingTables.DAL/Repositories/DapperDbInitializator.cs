using Dapper;
using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StandingTables.DAL.Repositories
{

    public static class DapperDbInitializator
    {
        private static string connectionString = Settings.Default.connectionString;
        public static void DbInit()
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                var s = db.State;
                string sqlQuery = "CREATE TABLE IF NOT EXISTS Player(" +
                                    "PlayerId       INTEGER Primary Key AUTOINCREMENT," +
                                    "PlayerFio      nvarchar(40)," +
                                    "PlayerBornDate DATE," +
                                    "PlayerHeight   REAL," +
                                    "PlayerWeight   REAL," +
                                    "PlayerGender   nvarchar(40)," +
                                    "ClubId   INTEGER," +
                                    "PlayerUnits    nvarchar(40)," +
                                    "Foreign Key(ClubId) references Club(ClubId)" +
                                    ")";
                db.Execute(sqlQuery);
                sqlQuery = "CREATE TABLE IF NOT EXISTS Club(" +
                            "ClubId             INTEGER Primary Key AUTOINCREMENT," +
                            "ClubName           nvarchar(40) ," +
                            "CityId         INTEGER," +
                            "Foreign Key(CityId) references City(CityId)" +
                            ")";
                db.Execute(sqlQuery);
                sqlQuery = "CREATE TABLE IF NOT EXISTS City(" +
                           "CityId       INTEGER Primary Key AUTOINCREMENT," +
                           "CityName     nvarchar(40) " +
                           ")";
                db.Execute(sqlQuery);
                sqlQuery = "CREATE TABLE IF NOT EXISTS Category(" +
                           "CategoryId                INTEGER Primary Key AUTOINCREMENT," +
                           "CategoryAge               INTEGER ," +
                           "CategoryValue     INTEGER " +
                           ")";
                db.Execute(sqlQuery);
                sqlQuery = "CREATE TABLE IF NOT EXISTS StandingRaw(" +
                           "StandingRawId                INTEGER Primary Key AUTOINCREMENT," +
                           "PlayerId          INTEGER ," +
                           "CategoryId        INTEGER ," +
                           "StandingRawLevel             INTEGER ," +
                           "StandingRawPairNum           INTEGER ," +
                           "StandingRawGender            nvarchar(40)," +
                           "Foreign Key(PlayerId) references Player(PlayerId)," +
                           "Foreign Key(CategoryId) references Category(CategoryId)" +
                           ")";
                db.Execute(sqlQuery);
                db.Close();
            }


        }





    }
}
