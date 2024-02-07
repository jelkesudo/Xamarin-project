using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace projekatIspit.Database
{
    public class Db
    {
        private SQLiteConnection _conn;
        public Db()
        {
            var dbPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            var dbFile = Path.Combine(dbPath, "projekatIspit.db3");

            _conn = new SQLiteConnection(dbFile);
            _conn.CreateTable<UserTable>();
            _conn.CreateTable<ImportanceTable>();
            _conn.CreateTable<TaskTable>();
        }
        public SQLiteConnection Conn => _conn;
    }
}
