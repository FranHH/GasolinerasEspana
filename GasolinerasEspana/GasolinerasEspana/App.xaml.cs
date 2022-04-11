using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace GasolinerasEspana
{
    public partial class App : Application
    {
        
        public App()
        {
            NavigationPage.SetHasNavigationBar(this, false);
            InitializeComponent();

            MainPage = new MainPage();
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
