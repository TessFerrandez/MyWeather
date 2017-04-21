using Android.App;
using Android.OS;
using MyWeather.Core.ViewModels;
using GalaSoft.MvvmLight.Helpers;
using System.Collections.Generic;
using MyWeather.Core.Helpers;

namespace MyWeather.Droid
{
    [Activity(Label = "Weather", MainLauncher = true, Icon = "@drawable/icon")]
    public partial class MainActivity
    {
        public IHomeViewModel Vm { get { return ViewModel as IHomeViewModel; } }

        public MainActivity()
        {
            ViewModel = App.Locator.HomeViewModel as IContentViewModelBase;
        }

        public override void SetupBindings()
        {
            SetContentView(Resource.Layout.Main);

            GetWeatherButton.SetCommand(Vm.ToWeatherCommand);
            //save the binding to avoid garbage collection
            _bindings.Add(this.SetBinding(
                () => Vm.SearchLocation, () => LocationEditText.Text, BindingMode.TwoWay));
        }
    }
}

