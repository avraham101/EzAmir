using System;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using Amrious2.Logic;
using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Android.Content.Res;
using System.IO;

namespace Amrious2.Droid
{
    [Activity(Label = "EzAmir", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation ,ScreenOrientation = ScreenOrientation.Portrait)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        public static List<Word>[] list = null;
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            base.OnCreate(bundle);
            //List<Word> output = new List<Word>();
            List<Word>[] output;
            TextView tv = new TextView(this);
            AssetManager assets = this.Assets;
            try
            {
                //output = GetListOfWords();
                output = GetArrayListOfWords();
                //String txt ="";
                //foreach (Word e in output)
                //    txt = txt + e.GetWord + e.GetMeaning + "/n";
                //tv.Text = txt;
                list = output;
            }
            catch
            {
                //tv.Text = "Something broken.";
            }
            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
        }
        public List<Word> GetListOfWords()
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            List<Word> output = new List<Word>();
            AssetManager assets = this.Assets;
            try
            {
                using (Stream stream = assets.Open("words.bee"))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    output = (List<Word>)bin.Deserialize(stream);
                    stream.Dispose();
                }
            }
            catch(Exception e)
            {

                //Some thing Broken
            }
            return output;
        }

        public List<Word>[] GetArrayListOfWords()
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            List<Word>[] output=null;
            AssetManager assets = this.Assets;
            try
            {
                using (Stream stream = assets.Open("words.bee"))
                {
                    BinaryFormatter bin = new BinaryFormatter();
                    output = (List<Word>[])bin.Deserialize(stream);
                    stream.Dispose();
                }
            }
            catch (Exception e)
            {
                //Some thing Broken
            }
            return output;
        }
    }
}

