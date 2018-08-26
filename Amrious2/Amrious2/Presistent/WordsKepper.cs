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
        private String uml;
        public WordsKepper()
        {
            uml = "words.bin";
        }

        public List<Word> GetWordList()
        {
            List<Word> output = new List<Word>();
            try
            {
                using (Stream stream = File.Open(uml, FileMode.Open))
                {
                    // path, FileMode.Open, FileAccess.Write, FileShare.Read);
                    BinaryFormatter bin = new BinaryFormatter();
                    output = (List<Word>)bin.Deserialize(stream);
                    stream.Dispose();
                }
            }
            catch (Exception e)
            {
                //log.Error("Cant read User List bin", e);
                output.Add(new Word("Fail! Fail! Fail!","Didnt read the words.bin",1000));
            }
            return output;
        }
    }
}
