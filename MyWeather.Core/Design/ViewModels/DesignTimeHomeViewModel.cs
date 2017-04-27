using MyWeather.Core.Helpers;
using MyWeather.Core.ViewModels;
using System;
using GalaSoft.MvvmLight.Command;

namespace MyWeather.Core.Design.ViewModels
{
    public class DesignTimeHomeViewModel : DTAppContentViewModelBase, IHomeViewModel
    {
        public string SearchLocation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public RelayCommand ToWeatherCommand => throw new NotImplementedException();
    }
}
