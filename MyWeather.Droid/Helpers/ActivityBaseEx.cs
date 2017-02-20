using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace MyWeather.Droid.Helpers
{
    public class ActivityBaseEx : ActivityBase
    {
        public NavigationService GlobalNavigation
        {
            get { return (NavigationService)ServiceLocator.Current.GetInstance<INavigationService>(); }
        }
    }
}