using GalaSoft.MvvmLight.Command;
using MyWeather.Core.Helpers;

namespace MyWeather.Core.ViewModels
{
    public interface IHomeViewModel : IContentViewModelBase
    {
        string SearchLocation { get; set; }
        RelayCommand ToWeatherCommand { get; }
    }
    public class HomeViewModel : AppContentViewModelBase, IHomeViewModel
    {
        private RelayCommand _toWeatherCommand;
        private string _searchLocation;
        public RelayCommand ToWeatherCommand
        {
            get
            {
                return _toWeatherCommand ?? (_toWeatherCommand = new RelayCommand(() => {
                    if (!string.IsNullOrEmpty(SearchLocation))
                    {
                        NavigationService.NavigateTo(ViewModelLocator.WeatherPageKey, SearchLocation);
                    }
                }));
            }
        }
        public string SearchLocation
        {
            get { return _searchLocation; }
            set { Set(ref _searchLocation, value); }
        }
    }
}
