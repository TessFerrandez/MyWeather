using MyWeather.Core.Providers;
using System;
using System.Threading.Tasks;

namespace MyWeatherForms.Providers
{
    public class DispatcherProvider : IDispatcherProvider
    {
        public Task RunAsync(Action a)
        {
            throw new NotImplementedException();
        }

        public Task RunOnUIThreadAsync(Action a)
        {
            throw new NotImplementedException();
        }
    }
}
