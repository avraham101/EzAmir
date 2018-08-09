using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation (XamlCompilationOptions.Compile)]
namespace Amrious2
{
	public partial class App : Application
	{
        private NavigationPage navigator;
        public App ()
		{
			InitializeComponent();
		}

		protected override void OnStart ()
		{
            // Handle when your app starts
            navigator = new NavigationPage(new MainPage());
            MainPage = navigator;
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
