using GalaSoft.MvvmLight.Threading;
using MyWeather.Core.Providers;
using System;
using System.Threading.Tasks;

namespace MyWeather.iOS.Providers
{
    public class DispatcherProvider : IDispatcherProvider
    {
        public async Task RunAsync(Action a)
        {
            if (a != null) await Task.Run(a);
        }

        public async Task RunOnUIThreadAsync(Action a)
        {
            DispatcherHelper.CheckBeginInvokeOnUI(a);
        }
    }
}
