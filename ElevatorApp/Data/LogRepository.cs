using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using ElevatorApp.Models;

namespace ElevatorApp.Data
{
    public class LogRepository
    {
        private readonly string _connectionString;

        public LogRepository()
        {
            string dbFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
            Directory.CreateDirectory(dbFolder);

            string dbPath = Path.Combine(dbFolder, "elevator.db");
            _connectionString = "Data Source=" + dbPath + ";Version=3;";

            EnsureTableExists();
        }

        private void EnsureTableExists()
        {
            using (var connection = new SQLiteConnection(_connectionString))
            {
                connection.Open();
                using (var command = connection.CreateCommand())
                {
                    command.CommandText =
                        "CREATE TABLE IF NOT EXISTS ElevatorLog (" +
                        "Id INTEGER PRIMARY KEY AUTOINCREMENT, " +
                        "ElevatorName TEXT NOT NULL, " +
                        "Floor INTEGER NOT NULL, " +
                        "Timestamp TEXT NOT NULL)";
                    command.ExecuteNonQuery();
                }
            }
        }

        public void InsertLog(LogEntry entry)
        {
            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    var table = new DataTable();
                    using (var adapter = new SQLiteDataAdapter(
                        "SELECT Id, ElevatorName, Floor, Timestamp FROM ElevatorLog WHERE 1 = 0", connection))
                    {
                        adapter.Fill(table);

                        adapter.InsertCommand = new SQLiteCommand(
                            "INSERT INTO ElevatorLog (ElevatorName, Floor, Timestamp) " +
                            "VALUES (@ElevatorName, @Floor, @Timestamp)",
                            connection);

                        adapter.InsertCommand.Parameters.Add(
                            new SQLiteParameter("@ElevatorName", DbType.String) { SourceColumn = "ElevatorName" });
                        adapter.InsertCommand.Parameters.Add(
                            new SQLiteParameter("@Floor", DbType.Int32) { SourceColumn = "Floor" });
                        adapter.InsertCommand.Parameters.Add(
                            new SQLiteParameter("@Timestamp", DbType.String) { SourceColumn = "Timestamp" });

                        DataRow row = table.NewRow();
                        row["ElevatorName"] = entry.ElevatorName;
                        row["Floor"] = entry.Floor;
                        row["Timestamp"] = entry.Timestamp.ToString("yyyy-MM-dd HH:mm:ss");
                        table.Rows.Add(row);

                        adapter.Update(table);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not write elevator log entry.", ex);
            }
        }

        public DataTable GetAllLogs()
        {
            var table = new DataTable();

            try
            {
                using (var connection = new SQLiteConnection(_connectionString))
                {
                    connection.Open();

                    using (var adapter = new SQLiteDataAdapter(
                        "SELECT Id, ElevatorName, Floor, Timestamp FROM ElevatorLog ORDER BY Id DESC", connection))
                    {
                        adapter.Fill(table);
                    }
                }
            }
            catch (Exception ex)
            {
                throw new ApplicationException("Could not read elevator log entries.", ex);
            }

            return table;
        }
    }
}
