using GalaSoft.MvvmLight.Helpers;
using MyWeather.Core.Helpers;
using MyWeather.Core.ViewModels;
using System;
using System.Collections.Generic;
using UIKit;

namespace MyWeather.iOS
{
    public partial class HomeViewController : MvvmViewControllerBase
    {
        private IHomeViewModel Vm { get { return ViewModel as IHomeViewModel; } }

        public HomeViewController(IntPtr handle) : base(handle)
        {
            ViewModel = Application.Locator.HomeViewModel as IContentViewModelBase;
        }

        public override void SetupBindings()
        {
            try
            {
                _bindings.Add(this.SetBinding(() => Vm.SearchLocation, () => LocationText.Text, BindingMode.TwoWay));
                GetWeatherButton.SetCommand(Vm.ToWeatherCommand);
            }
            catch (Exception ex)
            {
            }
        }
    }
}