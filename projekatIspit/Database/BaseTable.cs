using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekatIspit.Database
{
    public class BaseTable
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
    }
}
