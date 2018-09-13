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
using System.Timers;

namespace Amrious2.Presentaion
{
    public class BinderWordLearning : INotifyPropertyChanged
    {
        private WordsLogic logicer; //Logic object

        //the title of the Screen
        private String _title; 
        public String Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
            }
        }

        //Visibilty to List Of Words
        private Boolean _individualvisibilty;
        public Boolean IndividualVisibilty
        {
            get { return _individualvisibilty; }
            set
            {
                _individualvisibilty = value;
                OnPropertyChanged("IndividualVisibilty");
            }
        }

        //Visibilty to List Of Words
        private Boolean _wordslistvisibilty;
        public Boolean WordListVisibility
        {
            get { return _wordslistvisibilty; }
            set
            {
                _wordslistvisibilty = value;
                OnPropertyChanged("WordListVisibility");
            }
        }
    
        //List Of Words Option Varibels
        private ObservableCollection<BasicWord> _wordslist; 
        public ObservableCollection<BasicWord> WordsList
        {
            get
            {
                return _wordslist;
            }
            set
            {
                _wordslist = value;
                OnPropertyChanged("WordsList");
            }
        }
        private Boolean _listcelloptionsvisibilty;
        public Boolean ListCellOptionsVisibilty
        {
            get { return _listcelloptionsvisibilty; }
            set
            {
                _listcelloptionsvisibilty = value;
                OnPropertyChanged("ListCellOptionsVisibilty");
            }
        }
        
        //Indivitual Option Varibels
        private Boolean _indivitualwordvisibilty;
        private Boolean _indivitualmeaningvisibilty;
        public Boolean IndivitualWordVisibilty
        {
            get { return _indivitualwordvisibilty; }
            set
            {
                _indivitualwordvisibilty = value;
                OnPropertyChanged("IndivitualWordVisibilty");
            }
        }
        public Boolean IndivitualMeaningdVisibilty
        {
            get { return _indivitualmeaningvisibilty; }
            set
            {
                _indivitualmeaningvisibilty = value;
                OnPropertyChanged("IndivitualMeaningdVisibilty");
            }
        }

        //Picked Word var
        private int _pickedWordIndex;
        private String _pickedwordName;
        public String PickedWordName
        {
            get { return _pickedwordName; }
            set
            {
                _pickedwordName = value;
                OnPropertyChanged("PickedWordName");
            }
        }
        private String _pickedwordMeaning;
        public String PickedWordMeaning
        {
            get { return _pickedwordMeaning; }
            set
            {
                _pickedwordMeaning = value;
                OnPropertyChanged("PickedWordMeaning");
            }
        }
        private String _pickedwordindex;
        public String PickedWordIndex
        {
            get { return _pickedwordindex; }
            set
            {
                _pickedwordindex = value;
                OnPropertyChanged("PickedWordIndex");
            }
        }
        private String _pickedWordlevel;
        public String PickedWordlevel
        {
            get { return _pickedWordlevel; }
            set
            {
                _pickedWordlevel = value;
                OnPropertyChanged("PickedWordlevel");
            }
        }
        private Boolean _pickedWordLearned;
        public Boolean PickedWordLearned
        {
            get { return _pickedWordLearned; }
            set
            {
                _pickedWordLearned = value;
                OnPropertyChanged("PickedWordLearned");
            }
        }
        private String _pickedWordLearnedDate;
        public String PickedWordLearnedDate
        {
            get { return _pickedWordLearnedDate; }
            set { _pickedWordLearnedDate = value;
                OnPropertyChanged("PickedWordLearnedDate");
            }
        }

        //Flip Varbiles
        private int _rotaionx;
        public int RotationXWord
        {
            get { return _rotaionx; }
            set
            {
                _rotaionx = value;
                OnPropertyChanged("RotationXWord");
                OnPropertyChanged("RotationXMeaning");
            }
        }
        public int RotationXMeaning
        {
            get { return _rotaionx+180; }
            set
            {
                _rotaionx = value-180;
                OnPropertyChanged("RotationXWord");
                OnPropertyChanged("RotationXMeaning");
            }
        }
        //Timer Varibles for Flip
        private Timer _rotaiontimer;
        private void SetTimer()
        {
            _rotaiontimer = new Timer(5);
            _rotaiontimer.Elapsed += RotaionTimerTick;
            _rotaiontimer.Enabled = false;
        }
        public void TimerStart()
        {
            if (_rotaiontimer != null && _rotaiontimer.Enabled!=true)
                _rotaiontimer.Enabled = true;
        }
        private void RotaionTimerTick(Object source, ElapsedEventArgs e)
        {
            if (RotationXWord == 90)
            {
                IndivitualWordVisibilty = false;
                IndivitualMeaningdVisibilty = true;
            }
            else if (RotationXWord == 180)
                _rotaiontimer.Stop();
            else if (RotationXWord == 270)
            {
                IndivitualWordVisibilty = true;
                IndivitualMeaningdVisibilty = false;
            }
            else if (RotationXWord == 360)
            {
                RotationXWord = 0;
                _rotaiontimer.Stop();
            }
            RotationXWord = RotationXWord + 1;
        }
        
        //Propoerty Changed vars
        public event PropertyChangedEventHandler PropertyChanged;
        
        //Constructor
        public BinderWordLearning(WordsLogic logicer)
        {
            this.logicer = logicer;
            _title = logicer.GetPickedName(); //the name of the screen
            //ipos picked word
            _pickedWordIndex = 0;
            PickedWordName = "";
            PickedWordIndex = "/";
            PickedWordlevel = "";
            PickedWordLearned = false;
            //set the vars
            IndividualVisibilty = true;
            SetListView();
            SetTimer();
            //create the words List
            _wordslist = new ObservableCollection<BasicWord>();
            UpdateListOfWords();
        }
            
        //set the list view
        public void SetListView()
        {
            if (IndividualVisibilty)
            {
                IndividualVisibilty = false;
                ListCellOptionsVisibilty = false;
                WordListVisibility = true;
            }
        }

        //set the Indivitaul view
        public void SetIndivitaulView()
        {
            if (!IndividualVisibilty)
            {
                IndivitaulViewIpos();
                UpdateWordPicked(logicer.GetWord());
            }
        }

        //Ipos for Indivitaul View
        private void IndivitaulViewIpos()
        {
            _rotaiontimer.Stop();
            RotationXWord = 0;
            IndivitualWordVisibilty = true;
            IndivitualMeaningdVisibilty = false;
            IndividualVisibilty = true;
            WordListVisibility = false;
            ListCellOptionsVisibilty = false;
        }

        //Update Word Picked
        private void UpdateWordPicked(BasicWord tmp)
        {
            PickedWordName = tmp.GetWord;
            PickedWordMeaning = tmp.GetMeaning;
            PickedWordIndex = logicer.GetIndexWord() + "/" + _wordslist.Count;//logicer.GetIndexWord() + "/" + _wordslist.Count;
            _pickedWordIndex = tmp.GetIndex;
            PickedWordLearned = tmp.IsWordLearned;
            PickedWordLearnedDate = tmp.GetDate.ToLongDateString();
        }

        //Next Word for Indivitaul View
        public void NextIndivitaulWord()
        {
            UpdateWordPicked(logicer.GetNextWord());
            IndivitaulViewIpos();
        }

        //Prev Word for Indivitaul View
        public void PrevIndivitaulWord()
        {
            UpdateWordPicked(logicer.GetPrevWord());
            IndivitaulViewIpos();
        }

        //List Mode: Update the Picked Word
        public void SelectListItem(BasicWord e)
        {
            UpdateWordPicked(e);
        }

        //Picked Word Update
        public void PickWordStatus(Boolean status)
        {
            _wordslist[_pickedWordIndex].IsWordLearned = status;
            logicer.WordMastered(status);
            PickedWordLearned = status;
            if (status)
                PickedWordLearnedDate = DateTime.Now.ToString();
            else
                PickedWordLearnedDate = "No Date";
        }
        
        //Update the Observeble List
        private void UpdateListOfWords()
        {
            foreach (BasicWord e in logicer.GetListOfWords())
                _wordslist.Add(e);
        }

        //The Method For the binding, Property Changed
        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
