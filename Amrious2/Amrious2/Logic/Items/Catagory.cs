using System;
using System.Collections.Generic;
using System.Text;

namespace Amrious2.Logic
{
    //object between presentaion and logic, so the presentation will know less inforamtaion then it needs
    public class Catagory
    {
        private int number;
        private String name;
        public Catagory(int number, String name)
        {
            if (number < 0 || String.IsNullOrWhiteSpace(name))
                throw new Exception("Cant Create Catagory number or name is in valid");
            this.number = number;
            this.name = name;
        }

        //Getters
        public int GetNumber()
        {
            return number;
        }

        public String GetName()
        {
            return name;
        }

    }
}
