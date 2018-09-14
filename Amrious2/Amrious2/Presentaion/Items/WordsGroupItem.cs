using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amrious2
{
    public class WordsGroupItem
    {
        //✔❌?
        private int number;
        public String LabelText
        {
            get { return number+" words"; }
        }

        private String buttontext;
        public String ButtonText
        {
            get
            {
                return buttontext;
            }
        }

        public WordsGroupItem(String buttontext, int number)
        {
            this.buttontext = buttontext;
            this.number = number;
        }
        
    }
}
