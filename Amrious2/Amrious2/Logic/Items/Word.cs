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

        /*public Word(String word, String meaning, int index, int level) : base(word,meaning,index,level)
        {
        }
        */

        public Word(String word, String meaning, int index, Boolean learned) 
            : base(word,meaning,index,learned)
        {
        }

        public Word(String word, String meaning, int index, Boolean learned, DateTime timelearned)
    : base(word, meaning, index, learned)
        {
            wordLearnedDate = timelearned;
        }

        public void WordMastered()
        {
            wordLearnedDate = DateTime.Now;
            learned = true;
        }

        public void WordForgot()
        {
            wordLearnedDate = new DateTime();
            learned = false;
        }

        public void SetIndex(int index)
        {
            if (index > 0)
                this.index = index;
        }

    }
}
