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

        public Word(String word, String meaning, int index, int level) : base(word,meaning,index,level)
        {
        }

        public Word(String word, String meaning, int index, int level, Boolean learned) 
            : base(word,meaning,index,level,learned)
        {
        }

        public Word(String word, String meaning, int index, int level, Boolean learned, DateTime timelearned)
    : base(word, meaning, index, level, learned)
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

    }
}
