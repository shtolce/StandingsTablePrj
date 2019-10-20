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
    public class DapperCategoryRepository : IRepository<Category>
    {
        private string connectionString = Settings.Default.connectionString;
        public DapperCategoryRepository()
        {
            string pathLoc = AppDomain.CurrentDomain.BaseDirectory;
            DapperDbInitializator.DbInit();
        }

        public void Create(Category item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
               db.Open();
               string sqlQuery = "Select CategoryId From Category Where (CategoryAge = @CategoryAge and CategoryValue = @CategoryValue)";
               int? id = db.Query<int?>(sqlQuery, item).FirstOrDefault();
               if (id != null) return;
               sqlQuery = "Insert into Category(CategoryAge,CategoryValue)" +
                    "values" +
                    "(@CategoryAge,@CategoryValue);" +
                    "SELECT CAST(last_insert_rowid() as int)";
               id =  db.Query<int>(sqlQuery, item).FirstOrDefault();
               if (id!=null) item.CategoryId = (int)id;
               db.Close();
            }
        }

        public void Delete(int id)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Delete from Category Where (CategoryId = @CategoryId)";
                db.Execute(sqlQuery, new { CategoryId = id });
                db.Close();

            }
        }

        public Category get(int id)
        {
            var p = new DynamicParameters();
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Select * From Category where (CategoryId=@CategoryId)";
                p.Add("@CategoryId", id);
                var category = db.Query<Category>(sqlQuery, p).FirstOrDefault();
                db.Close();
                return category;
            }
        }

        public IEnumerable<Category> getAll()
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "Select * From Category";
                var categories = db.Query<Category>(sqlQuery);
                db.Close();
                return categories;
            }
        }

        public void Update(Category item)
        {
            using (var db = new SQLiteConnection(connectionString))
            {
                db.Open();
                string sqlQuery = "UPDATE Category " +
                                    "SET " +
                                    "CategoryValue = @CategoryValue," +
                                    "CategoryAge = @CategoryAge " +
                                    "WHERE (CategoryId=@CategoryId)";
                db.Execute(sqlQuery, item);
                db.Close();
            }
        }
    }
}
