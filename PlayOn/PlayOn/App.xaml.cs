using System;
using System.IO;
using PlayOn.Models;
using PlayOn.Views;
using Xamarin.Forms;

namespace PlayOn
{
    public partial class App : Application
    {

        private static BaseDatos database;

        public static BaseDatos Database
        {
            get
            {
                if (database == null)
                {
                    database = new BaseDatos(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PlayOnData.db"));
                }

                return database;
            }
        }

        public App ()
        {
            InitializeComponent();

            var database = Database;

            MainPage = new NavigationPage(new HomePage());
        }

        protected override void OnStart ()
        {
        }

        protected override void OnSleep ()
        {
        }

        protected override void OnResume ()
        {
        }
    }
}

