using System;
using System.Collections.Generic;
using System.Text;
using Amrious2.Presistent;
namespace Amrious2.Logic
{
    public class WordsLogic
    {
        private enum State { Letter, AllWords, None};
        private enum Fillter { AllWords,KnownWords, UnknownWords, NewWords}

        private WordsKepper kepper;
        private State state;
        private Fillter fillter;

        private List<Word>[] words; // Array of Lists of all the words by first Letter
        private char _letter; //picked letter in Upper Mode
        private List<Word> _listPicked; //the list picked
        
        private int _listpickedindex;//the index of the word in the list
        
        //constructor
        public WordsLogic()
        {
            Init();
        }
        
        //init vars
        public void Init()
        {
            state = State.None;
            fillter = Fillter.AllWords;
            _letter = 'A';
            _listPicked = null;
            _listpickedindex = 0;
            kepper = new WordsKepper();
            words = kepper.GetArrayWordList(); //26 letters        
        }

        //get the catagories
        public Catagory[] GetLetterCatagorys()
        {
            Catagory[] output = new Catagory[words.Length];
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i] != null)
                    output[i] = new Catagory(words[i].Count, "" + (char)('A' + i));
                else
                    output[i] = new Catagory(0, "" + (char)('A' + i));
            }
            return output;
        }

        // get the picked state
        public String GetPickedName()
        {
            if (state == State.Letter)
                return "" + _letter;
            else if (state == State.AllWords)
                return "All words";
            return null;
        }

        // get list of word by the state and pick
        public List<BasicWord> GetListOfWords()
        {
            List<BasicWord> output = new List<BasicWord>();
            if (state == State.None)
                throw new Exception("Cant get List Of Words without the correct State");
            else
            {
                if(_listPicked!= null && _listPicked.Count>0)
                {
                    foreach (Word e in _listPicked)
                        output.Add(e);
                }
            }
            return output;
        }

        //get word from the index picked
        public BasicWord GetWord()
        {
            if (_listPicked == null || _listPicked.Count < 1)
                //return new BasicWord("No More Words Left", "Congratulations", 1, false);
                throw new Exception("Picked List is empty cant Get Words");
            else if (_listpickedindex > _listPicked.Count | _listpickedindex < 0)
                throw new Exception("Index Picked isn't valid");
            return _listPicked[_listpickedindex];
        }

        //get word meaning from new index
        public BasicWord GetWord(String word)
        {
            if (String.IsNullOrWhiteSpace(word))
                throw new Exception("word isnt correct");
            if (_listPicked != null && _listPicked.Count > 0)
                for (int i = 0; i < _listPicked.Count; i++)
                    if (_listPicked[i].GetWord.Equals(word))
                    {
                        _listpickedindex = i;
                        return _listPicked[i];
                    }
            return null;
        }

        //get the next Word
        public BasicWord GetNextWord()
        {
            NextWord();
            return GetWord();
        }

        //get the prev Word
        public BasicWord GetPrevWord()
        {
            PrevWord();
            return GetWord();
        }

        //set to the next Word
        private void NextWord()
        {
            if (_listPicked != null && _listPicked.Count > 0)
            {
                _listpickedindex++;
                if (_listpickedindex > _listPicked.Count-1)
                    _listpickedindex = 0; //first var
            }
        }
        
        //set to the prev Word
        private void PrevWord()
        {
            if (_listPicked != null && _listPicked.Count > 0)
            {
                _listpickedindex--;
                if (_listpickedindex < 0)
                    _listpickedindex = _listPicked.Count - 1; //last var
            }
        }

        //get the index of the word
        public int GetIndexWord()
        {
            if (_listpickedindex < 0 || _listpickedindex > _listPicked.Count)
                throw new Exception("Index Out Of Bonds");
            return _listpickedindex+1;
        } 

        //ipos the index
        public void ResetIndex()
        {
            _listpickedindex = 0;
        }

        //Change word Status of the picked: Mastered and unMastered
        public void WordMastered(Boolean status)
        {
            if (status)
                _listPicked[_listpickedindex].WordMastered();
            else
                _listPicked[_listpickedindex].WordUnMastered();
        }
        
        //Change the picked letter
        public Boolean PickLetter(char _letter, String _fillter)
        {
            int indexofLetter = (int)(_letter - 'A');
            _listPicked = null;
            if (words[indexofLetter] != null && words[indexofLetter].Count > 0)
            {
                this._letter = _letter;
                _listPicked = words[indexofLetter];
                state = State.Letter;
                UpdateFillter(_fillter);
                return true;
            }
            return false;
        }

        //the function return all the words avalible
        public Boolean PickAllWords(String _fillter)
        {
            List<Word> output = new List<Word>();
            int counter = 0;
            foreach (List<Word> tmp in words)
            {
                if (tmp != null && tmp.Count > 0)
                    foreach (Word w in tmp)
                    {
                        w.SetIndex(counter);
                        output.Add(w);
                        counter++; 
                    }
            }
            _listPicked = output;
            state = State.AllWords;
            UpdateFillter(_fillter);
            return true;
        }

        //The function update the Fillter from a choosen string
        private void UpdateFillter(String filt)
        {
            if(filt!=null)
            {
                switch(filt)
                {
                    case "All Words": fillter = Fillter.AllWords; break;
                    case "Known Words": fillter = Fillter.KnownWords; break;
                    case "Unknown Words": fillter = Fillter.UnknownWords; break;
                    case "New Words": fillter = Fillter.NewWords; break;
                    default: fillter = Fillter.AllWords; break;
                }
                RunFillter();
            }
        }

        //The Function Run the fillter chosen on the picked List
        private void RunFillter()
        {
            if (_listPicked != null && _listPicked.Count > 0)
            {
                switch (fillter)
                {
                    case Fillter.AllWords: break; //need to put her a reset to the list or only its one way to go?
                    case Fillter.KnownWords: _listPicked = FillterKnownWords(); break;
                    case Fillter.NewWords: _listPicked = FillterNewWords(); break;
                    case Fillter.UnknownWords: _listPicked = FillterUnKnownWords(); break;
                }
                if (_listPicked.Count < 1)
                    _listPicked.Add(new Word("No Words Left", "No Words Left", 1));
            }
        }

        //The Function return a List filltered By Known Words
        private List<Word> FillterKnownWords()
        {
            List<Word> output = new List<Word>();
            if(_listPicked!=null && _listPicked.Count>0)
            {
                foreach(Word w in _listPicked)
                {
                    if (w.IsWordLearned)
                        output.Add(w);
                }
            }
            return output;
        }

        //The function return a List filltered By UnKnown Words
        private List<Word> FillterUnKnownWords()
        {
            List<Word> output = new List<Word>();
            if (_listPicked != null && _listPicked.Count > 0)
            {
                foreach (Word w in _listPicked)
                {
                    if (!w.IsWordLearned & w.IsWordSeen)
                        output.Add(w);
                }
            }
            return output;
        }

        //The function return a List filltered By New Words
        private List<Word> FillterNewWords()
        {
            List<Word> output = new List<Word>();
            if (_listPicked != null && _listPicked.Count > 0)
            {
                foreach (Word w in _listPicked)
                {
                    if (!w.IsWordSeen)
                        output.Add(w);
                }
            }
            return output;
        }
    }
}
