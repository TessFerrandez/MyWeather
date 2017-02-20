using Android.App;
using Android.OS;
using MyWeather.Core.ViewModels;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;

namespace MyWeather.Droid
{
    [Activity(Label = "Weather", MainLauncher = true, Icon = "@drawable/icon")]
    public partial class MainActivity
    {
        //trick the linker into believing we use events
        // See https://developer.xamarin.com/guides/android/advanced_topics/linking/
        private static bool _falseFlag = false;
        //save the bindings to avoid garbage collection
        private readonly List<Binding> _bindings = new List<Binding>();
        public IHomeViewModel Vm { get { return App.Locator.HomeViewModel; } }
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            GetWeatherButton.SetCommand(Vm.ToWeatherCommand);

            //save the binding to avoid garbage collection
            _bindings.Add(this.SetBinding(
                () => Vm.SearchLocation, () => LocationEditText.Text, BindingMode.TwoWay));

            if (_falseFlag)
            {
                GetWeatherButton.Click += (s, e) => { };
            }
        }
    }
}

