using DearDIary.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using DearDIary.ViewModels;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DearDIary
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private JournalEntrysViewModel _journalEntrysViewModel = null;
        private ObservableCollection<JournalEntryViewModel> journalEntrys = null; 
        public MainPage()
        {
            
            this.DataContext = this;
            this.InitializeComponent();
           
            
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            _journalEntrysViewModel = new JournalEntrysViewModel();
            journalEntrys = _journalEntrysViewModel.GetJournalEntrys();

            JournalEntryListview.ItemsSource = journalEntrys;
            
            
            base.OnNavigatedTo(e);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (TextContent.Text == "")
            {
                return;
            }
            else
            {
                var newEnter = new JournalEntryViewModel() { Content = TextContent.Text, Date = DateTime.Now };
                newEnter.Id = newEnter.GetNewJournalEntryId();
                newEnter.SaveJournalEntry(newEnter);
                journalEntrys.Add(newEnter);
                TextContent.Text = "";


            }
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {

        }
        private void JournalEntryListview_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {


            JournalEntryViewModel previous = (JournalEntryViewModel)JournalEntryListview.SelectedItem;
            

            TextContent.Text = previous.Content;
        }

    }
}
