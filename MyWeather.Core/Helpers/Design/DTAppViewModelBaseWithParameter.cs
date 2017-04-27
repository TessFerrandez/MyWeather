using System;
using System.Threading.Tasks;

namespace MyWeather.Core.Helpers
{
    public class DTAppViewModelBaseWithParameter<T> : DTAppContentViewModelBase, IViewModelBaseWithParameter<T>
    {
        public T Parameter => throw new NotImplementedException();

        public new Task LoadAsync(PopulateReason reason, T parameter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Populate(PopulateReason reason, T parameter)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RequestAsync(T parameter)
        {
            throw new NotImplementedException();
        }
    }
}
