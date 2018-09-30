using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amrious2.Presentaion;
using Amrious2.Logic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Amrious2
{
	public partial class WordsPage : ContentPage
	{
        private BinderWordsPage binder;
        private WordsLogic logicer;
        private String fillter;
        private String sorter;
        
        //constructor
        public WordsPage ()
		{
			InitializeComponent();
            Init();
		}

        private void Init()
        {
            logicer = new WordsLogic();
            binder = new BinderWordsPage(logicer);
            this.BindingContext = binder;
            fillter = "I am Fillter";
            sorter = "I am Sorter";
        }

        //Click Events
        void LetterClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                String tmp = (sender as Button).Text;
                if(!String.IsNullOrWhiteSpace(tmp))
                {
                    if (tmp.Length > 1)
                        throw new Exception("Letter invalid more then 1 char");
                    char letter = char.ToUpper(tmp[0]);
                    if(logicer.PickLetter(letter,fillter,sorter))
                        MoveToLearnScreen();
                    else
                        DisplayAlert("Un avilable Path", "There is no words left", "Ok");
                }
            }
        }

        //move to the next screen
        private void MoveToLearnScreen()
        {
            Navigation.PushAsync(new WordsLearnPage(logicer));
        }

        //go back to the manue 
        void BackClick(object sender, EventArgs e)
        {
            Navigation.PopAsync();
        }

        //pick all words and move to the next screen
        void AllWordsClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                if (logicer.PickAllWords(fillter,sorter))
                    MoveToLearnScreen();
            }
        }

        //pick progress and move to the next screen
        void ProgressClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                Navigation.PushAsync(new ProgressPage(logicer));
            }
        }

        //the function update the fillter
        //note; first it update the presentaion localy After he moved screen it update the app
        void ChangedFillter(object sender, EventArgs e)
        {
            if(sender!=null)
            {
                fillter = (sender as Picker).SelectedItem as String;
            }
        }

        //the function update the sorter
        //note; first it update the presentaion localy After he moved screen it update the app
        void ChangedSorter(object sender, EventArgs e)
        {
            if (sender != null)
            {
                sorter = (sender as Picker).SelectedItem as String;
            }
        }
    }
}