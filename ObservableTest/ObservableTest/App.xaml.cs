using ObservableTest.Data;
using ObservableTest.Model;
using ObservableTest.View;
using ObservableTest.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace ObservableTest
{
    public partial class App : Application
    {
        static ObservableTestDatabase database;

        public App()
        {
            InitializeComponent();

            UsuarioVM = new UsuarioViewModel();
            PostVM = new PostViewModel();

            MainPage = new LoginViewCS();
        }

        public static PostViewModel PostVM { get; set; }
        public static UsuarioViewModel UsuarioVM { get; set; }

        public static ObservableTestDatabase Database
        {
            get
            {
                if (database == null)
                {
                    database = new ObservableTestDatabase(DependencyService.Get<IFileHelper>().GetLocalFilePath("ObservableTestSQLite.db3"));
                }
                return database;
            }
        }


        static UsuarioModel usuarioModel;
        public static UsuarioModel UsuarioModel
        {
            get
            {
                if (usuarioModel == null)
                {
                    usuarioModel = new UsuarioModel();
                }
                return usuarioModel;
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
