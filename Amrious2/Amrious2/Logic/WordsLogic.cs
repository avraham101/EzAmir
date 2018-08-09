﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Amrious2.Logic
{
    public class WordsLogic
    {
        private enum State { Letter, Level, None};
        private State state;
        private List<Word>[] words; // Array of Lists of all the words by first Letter
        private List<Word>[] levels; // Array of Lists of all the words by level

        private char _letter; //picked letter in Upper Mode
        private int _level; // picked level

        private List<Word> _listPicked; //the list picked
        private int _listpickedindex;

        //constructor
        public WordsLogic()
        {
            Init();
        }
        
        //init vars
        public void Init()
        {
            state = State.None;
            _level = 1;
            _letter = 'A';
            words = new List<Word>[26]; //26 letters    
            levels = new List<Word>[4]; //4 levels
            _listPicked = null;
            _listpickedindex = 0;
            //need to call from presistent
            words[0] = new List<Word>();
            for (int i = 0; i < 4; i++)
                levels[i] = new List<Word>();
            for (int i = 0; i < 30; i++)
            {
                if(i%5==0)
                    words[0].Add(new Word("a" + (char)('a' + i), "" + i, i, i % 4 + 1, true, DateTime.Now));
                else
                    words[0].Add(new Word("a" + (char)('a' + i), "" + i, i, i % 4 + 1, false));
                levels[i % 4].Add(words[0][i]); //Must be pointer
            }
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

        public Catagory[] GetLevelCatagorys()
        {
            Catagory[] output = new Catagory[levels.Length];
            for (int i = 0; i < levels.Length; i++)
            {
                if (levels[i] != null)
                    output[i] = new Catagory(levels[i].Count, "Level " + (i + 1));
                else
                    output[i] = new Catagory(0, "Level" + (i + 1));
            }
            return output;
        }

        // get the picked state
        public String GetPickedName()
        {
            if (state == State.Letter)
                return "" + _letter;
            else if (state == State.Level)
                return "Level " + _level;
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
                _listPicked[_listpickedindex].WordForgot();
        }

        //change the current State and update the pick
        public Boolean PickLevel(int _level)
        {
            int indexofLevel = _level - 1;
            _listPicked = null;
            if (levels[indexofLevel] != null && levels[indexofLevel].Count > 0)
            {
                this._level = _level;
                state = State.Level;
                _listPicked = levels[indexofLevel];
                return true;
            }
            return false;
        }

        public Boolean PickLetter(char _letter)
        {
            int indexofLetter = (int)(_letter - 'A');
            _listPicked = null;
            if (words[indexofLetter] != null && words[indexofLetter].Count > 0)
            {
                this._letter = _letter;
                state = State.Letter;
                _listPicked = words[indexofLetter];
                return true;
            }
            return false;
        }

      }
}