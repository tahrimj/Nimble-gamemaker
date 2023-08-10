using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nimble_UI
{
    public class Database_Sqlite
    {
        SQLiteConnection sqlite_conn = null;
        private static string Table_Name = "Player_Score";

        public Database_Sqlite()
        {
            this.sqlite_conn = new SQLiteConnection();
        }

        // ************* Create Connection *******************
        public SQLiteConnection CreateConnection(string databaseName)
        {
            // Create a new database connection:
            if (this.sqlite_conn != null)
            {
                this.sqlite_conn = new SQLiteConnection("Data Source=" + databaseName + ".db" + ";" + "Version = 3; New = True; Compress = True;");
            }

            return sqlite_conn;
        }
        // ************* Create Connection END *******************


        // ************* Create Table IF NOT EXIST *******************
        public void CreateTable()
        {
            if (this.sqlite_conn != null)
            {
                this.sqlite_conn.Open();

                SQLiteCommand sqlite_cmd = this.sqlite_conn.CreateCommand();

                string createTable = "CREATE TABLE IF NOT EXISTS " + Table_Name + " (Id INTEGER PRIMARY KEY AUTOINCREMENT NOT NULL, Score INT)";

                sqlite_cmd.CommandText = createTable;
                sqlite_cmd.ExecuteNonQuery();

            }

            this.sqlite_conn.Close();
        }
        // ************* Create Table END *******************


        // ************* INSERT DATA *******************
        public void InsertData(int score)
        {

            if (this.sqlite_conn != null)
            {
                this.sqlite_conn.Open();

                SQLiteCommand sqlite_cmd = this.sqlite_conn.CreateCommand();

                sqlite_cmd.CommandText = "INSERT INTO " + Table_Name + " (Id, Score) VALUES(NULL, " + score + "); ";
                sqlite_cmd.ExecuteNonQuery();
            }

            this.sqlite_conn.Close();
        }
        // ************* INSERT DATA END *******************

        public List<long> ReadData()
        {
            List<long> getReaderData = new List<long>();

            if (this.sqlite_conn != null)
            {
                this.sqlite_conn.Open();

                string readData = "SELECT * FROM " + Table_Name;

                using (var cmd = new SQLiteCommand(readData, this.sqlite_conn))
                {
                    SQLiteDataReader dataReader = cmd.ExecuteReader();

                    while (dataReader.Read())
                    {
                        getReaderData.Add(dataReader.GetInt64(0));
                        getReaderData.Add(dataReader.GetInt64(1));
                    }
                }
            }
            this.sqlite_conn.Close();

            return getReaderData;
        }

        // ************* UPDATE DATA *******************
        public void UpdateData(int id, int score)
        {
            if (this.sqlite_conn != null)
            {
                this.sqlite_conn.Open();

                using (SQLiteCommand command = this.sqlite_conn.CreateCommand())
                {
                    command.CommandText = "update " + Table_Name + "  set Score = :score where ID=:id";
                    command.Parameters.Add("id", DbType.Int64).Value = id;
                    command.Parameters.Add("score", DbType.Int64).Value = score;
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }

            this.sqlite_conn.Close();
        }
        // ************* UPDATE DATA END *******************

        public void DeleteData(int id)
        {
            if (this.sqlite_conn != null)
            {
                this.sqlite_conn.Open();

                using (SQLiteCommand command = this.sqlite_conn.CreateCommand())
                {
                    command.CommandText = "DELETE FROM " + Table_Name + " WHERE id=:id";
                    command.Parameters.Add("id", DbType.Int64).Value = id;
                    command.ExecuteNonQuery();
                    command.Dispose();
                }
            }

            this.sqlite_conn.Close();
        }
    }
}
