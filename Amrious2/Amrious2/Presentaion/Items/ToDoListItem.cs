using Xamarin.Forms;
using System;
using System.Collections.Generic;
using System.Text;

namespace Amrious2.Presentaion
{
    public class ToDoListItem
    {
        public static Color blueTrue = Color.FromHex("#B3D1F0");
        public static Color blueFalse = Color.FromHex("#737D88");

        private String _text;
        public String Text
        {
            get { return _text; }
            set { _text = value; }
        }
        private Color _buttonColor;
        public Color ButtonColor
        {
            get { return _buttonColor; }
            set { _buttonColor = value; }
        }
        private String _buttontext;
        public String ButtonText
        {
            get
            {
                return _buttontext;
            }
        }
        public ToDoListItem(String _text)
        {
            this._text = _text;
            _buttonColor = blueFalse;
            _buttontext = "x";
        }
        
    }
}
