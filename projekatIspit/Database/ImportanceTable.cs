using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekatIspit.Database
{
    [Table("Importances")]
    public class ImportanceTable : BaseTable
    {
        [Unique, NotNull]
        public string Name { get; set; }
    }
}
