using System;
using System.Collections.Generic;
using System.Text;

namespace Amrious2.Logic
{
    [Serializable]
    public class BasicWord 
    {
        protected enum State { Mastered, NotMastered, DidntSee} //the state of the word
        protected String word; //the word
        protected String meaning; //the meaning
        protected char catagory; // first letter of the word in Upper
        protected int index; //the index of the word 
        protected State status; //status of the word
        protected DateTime wordLearnedDate; //the date the word learned 

        public BasicWord(String word, String meaning,int index)
        {
            if (String.IsNullOrWhiteSpace(word) || String.IsNullOrWhiteSpace(meaning))
                throw new Exception("Word isn't valid");
            this.word = RemoveAllSpaces(word);
            this.meaning = meaning;
            this.index = index;
            catagory = char.ToUpper(this.word[0]);
            status = State.DidntSee;
            wordLearnedDate = new DateTime();
        }

        public BasicWord(String word, String meaning, int index, Boolean wordseen, Boolean learned)
        {
            if (String.IsNullOrWhiteSpace(word) || String.IsNullOrWhiteSpace(meaning))
                throw new Exception("Word or Associations or learned isn't valid");
            this.word = RemoveAllSpaces(word);
            this.meaning = meaning;
            if (wordseen & learned)
                status = State.Mastered;
            else if (wordseen & !learned)
                status = State.NotMastered;
            else
                status = State.DidntSee;
            catagory = char.ToUpper(this.word[0]);
            wordLearnedDate = new DateTime();
        }

        public BasicWord(BasicWord other)
        {
            if (other == null)
                throw new Exception("Other Word is null");
            catagory = other.catagory;
            index = other.index;
            word = RemoveAllSpaces(other.word);
            meaning = other.meaning;
            status = other.status;
            wordLearnedDate = other.wordLearnedDate;
        }

        //the function remove all spaces from a string
        private String RemoveAllSpaces(String value)
        {
            if (String.IsNullOrWhiteSpace(value))
                throw new Exception("the string value is null or empty");
            int counter = 0;
            //remove spacing from the start
            for (int i=0;i<value.Length;i++)
            {
                string a = ""+value[i];
                if (string.IsNullOrWhiteSpace(a))
                    counter++;
                else 
                    break;
            }
            value = value.Substring(counter);
            //remove spasing from the end --missing
            return value;
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
             get { return (status==State.Mastered); }
        }
        
        public Boolean IsWordSeen
        {
            get { return (status != State.DidntSee); }
        }

        public int GetIndex
        {
            get { return index; }
        }

        public DateTime GetDate
        {
            get { return wordLearnedDate; }
        }

        public Char GetCatagory
        {
            get { return catagory; }
        }

        public override String ToString()
        {
            return word + "(" + index +"/"+catagory+ ") " + " : " + meaning;
        }

    }
}
