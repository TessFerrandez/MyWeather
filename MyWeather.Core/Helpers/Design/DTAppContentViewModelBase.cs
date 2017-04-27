using System;
using System.Threading.Tasks;

namespace MyWeather.Core.Helpers
{
    public class DTAppContentViewModelBase : DTAppViewModelBase, IContentViewModelBase
    {
        public bool IsOnline { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public bool IsBusy => throw new NotImplementedException();

        public bool IsActive => throw new NotImplementedException();

        public event PopulateComplete PopulateComplete;

        public void AddBusy()
        {
            throw new NotImplementedException();
        }

        public Task<T> LoadAsync<T>(string name)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedFrom(object parameter, NavigationMode navigationModel)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatedTo(object parameter, NavigationMode navigationMode)
        {
            throw new NotImplementedException();
        }

        public void OnNavigatingFrom(ref bool cancel, object parameter, NavigationMode navigationMode)
        {
            throw new NotImplementedException();
        }

        public void ReleaseBusy()
        {
            throw new NotImplementedException();
        }

        public Task SaveAsync<T>(string name, T data)
        {
            throw new NotImplementedException();
        }
    }
}
