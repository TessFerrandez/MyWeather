using Android.Widget;
using GalaSoft.MvvmLight.Views;
using MyWeather.Droid.Helpers;

namespace MyWeather.Droid
{
    public partial class MainActivity : MvvmActivityBase
    {
        private EditText _locationEditText;
        private Button _getWeatherButton;

        public EditText LocationEditText
        {
            get { return _locationEditText ?? (_locationEditText = FindViewById<EditText>(Resource.Id.Main_LocationEditText)); }
        }
        public Button GetWeatherButton
        {
            get { return _getWeatherButton ?? (_getWeatherButton = FindViewById<Button>(Resource.Id.Main_GetWeatherButton)); }
        }
    }
}