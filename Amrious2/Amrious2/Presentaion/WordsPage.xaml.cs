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
        
        //constructor
        public WordsPage ()
		{
			InitializeComponent ();
            Init();
		}

        private void Init()
        {
            logicer = new WordsLogic();
            binder = new BinderWordsPage(logicer);
            this.BindingContext = binder;
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
                    if(logicer.PickLetter(letter))
                        MoveToLearnScreen();
                    else
                        DisplayAlert("Un avilable Path", "There is no words left", "Ok");
                }
            }
        }

        void LevelClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                String tmp = (sender as Button).Text;
                if (!String.IsNullOrWhiteSpace(tmp))
                {
                    //Level 1 6
                    if (tmp.Length > 7)
                        throw new Exception("Level invalid to many chars");
                    tmp = tmp.Substring(6, 1);
                    int level = Int32.Parse(tmp);
                    if (level < 1 || level > 4)
                        throw new Exception("Level invalid not between 1 to 4");
                    if (logicer.PickLevel(level))
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

    }
}