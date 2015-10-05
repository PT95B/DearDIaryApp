using DearDIary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DearDIary.ViewModels
{
    
    public class JournalEntryViewModel:ViewModelBase
    {

        private int _id = 0;
        public int Id
        {
            get { return _id; }
            set
            {
                if (_id == value)
                {
                    return;
                }
                _id = value;
                RaisePropertyChanged("Id");
            }
        }
        private string _title = "";
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title == value)
                {
                    return;
                }
                _title = value;
                RaisePropertyChanged("Title");
            }
        }
        private string _content = "";
        public string Content
        {
            get { return _content; }
            set
            {
                if (_content == value)
                {
                    return;
                }
                _content = value;
                RaisePropertyChanged("Content");
            }
        }

        private App app = (Application.Current as App);

        private DateTime _date = DateTime.Now;
        public DateTime Date
        {
            get { return _date; }
            set
            {
                if (_date == value)
                {
                    return;
                }
                _date = value;
                RaisePropertyChanged("Date");
            }
        }
        public JournalEntryViewModel GetJournalEntry(int journalEntryId)
        {
            var journalEntry = new JournalEntryViewModel();
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                var je = db.Table<JournalEntry>().FirstOrDefault(x => x.Id == journalEntryId);
                journalEntry.Id = je.Id;
                journalEntry.Content = je.Content;
                journalEntry.Date = je.date;
                journalEntry.Title = je.Title;
            }
            return journalEntry;
        }

        public string GetJournalEntryTitle(int journalEntryId)
        {
            string journalEntryTitle = "Unknown";
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                var customer = (db.Table<JournalEntry>().Where(
                    c => c.Id == journalEntryId)).Single();
                journalEntryTitle = customer.Title;
            }
            return journalEntryTitle;
        }

        public string SaveJournalEntry(JournalEntryViewModel journalEntry)
        {
            string result = string.Empty;
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                string change = string.Empty;
                try
                {
                    var existingJournalEntry = (db.Table<JournalEntry>().Where(
                        c => c.Id == journalEntry.Id)).SingleOrDefault();

                    if (existingJournalEntry != null)
                    {
                        existingJournalEntry.Title = journalEntry.Title;
                        existingJournalEntry.Content = journalEntry.Content;
                        int success = db.Update(existingJournalEntry);
                    }
                    else
                    {
                        int success = db.Insert(new JournalEntry()
                        {
                            Id = journalEntry.Id,
                            Title = journalEntry.Title,
                            Content = journalEntry.Content,
                        });
                    }
                    result = "Success";
                }
                catch (Exception ex)
                {
                    result = "This customer was not saved.";
                }
            }
            return result;
        }

        public bool DeleteJournalEntry(int journalEntryId)
        {
            
            
            
            throw new NotImplementedException();
        }

        public int GetNewJournalEntryId()
        {
            int JeId = 0;
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                var journalEntries = db.Table<JournalEntry>();
                if (journalEntries.Count() >0)
                {
                    JeId =  db.Table<JournalEntry>().Max(x =>x.Id);
                    JeId += 1;
                }
                else
                {
                    JeId = 1;
                }
            }
            return JeId;
            
        }



    }
}
