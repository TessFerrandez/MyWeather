using MyWeather.Core.Providers;
using System;

namespace MyWeatherForms.Providers
{
    public class NetworkStatusProvider : INetworkStatusProvider
    {
        public bool IsOnline => throw new NotImplementedException();

        public event EventHandler NetworkStatusChanged;
    }
}
