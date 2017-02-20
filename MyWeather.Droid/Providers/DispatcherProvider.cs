using System;
using System.Threading.Tasks;
using MyWeather.Core.Providers;
using GalaSoft.MvvmLight.Threading;

namespace MyWeather.Droid.Providers
{
    public class DispatcherProvider : IDispatcherProvider
    {
        public async Task RunAsync(Action a)
        {
            if (a != null) await Task.Run(a);
        }

        public async Task RunOnUIThreadAsync(Action a)
        {
            //TODO: schedule on UI Thread
            //await RunAsync(a);
            DispatcherHelper.CheckBeginInvokeOnUI(a);
        }
    }
}