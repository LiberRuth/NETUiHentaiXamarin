using NETUiHentaiXamarin.view;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NETUiHentaiXamarin
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new MainPage();
            //MainPage = new NavigationPage(new MainPage());
            TabbedPage tabbed = new TabbedPage();
            tabbed.Children.Add(new NavigationPage(new MainPage()) {Title = "메인" } );
            tabbed.Children.Add(new NavigationPage(new SearchPage()) { Title = "검색" } );
            MainPage = tabbed;
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
