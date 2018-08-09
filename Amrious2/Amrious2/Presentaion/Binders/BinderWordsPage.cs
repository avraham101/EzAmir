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

        private ObservableCollection<WordsGroupItem> _wordslevels;
        public ObservableCollection<WordsGroupItem> WordsLevels
        {
            get
            {
                return _wordslevels;
            }
            set
            {
                _wordslevels = value;
                OnPropertyChanged("WordsLevels");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public BinderWordsPage(WordsLogic logicer)
        {
            this.logicer = logicer;
            Catagory[] tletter = logicer.GetLetterCatagorys();
            Catagory[] tlevel = logicer.GetLevelCatagorys();
            _wordsgroups = new ObservableCollection<WordsGroupItem>();
            _wordslevels = new ObservableCollection<WordsGroupItem>();
            for (int i = 0; i < tletter.Length; i++)
                _wordsgroups.Add(new WordsGroupItem(tletter[i].GetName(), tletter[i].GetNumber()));
            for (int i = 0; i < tlevel.Length; i++)
                _wordslevels.Add(new WordsGroupItem(tlevel[i].GetName(), tlevel[i].GetNumber()));
            OnPropertyChanged("WordsGroups");
            OnPropertyChanged("WordsLevels");
        }

        //The Method For the binding, Property Changed
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
