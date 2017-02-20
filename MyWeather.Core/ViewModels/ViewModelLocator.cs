using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using MyWeather.Core.Services;
using MyWeather.Core.Design;

namespace MyWeather.Core.ViewModels
{
    public class ViewModelLocator
    {
        private const bool ForceDesignData = false;
        public const string WeatherPageKey = "WeatherPage";

        public IHomeViewModel HomeViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IHomeViewModel>(); }
        }
        public IWeatherViewModel WeatherViewModel
        {
            get { return SimpleIoc.Default.GetInstance<IWeatherViewModel>(); }
        }
        static ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            if (UseDesignData)
            {
                if (!SimpleIoc.Default.IsRegistered<INavigationService>())
                {
                    SimpleIoc.Default.Register<INavigationService, DesignNavigationService>();
                }
                SimpleIoc.Default.Register<IDataAccessService, DesignDataAccessService>();
                SimpleIoc.Default.Register<IWeatherViewModel, DesignWeatherViewModel>();
            }
            else
            {
                SimpleIoc.Default.Register<IDataAccessService, DataAccessService>();
                SimpleIoc.Default.Register<IWeatherViewModel, WeatherViewModel>();
            }
            SimpleIoc.Default.Register<IHomeViewModel, HomeViewModel>();
        }

        private static bool UseDesignData
        {
            get { return ViewModelBase.IsInDesignModeStatic || ForceDesignData; }
        }
    }
}
