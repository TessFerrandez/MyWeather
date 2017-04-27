using System;
using System.Threading.Tasks;

namespace MyWeather.Core.Helpers
{
    public class DTAppViewModelBaseWithoutParameter : DTAppContentViewModelBase, IViewModelBaseWithoutParameter
    {
        public new Task LoadAsync(PopulateReason reason)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Populate(PopulateReason reason)
        {
            throw new NotImplementedException();
        }

        public Task<bool> RequestAsync()
        {
            throw new NotImplementedException();
        }
    }
}
