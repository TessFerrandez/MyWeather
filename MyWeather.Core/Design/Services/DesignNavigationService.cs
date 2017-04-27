using GalaSoft.MvvmLight.Views;

namespace MyWeather.Core.Design
{
    public class DesignNavigationService : INavigationService
    {
        public string CurrentPageKey => null;

        public void GoBack()
        {
            //Do nothing
        }
        public void NavigateTo(string pageKey)
        {
            //do nothing
        }
        public void NavigateTo(string pageKey, object parameter)
        {
            //do nothing
        }
    }
}
