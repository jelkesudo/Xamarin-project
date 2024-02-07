using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace projekatIspit.Database
{
    [Table("Users")]
    public class UserTable : BaseTable
    {
        public string FullName { get; set; }
        [Unique]
        public string Username { get; set; }
        [Unique]
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
