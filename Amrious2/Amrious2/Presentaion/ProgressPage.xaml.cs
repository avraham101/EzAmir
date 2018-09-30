using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Amrious2.Logic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Amrious2.Presentaion
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class ProgressPage : ContentPage
	{
        private WordsLogic logicer;
		public ProgressPage (WordsLogic logicer)
		{
            this.logicer = logicer;
			InitializeComponent();
		}
	}
}