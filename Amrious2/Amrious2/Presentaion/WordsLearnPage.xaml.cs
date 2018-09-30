using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Amrious2.Logic;

namespace Amrious2.Presentaion
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WordsLearnPage : ContentPage
	{
        private WordsLogic logicer;
        private BinderWordLearning binder;
        //builder
        public WordsLearnPage (WordsLogic logicer)
		{
			InitializeComponent();
            this.logicer = logicer;
            Init();
		}
        
        //ipos varibels
        private void Init()
        {
            logicer.ResetIndex();
            binder = new BinderWordLearning(logicer);
            this.BindingContext = binder;
        }

        //List Mode On
        void ListClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                logicer.ResetIndex();
                binder.SetListView();
            }
        }

        //Individual Mode On
        void IndividualClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                logicer.ResetIndex();
                binder.SetIndivitaulView();
            }
        }

        //Individual Mode: Flip to the meaning
        void WordIndividualItemClick(object sender, EventArgs e)
        {
            if(sender!=null)
                binder.TimerStart();
        }

        //Individual Mode:Next Word
        void WordIndividualNextClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                binder.NextIndivitaulWord();
            }
        }
        
        //Individual Mode:Prev Word
        void WordIndividualPrevClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                binder.PrevIndivitaulWord();
            }
        }

        //List Mode: show meaning in a pop up
        void WordListItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (sender != null)
            {
                logicer.GetWord((e.SelectedItem as BasicWord).GetWord); //here the function ment to update the index in the logic layer
                binder.SelectListItem(e.SelectedItem as BasicWord);
                binder.ListCellOptionsVisibilty = true;
            }
        }

        //List Mode/Indivitual Mode: Word Mastered
        void WordMastered(object sender, EventArgs e)
        {
            if(sender!=null)
            {
                logicer.WordMastered(true);
                binder.PickWordStatus(true);
            }
        }

        //List Mode/Indivitual Mode: Word Not Learned
        void WordForgot(object sender, EventArgs e)
        {
            if (sender != null)
            {
                logicer.WordMastered(false);
                binder.PickWordStatus(false);
            }
        }

        //need to be delleted
        void WordListItemClick(object sender, EventArgs e)
        {
            if (sender != null)
            {
                String tmp = (sender as Button).Text;
                DisplayAlert(tmp, logicer.GetWord(tmp).GetMeaning, "✔", "❌");
            }
        }

    }
}