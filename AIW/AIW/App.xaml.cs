using AIW.Data;
using AIW.DependencyServices;
using AIW.Models;
using AIW.Views;
using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace AIW
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

         
            MainPage = new NavigationPage(new TabbedPageStart());
           // MainPage = new NavigationPage(new WView());

            //MainPage = new TabbedPageStart();

        }

        static DownloadsDatabase database;

        public static DownloadsDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new DownloadsDatabase(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DownloadDatabase.db3"));
                }
                return database;
            }
        }



        protected override void OnStart()
        {
            // Handle when your app starts
           
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
