using Android.Support.V7.Widget;
using Android.Widget;
using MyWeather.Droid.Helpers;

namespace MyWeather.Droid
{
    public partial class WeatherActivity : MvvmActivityBase
    {
        private TextView _locationTextView;
        public TextView LocationTextView
        {
            get { return _locationTextView ?? (_locationTextView = FindViewById<TextView>(Resource.Id.Weather_LocationTextView)); }
        }
        private TextView _descriptionTextView;
        public TextView DescriptionTextView
        {
            get { return _descriptionTextView ?? (_descriptionTextView = FindViewById<TextView>(Resource.Id.Weather_DescriptionTextView)); }
        }
        private TextView _tempTextView;
        public TextView TempTextView
        {
            get { return _tempTextView ?? (_tempTextView = FindViewById<TextView>(Resource.Id.Weather_TempTextView)); }
        }
        private ImageView _iconImageView;
        public ImageView IconImageView
        {
            get { return _iconImageView ?? (_iconImageView = FindViewById<ImageView>(Resource.Id.Weather_IconImageView)); }
        }
        private RecyclerView _forecastList;
        public RecyclerView ForecastList
        {
            get { return _forecastList ?? (_forecastList = FindViewById<RecyclerView>(Resource.Id.Weather_ForecastList)); }
        }
    }
}