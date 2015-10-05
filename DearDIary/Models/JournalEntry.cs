using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DearDIary.Models
{
    public class JournalEntry
    {


        [SQLite.PrimaryKey]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime date { get; set; }
    }
}
