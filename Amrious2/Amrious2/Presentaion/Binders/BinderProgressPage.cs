using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
namespace Amrious2.Presentaion
{
    public class BinderProgressPage : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //Properties to what to see
        private Boolean showNumberWords;
        public Boolean ShowNumberWords
        {
            get { return showNumberWords; }
            set { showNumberWords = value;
                OnPropertyChanged("ShowNumberWords");
            }
        }

        private Boolean showNumberWordsMastered;
        public Boolean ShowNumberWordsMastered
        {
            get { return showNumberWordsMastered; }
            set
            {
                showNumberWordsMastered = value;
                OnPropertyChanged("ShowNumberWordsMastered");
            }
        }

        private Boolean showNumberWordsUnMastered;
        public Boolean ShowNumberWordsUnMastered
        {
            get { return showNumberWordsUnMastered; }
            set
            {
                showNumberWordsUnMastered = value;
                OnPropertyChanged("ShowNumberWordsUnMastered");
            }
        }

        private Boolean showNumberWordsSeen;
        public Boolean ShowNumberWordsSeen
        {
            get { return showNumberWordsSeen; }
            set
            {
                showNumberWordsSeen = value;
                OnPropertyChanged("ShowNumberWordsSeen");
            }
        }

        private Boolean showNumberWordsUnSeen;
        public Boolean ShowNumberWordsUnSeen
        {
            get { return showNumberWordsUnSeen; }
            set
            {
                showNumberWordsUnSeen = value;
                OnPropertyChanged("ShowNumberWordsUnSeen");
            }
        }


        private ObservableCollection<ToDoListItem> _todolist;
        public ObservableCollection<ToDoListItem> ToDoList
        {
            get
            {
                return _todolist;
            }
            set
            {
                _todolist = value;
                OnPropertyChanged("ToDoList");
            }
        }

        public BinderProgressPage()
        {

        }

        private void Ipos()
        {
            ShowNumberWords = true;
            ShowNumberWordsMastered = true;
            ShowNumberWordsSeen = true;
            ShowNumberWordsUnMastered = true;
            ShowNumberWordsUnSeen = true;
        }

        //The Method For the binding, Property Changed
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
