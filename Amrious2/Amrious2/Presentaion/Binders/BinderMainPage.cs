using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Xamarin.Forms;
using System.Runtime.CompilerServices;

namespace Amrious2.Presentaion
{
    public class BinderMainPage: INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
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

        public BinderMainPage()
        {
            _todolist = new ObservableCollection<ToDoListItem>();
            //default To Do List 
            _todolist.Add(new ToDoListItem("Learn Some Words"));
            _todolist.Add(new ToDoListItem("Read a Book for an hour"));
            _todolist.Add(new ToDoListItem("Do Some Unseens"));
            _todolist.Add(new ToDoListItem("Take Some Tests"));
            _todolist.Add(new ToDoListItem("Write Some Notes"));
            OnPropertyChanged("ToDoList");
        }

        //The Method For the binding, Property Changed
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
