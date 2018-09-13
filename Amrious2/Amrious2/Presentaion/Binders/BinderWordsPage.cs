using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using Amrious2.Logic;

namespace Amrious2.Presentaion
{
    public class BinderWordsPage: INotifyPropertyChanged
    {
        private WordsLogic logicer;

        private ObservableCollection<WordsGroupItem> _wordsgroups;
        public ObservableCollection<WordsGroupItem> WordsGroups
        {
            get
            {
                return _wordsgroups;
            }
            set
            {
                _wordsgroups = value;
                OnPropertyChanged("WordsGroups");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BinderWordsPage(WordsLogic logicer)
        {
            this.logicer = logicer;
            Catagory[] tletter = logicer.GetLetterCatagorys();
            _wordsgroups = new ObservableCollection<WordsGroupItem>();
            for (int i = 0; i < tletter.Length; i++)
                _wordsgroups.Add(new WordsGroupItem(tletter[i].GetName(), tletter[i].GetNumber()));
            OnPropertyChanged("WordsGroups");
        }

        //The Method For the binding, Property Changed
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
