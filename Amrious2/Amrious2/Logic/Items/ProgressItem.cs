using System;
using System.Collections.Generic;
using System.Text;

namespace Amrious2.Logic.Items
{
    //the class represent a progress item for the bars
    public class ProgressItem
    {
        private int words;
        private int wordsmastered;
        private int wordsunmastered;
        private int seen;

        public ProgressItem(int words,int wordsmastered,int wordsunmastered,int seen)
        {
            this.words = words;
            this.wordsmastered = wordsmastered;
            this.wordsunmastered = wordsunmastered;
            this.seen = seen;
        }

        //Gettets
        public int Words
        {
            get { return words; }
        }

        public int WordsMastered
        {
            get { return wordsmastered; }
        }

        public int WordsUnmastered
        {
            get { return wordsunmastered; }
        }

        public int WordsSeen
        {
            get { return seen; }
        }
    }
}
