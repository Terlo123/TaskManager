using System;
using TaskOrganizerAndro.View;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace TaskOrganizerAndro
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new DailyView());

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
