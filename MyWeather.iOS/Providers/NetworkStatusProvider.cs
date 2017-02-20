using MyWeather.Core.Providers;
using System;

namespace MyWeather.iOS.Providers
{
    public class NetworkStatusProvider : INetworkStatusProvider
    {
        //TODO: Implement this
        public bool IsOnline => true;

        public event EventHandler NetworkStatusChanged;
    }
}
