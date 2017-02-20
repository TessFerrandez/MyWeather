using MyWeather.Core.Providers;
using System;
using System.Net.NetworkInformation;
using Windows.Networking.Connectivity;

namespace MyWeather.Providers
{
    public class NetworkStatusProvider : INetworkStatusProvider
    {
        private bool _isOnline;
        public bool IsOnline { get { return _isOnline; } }

        public NetworkStatusProvider()
        {
            NetworkInformation.NetworkStatusChanged += OnNetworkStatusChanged;
            _isOnline = NetworkInterface.GetIsNetworkAvailable();
        }
        ~NetworkStatusProvider()
        {
            NetworkInformation.NetworkStatusChanged -= OnNetworkStatusChanged;
        }
        private async void OnNetworkStatusChanged(object sender)
        {
            DispatcherProvider provider = new DispatcherProvider();
            await provider.RunOnUIThreadAsync(() => {
                _isOnline = NetworkInterface.GetIsNetworkAvailable();
                NetworkStatusChanged?.Invoke(this, EventArgs.Empty);
            }).ConfigureAwait(false);
        }
        public event EventHandler NetworkStatusChanged;
    }
}
