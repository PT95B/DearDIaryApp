using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using DearDIary.Models;
using DearDIary;

namespace DearDIary
{
    public static class ResetData
    {
        public static void ResetDBData()
        {
            var app = (Application.Current as App);
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                db.DeleteAll<JournalEntry>();

                //db.Insert(new JournalEntry()
                //{
                //    Id = 1,
                //    Title = "Journal Entry 1",
                //    Content = "A journal entry content string blah blah",
                //    date = DateTime.Now
                //});

                //db.Insert(new JournalEntry()
                //{
                //    Id = 2,
                //    Title = "Journal Entry 2",
                //    Content = "A journal entry content string 2 blah blah",
                //    date = DateTime.UtcNow
                //});
            }
        }
    }
}
