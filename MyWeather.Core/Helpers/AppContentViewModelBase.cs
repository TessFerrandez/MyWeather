using GalaSoft.MvvmLight.Ioc;
using MyWeather.Core.Providers;
using Newtonsoft.Json;
using PCLStorage;
using System;
using System.Threading.Tasks;

namespace MyWeather.Core.Helpers
{
    public enum NavigationMode { New, Back, Forward, Refresh };
    public enum PopulateReason { New, Back, DataLoaded, Refresh };
    public delegate void PopulateComplete(EventArgs e);

    public interface IContentViewModelBase
    {
        bool IsOnline { get; set; }
        bool IsBusy { get; }
        bool IsActive { get; }

        void AddBusy();
        void ReleaseBusy();

        void OnNavigatedTo(object parameter, NavigationMode navigationMode);
        void OnNavigatingFrom(ref bool cancel, object parameter, NavigationMode navigationMode);
        void OnNavigatedFrom(object parameter, NavigationMode navigationModel);

        Task SaveAsync<T>(string name, T data);
        Task<T> LoadAsync<T>(string name);

        event PopulateComplete PopulateComplete;
    }
    public class AppContentViewModelBase : AppViewModelBase, IContentViewModelBase
    {
        private int _busyCounter = 0;
        private bool _isOnline;
        private bool _isActive;
        private INetworkStatusProvider _networkStatusProvider;

        public bool IsBusy
        {
            get
            {
                bool isBusy;
                lock (this)
                {
                    isBusy = (_busyCounter > 0 ? true : false);
                }
                return isBusy;
            }
        }
        public bool IsOnline
        {
            get { return _isOnline; }
            set
            {
                if (_isOnline != value)
                    OnOnlineChanged(value);
                Set(ref _isOnline, value);
            }
        }
        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                Set(ref _isActive, value);
            }
        }

        public AppContentViewModelBase()
        {
            if (!IsInDesignModeStatic)
            {
                _networkStatusProvider = SimpleIoc.Default.GetInstance<INetworkStatusProvider>();
                _networkStatusProvider.NetworkStatusChanged += OnNetworkStatusChanged;
                _isOnline = _networkStatusProvider.IsOnline;
            }
        }
        ~AppContentViewModelBase()
        {
            if (!IsInDesignModeStatic)
                _networkStatusProvider.NetworkStatusChanged -= OnNetworkStatusChanged;
        }

        public void AddBusy()
        {
            lock (this)
            {
                _busyCounter++;
            }
            SafeRaisePropertyChanged("IsBusy");
        }
        public void ReleaseBusy()
        {
            lock (this)
            {
                _busyCounter--;
                if (_busyCounter < 0)
                    _busyCounter = 0;
            }
            SafeRaisePropertyChanged("IsBusy");
        }

        public virtual void OnNavigatedTo(object parameter, NavigationMode navigationMode) { IsActive = true; }
        public virtual void OnNavigatingFrom(ref bool cancel, object parameter, NavigationMode navigationMode) { }
        public virtual void OnNavigatedFrom(object parameter, NavigationMode navigationMode) { IsActive = false; }


        public async Task SaveAsync<T>(string name, T data)
        {
            try
            {
                var folder = await FileSystem.Current.LocalStorage.CreateFolderAsync("data", CreationCollisionOption.OpenIfExists);
                if (folder != null)
                {
                    var file = await folder.CreateFileAsync(name + ".json", CreationCollisionOption.ReplaceExisting);
                    if (file != null)
                    {
                        var json = JsonConvert.SerializeObject(data);
                        await file.WriteAllTextAsync(json);
                    }
                }
            }
            catch (Exception)
            {
            }
        }
        public async Task<T> LoadAsync<T>(string name)
        {
            try
            {
                var folder = await PCLStorage.FileSystem.Current.LocalStorage.CreateFolderAsync("data", CreationCollisionOption.OpenIfExists);
                if (folder != null)
                {
                    var file = await folder.GetFileAsync(name + ".json");
                    if (file != null)
                    {
                        var json = await file.ReadAllTextAsync();
                        return JsonConvert.DeserializeObject<T>(json);
                    }
                }
            }
            catch (Exception)
            {
            }

            return default(T);
        }

        protected void FirePopulateComplete()
        {
            PopulateComplete?.Invoke(EventArgs.Empty);
        }
        protected virtual void OnOnlineChanged(bool isOnline) { }

        private async void OnNetworkStatusChanged(object sender, EventArgs e)
        {
            await RunAsync(() => { IsOnline = _networkStatusProvider.IsOnline; });
        }

        public event PopulateComplete PopulateComplete;
    }
}
