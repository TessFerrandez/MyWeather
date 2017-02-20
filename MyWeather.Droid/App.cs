using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MyWeather.Core.Providers;
using MyWeather.Core.ViewModels;
using MyWeather.Droid.Providers;

namespace MyWeather.Droid
{
    public static class App
    {
        private static ViewModelLocator _locator;
        public static ViewModelLocator Locator
        {
            get
            {
                if (_locator == null)
                {
                    //first time initialization
                    var nav = new NavigationService();
                    nav.Configure(ViewModelLocator.WeatherPageKey, typeof(WeatherActivity));
                    SimpleIoc.Default.Register<INavigationService>(() => nav);

                    //TODO: add other providers here
                    SimpleIoc.Default.Register<IDispatcherProvider, DispatcherProvider>();
                    SimpleIoc.Default.Register<INetworkStatusProvider, NetworkStatusProvider>();

                    _locator = new ViewModelLocator();
                }
                return _locator;
            }
        }
    }
}