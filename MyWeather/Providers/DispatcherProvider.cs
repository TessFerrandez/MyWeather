using MyWeather.Core.Providers;
using System;
using System.Threading.Tasks;
using Windows.UI.Core;

namespace MyWeather.Providers
{
    public class DispatcherProvider : IDispatcherProvider
    {
        public async Task RunAsync(Action a)
        {
            if (a != null) await Task.Run(a);
        }
        public async Task RunOnUIThreadAsync(Action a)
        {
            var w = Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow;
            if (w != null && w.Dispatcher != null && a != null)
            {
                await w.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, new DispatchedHandler(a));
            }
        }
    }
}
