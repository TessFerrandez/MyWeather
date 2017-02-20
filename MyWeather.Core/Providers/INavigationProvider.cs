using System;

namespace MyWeather.Core.Providers.Old
{
    public interface INavigationStackProvider
    {
        void GoBack();
        bool CanGoBack { get; }
        void GoForward();
        bool CanGoForward { get; }
        void Clear();
        void Pop();
        void ToPage(Type page, object parameter);
    }
    public interface INavigationProvider : INavigationStackProvider
    {
        void ToWeatherView(string searchLocation);
    }
}
