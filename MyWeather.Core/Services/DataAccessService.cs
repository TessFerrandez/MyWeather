using MyWeather.Core.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace MyWeather.Core.Services
{
    public class MethodFailedException : Exception
    {
        public MethodFailedException()
        {
        }
        public MethodFailedException(HttpStatusCode errorCode, string errorMessage)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
        }
        public HttpStatusCode ErrorCode { get; protected set; }
        public string ErrorMessage { get; protected set; }
    }
    public class RESTService
    {
        private Uri _serviceUri;
        private string _appKeyString;
        public string AuthHeader { get; set; }
        public RESTService(Uri uri, string appKeyString)
        {
            _serviceUri = uri;
            _appKeyString = appKeyString;
        }
        public async Task<T> GetAsync<T>(string query) where T : new()
        {
            return await GetRawAsync<T>(GetURI(query));
        }
        private async Task<T> GetRawAsync<T>(string uri) where T : new()
        {
            string json = await GetRawJsonAsync(uri);
            if (json == null)
                return default(T);
            else
                return JsonConvert.DeserializeObject<T>(json);
        }
        private async Task<string> GetRawJsonAsync(string uri)
        {
            var http = new HttpClient();

            if (!string.IsNullOrEmpty(AuthHeader))
                http.DefaultRequestHeaders.Add("Authorization", AuthHeader);

            try
            {
                var response = await http.GetAsync(uri);
                if(response.StatusCode == HttpStatusCode.NotFound)
                {
                    return null;
                }
                else if (response.IsSuccessStatusCode)
                {
                    return await response.Content.ReadAsStringAsync();
                }
                else
                {
                    throw new MethodFailedException(response.StatusCode, response.ReasonPhrase);
                }
            }
            catch (MethodFailedException)
            {
                throw;
            }
            catch(Exception ex)
            {
                throw new MethodFailedException(HttpStatusCode.Unused, ex.Message);
            }
        }
        private string GetURI(string query)
        {
            string fullUri;

            if (_serviceUri == null)
                throw new Exception("ServiceUri not set");

            if (_serviceUri.AbsoluteUri.EndsWith("/"))
                fullUri = $"{_serviceUri.AbsoluteUri}{query}";
            else
                fullUri = $"{_serviceUri.AbsoluteUri}/{query}";

            if (!string.IsNullOrEmpty(_appKeyString))
                fullUri += "&" + _appKeyString;

            return fullUri;
        }
    }
    public interface IDataAccessService
    {
        Task<ForecastModel> GetForecastAsync(string city, bool isMetric);
        Task<ForecastModel> GetForecastAsync(double lon, double lat, bool isMetric);
        Task<CurrentWeatherDataModel> GetCurrentWeatherAsync(string city, bool isMetric);
        Task<CurrentWeatherDataModel> GetCurrentWeatherAsync(double lon, double lat, bool isMetric);
    }
    public class DataAccessService : IDataAccessService
    {
        public RESTService _restService = new RESTService(new Uri("http://api.openweathermap.org/data/2.5/", UriKind.Absolute), "APPID=" + Constants.APPID);
        public async Task<CurrentWeatherDataModel> GetCurrentWeatherAsync(string city, bool isMetric)
        {
            var unit = isMetric ? "metric" : "imperial";
            string query = $"weather?q={city}&units={unit}&mode=json";
            return await _restService.GetAsync<CurrentWeatherDataModel>(query);
        }
        public async Task<CurrentWeatherDataModel> GetCurrentWeatherAsync(double lon, double lat, bool isMetric)
        {
            var unit = isMetric ? "metric" : "imperial";
            string query = $"weather?lat={lat}&lon={lon}&units={unit}&mode=json";
            return await _restService.GetAsync<CurrentWeatherDataModel>(query);
        }
        public async Task<ForecastModel> GetForecastAsync(string city, bool isMetric)
        {
            var unit = isMetric ? "metric" : "imperial";
            string query = $"forecast/daily?q={city}&units={unit}&mode=json";
            return await _restService.GetAsync<ForecastModel>(query);
        }
        public async Task<ForecastModel> GetForecastAsync(double lon, double lat, bool isMetric)
        {
            var unit = isMetric ? "metric" : "imperial";
            string query = $"forecast/daily?lat={lat}&lon={lon}&units={unit}&mode=json";
            return await _restService.GetAsync<ForecastModel>(query);
        }
    }
}
