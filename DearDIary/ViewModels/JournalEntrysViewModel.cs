using DearDIary.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;

namespace DearDIary.ViewModels
{
    public class JournalEntrysViewModel:ViewModelBase
    {
        private ObservableCollection<JournalEntryViewModel> _journalEntrys;

        public ObservableCollection<JournalEntryViewModel> JournalEntrys
        {
            get { return _journalEntrys; }
            set
            {
                _journalEntrys = value;
                RaisePropertyChanged("JournalEntrys");
            }
        }

        private App app = (Application.Current as App);

        public ObservableCollection<JournalEntryViewModel> GetJournalEntrys()
        {
            _journalEntrys = new ObservableCollection<JournalEntryViewModel>();
            using (var db = new SQLite.SQLiteConnection(app.DBPath))
            {
                var jes = db.Table<JournalEntry>().Select(x => x);
                foreach(var item in jes)
                {
                    var something = new JournalEntryViewModel();
                    something.Id = item.Id;
                    something.Title = item.Title;
                    something.Content = item.Content;
                    something.Date = item.date;
                    _journalEntrys.Add(something);

                }

            }
            return _journalEntrys;
        } 

    }
}
