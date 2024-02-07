using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekatIspit.Database
{
    [Table("Tasks")]
    public class TaskTable : BaseTable
    {
        [NotNull]
        public string Name { get; set; }
        [NotNull]
        public string Description { get; set; }
        [NotNull]
        public int ImportanceId { get; set; }
    }
}
