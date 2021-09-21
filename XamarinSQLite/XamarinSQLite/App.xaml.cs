using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinSQLite.ViewModels;

namespace XamarinSQLite
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            var mainVm = new HomeVm();
            MainPage = mainVm.InitNavigation();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
