using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MvvmLight.XamarinForms;
using MyWeather.Core.Providers;
using MyWeather.Core.ViewModels;
using MyWeatherForms.Providers;
using MyWeatherForms.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace MyWeatherForms
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            InitProviders();
            Current.MainPage = new HomeView();
            SetMainPage();
        }

        private void InitProviders()
        {
            var nav = new NavigationService();
            nav.Configure(ViewModelLocator.WeatherPageKey, typeof(WeatherView));

            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<IDispatcherProvider, DispatcherProvider>();
            SimpleIoc.Default.Register<INetworkStatusProvider, NetworkStatusProvider>();
        }

        public static void SetMainPage()
        {
            //Current.MainPage = new TabbedPage
            //{
            //    Children =
            //    {
            //        new NavigationPage(new ItemsPage())
            //        {
            //            Title = "Browse",
            //            Icon = Device.OnPlatform("tab_feed.png",null,null)
            //        },
            //        new NavigationPage(new AboutPage())
            //        {
            //            Title = "About",
            //            Icon = Device.OnPlatform("tab_about.png",null,null)
            //        },
            //    }
            //};
        }
    }
}
