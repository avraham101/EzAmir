using Amrious2.Logic;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace Amrious2.Presistent
{
    public class WordsKepper
    {
        private List<Word>[] arraylist;
        //builder
        public WordsKepper()
        {
            ReadListOfWords();
        }

        //the function Read The data from Assents Android and Check if something not broken
        //in the future need to insure which platform we work on android,ios,windows
        private void ReadListOfWords()
        {
            arraylist = new List<Word>[26];
            try
            {
                arraylist = Amrious2.Droid.MainActivity.list;
            }
            catch (Exception e)
            {
                for (int i = 0; i < arraylist.Length; i++)
                {
                    arraylist[i] = new List<Word>();
                    arraylist[i].Add(new Word("Cant create the class", "Fail", 1));
                }
            }
        }

        //The function return the List from the Assents
        public List<Word>[] GetArrayWordList()
        {
            return arraylist;
        }

        //the function get index in the Array and Return the list from it
        public List<Word> GetWordList(int index)
        {
            List<Word> output = new List<Word>();
            try
            {
                //output = Amrious2.Droid.MainActivity.list;
                if (arraylist != null)
                    output = arraylist[index];
                else
                    throw new Exception("Somthing Broken");
            }
            catch ( Exception e)
            {
                output = new List<Word>();
                output.Add(new Word("Cant create the class", "Fail", 1));
            }
            return output;
        }


    }
}
