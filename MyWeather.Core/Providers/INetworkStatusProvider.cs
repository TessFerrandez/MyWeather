using System;

namespace MyWeather.Core.Providers
{
    public interface INetworkStatusProvider
    {
        bool IsOnline { get; }
        event EventHandler NetworkStatusChanged;
    }
}
