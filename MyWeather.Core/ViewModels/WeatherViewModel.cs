using MyWeather.Core.Helpers;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace MyWeather.Core.ViewModels
{
    public interface IWeatherViewModel : IViewModelBaseWithParameter<string>
    {
        string SearchLocation { get; set; }
        bool IsMetric { get; set; }
        ObservableCollection<ForecastViewModel> Forecasts { get; }
        CurrentWeatherViewModel CurrentWeather { get; }
    }
    public class WeatherViewModel : AppViewModelBaseWithParameter<string>, IWeatherViewModel
    {
        public WeatherViewModel() { }
        //public WeatherViewModel(string location)
        //{
        //    SearchLocation = location;
        //}
        private CurrentWeatherViewModel _currentWeather = new CurrentWeatherViewModel();
        public CurrentWeatherViewModel CurrentWeather
        {
            get { return _currentWeather; }
            private set { Set(ref _currentWeather, value); }
        }
        public ObservableCollection<ForecastViewModel> Forecasts { get; } = new ObservableCollection<ForecastViewModel>();
        public bool IsMetric { get; set; } = true;
        private string _searchLocation;
        public string SearchLocation
        {
            get { return _searchLocation; }
            set { Set(ref _searchLocation, value); }
        }
        public override async Task<bool> Populate(PopulateReason reason, string parameter)
        {
            SearchLocation = string.IsNullOrEmpty(parameter) ? "Stockholm,SE" : parameter;

            if (reason == PopulateReason.New)
            {
                await LoadCurrentWeatherAsync();
                await LoadForecastsAsync();
            }
            return true;
        }
        private async Task LoadForecastsAsync()
        {
            var forecasts = await LoadAsync<ObservableCollection<ForecastViewModel>>("Forecasts");
            if (forecasts != null)
                UpdateCollection(Forecasts, forecasts);
        }
        private async Task LoadCurrentWeatherAsync()
        {
            var current = await LoadAsync<CurrentWeatherViewModel>("CurrentWeather");
            if (current != null)
                CurrentWeather = current;
        }
        protected override async Task<bool> OnRequestAsync(string parameter)
        {
            bool success = false;

            var newForecasts = new List<ForecastViewModel>();
            try
            {
                var current = await DataAccessService.GetCurrentWeatherAsync(SearchLocation, true);

                var forecasts = await DataAccessService.GetForecastAsync(SearchLocation, true);
                foreach (var forecast in forecasts.Forecasts)
                {
                    newForecasts.Add(ForecastViewModel.FromModel(forecast));
                }
                CurrentWeather = CurrentWeatherViewModel.FromModel(current);
                UpdateCollection(Forecasts, newForecasts);
                await SaveForecastsAsync();
                await SaveCurrentWeatherAsync();
                success = true;
            }
            catch
            {
                success = false;
            }

            return success;
        }
        private Task SaveCurrentWeatherAsync()
        {
            return SaveAsync("CurrentWeather", CurrentWeather);
        }
        private Task SaveForecastsAsync()
        {
            return SaveAsync("Forecasts", Forecasts);
        }
    }
}
