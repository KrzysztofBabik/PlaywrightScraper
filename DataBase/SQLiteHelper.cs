using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

namespace PlaywrightScraper.DataBase
{
    public class SQLiteHelper
    {
        private readonly string _connectionString = "Data Source=prices.db;Version=3;";

        public SQLiteHelper()
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            var command = new SQLiteCommand(
                "CREATE TABLE IF NOT EXISTS Prices (Id INTEGER PRIMARY KEY, Product TEXT, Price TEXT, Date DATETIME DEFAULT CURRENT_TIMESTAMP)",
                connection);
            command.ExecuteNonQuery();
        }

        public void SavePrices(string product, string price)
        {
            using var connection = new SQLiteConnection(_connectionString);
            connection.Open();
            var command = new SQLiteCommand("INSERT INTO Prices(Product, Price) VALUES(@product, @price)", connection);
            command.Parameters.AddWithValue("@product", product);
            command.Parameters.AddWithValue("@price", price);
            command.ExecuteNonQuery();
        }
    }
}
