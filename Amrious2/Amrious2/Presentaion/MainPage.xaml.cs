using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using Amrious2.Presentaion;

namespace Amrious2
{
	public partial class MainPage : ContentPage, INotifyPropertyChanged
    {
        private BinderMainPage binder;
        //constructor
        public MainPage()
		{
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            binder = new BinderMainPage();
            this.BindingContext = binder;
        }

        void WordsClick(object sender, EventArgs e)
        {
            Navigation.PushAsync(new WordsPage());
        }

        void ToDoListItemButton(object sender, EventArgs e)
        {
            if (sender == null)
                return;
            Button tmp = sender as Button;
            if(tmp.BackgroundColor.Equals(ToDoListItem.blueFalse))
            {
                tmp.BackgroundColor = ToDoListItem.blueTrue;
                tmp.Text = "✓";
            }
            else
            {
                tmp.BackgroundColor = ToDoListItem.blueFalse;
                tmp.Text = "x";
            }
        }
        
        void UnderBulidingClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                DisplayAlert("Not Avilable", "Under construction","Back");
            }
        }
    }
}
