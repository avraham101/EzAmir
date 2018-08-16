using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using Amrious2.Logic;
using ExcelDataReader;

namespace UploadWords
{
    public class Program
    {
        private static String uml;// address where the Chat Room User's List found.
        private static String uml2;// address where the Chat Room History List found.

        //the main
        static void Main(string[] args)
        {
            //Word a = new Word("ofir noni", "אופיר נוני", 5);
            Console.InputEncoding = Encoding.GetEncoding(1255);
            Console.OutputEncoding = Encoding.GetEncoding(1255);
            CreateUmls();
            ReadExcelFolders();
            Console.ReadKey();
        }

        //function that create the umls and ready the files to work
        private static void CreateUmls()
        {
            uml = "Words.bin"; //The words for the application
            uml2 = "C:\\Users\\avrah\\Desktop\\Amir Project\\wordstest.xlsm"; //The words save in the computer folder or file
            CreateNewFiles();
        }

        private static void CreateNewFiles()
        {
            try
            {
                FileStream chanle1 = new FileStream(uml, FileMode.Append);
                FileStream chanle2 = new FileStream(uml2, FileMode.Append);
                chanle1.Close();
                chanle2.Close();
            }
            catch (Exception e)
            {
                throw new Exception("Files doesn't exits");
            }
        }

        //function that create the List<Word> from a folder
        private static List<Word> ReadExcelFolders()
        {
            List<Word> output = new List<Word>();
            string[] filePaths = Directory.GetFiles(@"C:\Users\avrah\Desktop\Amir Project\words\");
            foreach (String e in filePaths)
            {
                if(!e.Contains("not yet"))
                    output.AddRange(ReadExcelWords(e,output.Count));
            }
            Console.WriteLine("List of Words");
            foreach (Word w in output)
                Console.WriteLine(w.ToString());
            return output;
        }

        //function that create the List<Word> from a file
        private static List<Word> ReadExcelWords(String path,int counter)
        {
            List<Word> output = new List<Word>();
            try
            {
                String word, meaning;
                using (Stream stream = File.Open(path, FileMode.Open))
                {
                    IExcelDataReader reader;
                    reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    DataSet result = reader.AsDataSet();
                    DataTable dt = result.Tables[0];
                    for (int row = 1; row < dt.Rows.Count; row++)
                    {
                        word = "";
                        meaning = "";
                        for (int col = 0; col < dt.Columns.Count; col++)
                        {
                            if (col == 0)
                                word = dt.Rows[row][col].ToString();
                            else if(col ==1)
                                meaning = dt.Rows[row][col].ToString();
                        }
                        Word tmp = new Word(word, meaning, counter);
                        output.Add(tmp);
                        counter++;
                        //Console.WriteLine(tmp.ToString());
                    }
                    stream.Dispose();
                    Console.WriteLine("List updated >> " + path);
                }
            }
            catch (Exception e)
            {
                throw new Exception("Cant read Words from excel");
            }
            return output;
        }

        /*public List<User> ReadUsers()
        {
            try
            {
                using (Stream stream = File.Open(uml, FileMode.Open))
                {
                    // path, FileMode.Open, FileAccess.Write, FileShare.Read);
                    BinaryFormatter bin = new BinaryFormatter();
                    List<User> a = (List<User>)bin.Deserialize(stream);
                    stream.Dispose();
                    return a;
                }
            }
            catch (Exception e)
            {
                log.Error("Cant read User List bin", e);
            }
            return null;
        }

        /*public Boolean SaveListUsers(List<User> list)
            {
                try
                {
                    using (Stream stream = File.OpenWrite(uml))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, list);
                        stream.Close();
                    }
                }
                catch
                {
                    log.Error("Cant save User List bin");
                    return false;
                }
                return true;
            }

        public Boolean SaveHistory(List<Message> list)
            {
                try
                {
                    using (Stream stream = File.OpenWrite(uml2))
                    {
                        BinaryFormatter bin = new BinaryFormatter();
                        bin.Serialize(stream, list);
                        stream.Close();
                    }
                }
                catch
                {
                    log.Error("Cant save History List bin");
                    return false;
                }
                return true;
            }

        */
        }
}
