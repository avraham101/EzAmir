using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Amrious2.Presistent;
namespace Amrious2.Logic
{
    public class WordsLogic
    {
        private enum State { Letter, AllWords, None};
        private enum Fillter { AllWords,KnownWords, UnknownWords, NewWords}
        private enum Sorter { Alphabetical, Mixed}
        private enum ActionToWord { Seen, Mastered, UnMastered}

        private WordsKepper kepper;
        private State state;
        private Fillter fillter;
        private Sorter sorter;

        private List<Word>[] words; // Array of Lists of all the words by first Letter
        private char _letter; //picked letter in Upper Mode
        private List<Word> _listPicked; //the list picked: 
        //we can changed it but the changes need to be saved in the words
        
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
            sorter = Sorter.Alphabetical;
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
            if (_listPicked == null || _listPicked.Count == 1)
                //return new BasicWord("No More Words Left", "Congratulations", 1, false);
                throw new Exception("Picked List is empty cant Get Words");
            else if (_listpickedindex > _listPicked.Count | _listpickedindex < 0)
                throw new Exception("Index Picked isn't valid");
            if (!_listPicked[_listpickedindex].IsWordSeen)
            {
                _listPicked[_listpickedindex].WordSeen();
                MarkWordInOriginalList(_listPicked[_listpickedindex].GetWord,ActionToWord.Seen);
            }
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
                        _listPicked[i].WordSeen();
                        MarkWordInOriginalList(_listPicked[i].GetWord, ActionToWord.Seen);
                        return _listPicked[i];
                    }
            return null;
        }

        //Mark the word from the original list words to seen
        private void MarkWordInOriginalList(String word,ActionToWord act)
        {
            if (String.IsNullOrWhiteSpace(word))
                throw new Exception("word isnt correct");
            int indexofLetter = -1;
            switch (state)
            {
                case State.AllWords: indexofLetter = (int)('A' - word.ToUpper()[0]); break;
                case State.Letter: indexofLetter = (int)(_letter - 'A'); break;
            }
            if (indexofLetter < 0)
                throw new Exception("Found it. It is the letter index wrong");
            if (words[indexofLetter] != null && words[indexofLetter].Count > 0)
                for (int i = 0; i < words[indexofLetter].Count; i++)
                    if (words[indexofLetter][i].GetWord.Equals(word))
                    {
                        switch(act)
                        {
                            case ActionToWord.Seen:
                                words[indexofLetter][i].WordSeen(); break;
                            case ActionToWord.Mastered:
                                words[indexofLetter][i].WordMastered(); break;
                            case ActionToWord.UnMastered:
                                words[indexofLetter][i].WordUnMastered();
                                break;
                        }
                    }
                        
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
            {
                _listPicked[_listpickedindex].WordMastered();
                MarkWordInOriginalList(_listPicked[_listpickedindex].GetWord, ActionToWord.Mastered);   
            }
            else
            {
                _listPicked[_listpickedindex].WordUnMastered();
                    MarkWordInOriginalList(_listPicked[_listpickedindex].GetWord, ActionToWord.UnMastered);
            }
            
        }
        
        //Change the picked letter
        public Boolean PickLetter(char _letter, String _fillter, String _sorter)
        {
            int indexofLetter = (int)(_letter - 'A');
            _listPicked = new List<Word>();
            if (words[indexofLetter] != null && words[indexofLetter].Count > 0)
            {
                this._letter = _letter;
                //_listPicked = words[indexofLetter];
                foreach (Word e in words[indexofLetter])
                    _listPicked.Add(e);
                state = State.Letter;
                UpdateFillter(_fillter);
                UpdateSorter(_sorter);
                if (_listPicked.Count < 1)
                    return false;
                return true;
            }
            return false;
        }

        //the function return all the words avalible
        public Boolean PickAllWords(String _fillter,String _sorter)
        {
            List<Word> output = new List<Word>();
            int counter = 1;
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
            UpdateSorter(_sorter);
            if (_listPicked.Count < 1)
                return false;
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

        //The function update the Sorter from a choosen string
        private void UpdateSorter(string sort)
        {
            if (sort != null)
            {
                switch (sort)
                {
                    case "Alphabetical": sorter = Sorter.Alphabetical; break;
                    case "Mixed": sorter = Sorter.Mixed; break;
                    default: sorter = Sorter.Alphabetical; break;
                }
                RunSorter();
            }
        }

        //The Function Run the sorter chosen on the picked List
        private void RunSorter()
        {
            if (_listPicked != null && _listPicked.Count > 0)
            {
                switch (sorter)
                {
                    case Sorter.Alphabetical: _listPicked = SorterAlphabetic(); break;
                    case Sorter.Mixed: _listPicked = SorterMixed(); break;
                }
            }
        }

        //the Function Return Mix List
        private List<Word> SorterMixed()
        {
            List<Word> output = new List<Word>();
            Random r = new Random();
            int tmp = 0;//,counter=0;
            while (_listPicked.Count>0)
            {
                tmp = r.Next(0, _listPicked.Count-1);
                Word add = _listPicked[tmp];
                //add.SetIndex(counter);
                output.Add(add);
                _listPicked.RemoveAt(tmp);
                //counter++;
            }
            return output;
        }

        //the function Return The List OrderBy Alpabatic
        private List<Word> SorterAlphabetic()
        {
            List<Word> output = new List<Word>();
            output = _listPicked.OrderBy(e => e.GetWord).ToList();
            return output;
        }
    }
}
