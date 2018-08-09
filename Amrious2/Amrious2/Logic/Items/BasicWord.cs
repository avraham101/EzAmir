using System;
using System.Collections.Generic;
using System.Text;

namespace Amrious2.Logic
{
    public class BasicWord 
    {
        protected DateTime wordLearnedDate;
        protected int level; //1 to 4. easy - hard
        protected char catagory; // first letter of the word in Upper
        protected int index; //the index of the word
        protected String word;
        protected String meaning;
        protected Boolean learned;
        
        public BasicWord(String word, String meaning,int index)
        {
            if (String.IsNullOrWhiteSpace(word) || String.IsNullOrWhiteSpace(meaning))
                throw new Exception("Word isn't valid");
            this.word = word;
            this.meaning = meaning;
            this.index = index;
            catagory = char.ToUpper(word[0]);
            level = 1;
            learned = false;
            wordLearnedDate = new DateTime(0,0,0);
        }

        public BasicWord(String word, String meaning, int index, int level)
        {
            if (String.IsNullOrWhiteSpace(word) || String.IsNullOrWhiteSpace(meaning)
                || level < 0 || level > 4)
                throw new Exception("Word or Level isn't valid");
            this.word = word;
            this.meaning = meaning;
            this.index = index;
            this.level = level;
            catagory = char.ToUpper(word[0]);
            learned = false;
            wordLearnedDate = new DateTime(0, 0, 0);
        }

        public BasicWord(String word, String meaning, int index, int level, Boolean learned)
        {
            if (String.IsNullOrWhiteSpace(word) || String.IsNullOrWhiteSpace(meaning)
                || level < 0 || level > 4)
                throw new Exception("Word or Level or Associations or learned isn't valid");
            this.word = word;
            this.meaning = meaning;
            this.index = index;
            this.level = level;
            this.learned = learned;
            catagory = char.ToUpper(word[0]);
            wordLearnedDate = new DateTime();
        }

        public BasicWord(BasicWord other)
        {
            if (other == null)
                throw new Exception("Other Word is null");
            level = other.level;
            catagory = other.catagory;
            index = other.index;
            word = other.word;
            meaning = other.meaning;
            learned = other.learned;
            wordLearnedDate = other.wordLearnedDate;
        }

        //Getters
        public String GetWord
        {
            get { return word; }
        }

        public String GetMeaning
        {
            get { return meaning; }
        }

        public Boolean IsWordLearned
        {
             get { return learned; }
             set { learned = value; }
        }

        public int GetLevel
        {
            get { return level; }
        }

        public int GetIndex
        {
            get { return index; }
        }

        public DateTime GetDate
        {
            get { return wordLearnedDate; }
        }
    }
}
