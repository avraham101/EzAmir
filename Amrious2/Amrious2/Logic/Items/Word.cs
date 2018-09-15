using System;
using System.Collections.Generic;
using System.Text;

namespace Amrious2.Logic
{
    [Serializable]
    public class Word : BasicWord
    {
        //Builders
        public Word(String word, String meaning,int index) : base(word,meaning,index)
        {
        }

        public Word(String word, String meaning, int index, Boolean wordseen, Boolean learned) 
            : base(word,meaning,index,wordseen,learned)
        {
        }

        public Word(String word, String meaning, int index, Boolean wordseen, Boolean learned, DateTime timelearned)
    : base(word, meaning, index, wordseen,learned)
        {
            wordLearnedDate = timelearned;
        }

        public void WordMastered()
        {
            wordLearnedDate = DateTime.Now;
            status = State.Mastered;
        }

        public void WordUnMastered()
        {
            wordLearnedDate = new DateTime();
            status = State.NotMastered;
        }

        public void WordSeen()
        {//each time the word seen its goes to NotMaterd only if the word was already didntsee
            if(status==State.DidntSee)
                status = State.NotMastered;
        }

        public void SetIndex(int index)
        {
            if (index > 0)
                this.index = index;
        }

    }
}
