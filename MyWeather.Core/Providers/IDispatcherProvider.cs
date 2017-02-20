using System;
using System.Threading.Tasks;

namespace MyWeather.Core.Providers
{
    public interface IDispatcherProvider
    {
        Task RunOnUIThreadAsync(Action a);
        Task RunAsync(Action a);
    }
}
