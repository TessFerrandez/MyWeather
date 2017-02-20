using System;
using MyWeather.Core.Providers;

namespace MyWeather.Droid.Providers
{
    public class NetworkStatusProvider : INetworkStatusProvider
    {
        public bool IsOnline
        {
            get
            {
                //TODO: Add android specific implmentation here
                return true;
            }
        }

        public event EventHandler NetworkStatusChanged;
    }
}