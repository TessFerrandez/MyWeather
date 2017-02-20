using System;
using System.Threading.Tasks;

namespace MyWeather.Core.Helpers
{
    public interface IViewModelBaseWithParameter<T>
    {
        T Parameter { get; }
        Task LoadAsync(PopulateReason reason, T parameter);
        Task<bool> Populate(PopulateReason reason, T parameter);
        Task<bool> RequestAsync(T parameter);
    }
    public class AppViewModelBaseWithParameter<T> : AppContentViewModelBase, IViewModelBaseWithParameter<T>
    {
        private T _parameter = default(T);

        public T Parameter { get { return _parameter; } }
        public override async void OnNavigatedTo(object parameter, NavigationMode navigationMode)
        {
            base.OnNavigatedTo(parameter, navigationMode);
            if (parameter != null && parameter is T)
            {
                _parameter = (T)parameter;
                await RunAsync(async () =>
                {
                    await LoadAsync((navigationMode == NavigationMode.New ? PopulateReason.New :
                                     navigationMode == NavigationMode.Back ? PopulateReason.Back :
                                     PopulateReason.Refresh),
                                     _parameter);
                });
            }
            else
            {
                _parameter = default(T);
                await RunAsync(async () =>
                {
                    await LoadAsync((navigationMode == NavigationMode.New ? PopulateReason.New :
                                     navigationMode == NavigationMode.Back ? PopulateReason.Back :
                                     PopulateReason.Refresh),
                                     _parameter);
                });
            }
        }
        protected override async void OnOnlineChanged(bool isOnline)
        {
            await RunAsync(async () =>
            {
                if (isOnline && IsActive)
                {
                    bool parameterValid = (Parameter is int ? !Parameter.Equals(0) : Parameter != null);
                    if (parameterValid)
                        await LoadAsync(PopulateReason.Refresh, Parameter);
                }
            });
        }
        public virtual async Task LoadAsync(PopulateReason reason, T parameter)
        {
            bool populated = false;
            if (parameter != null)
                _parameter = parameter;
            AddBusy();
            if (reason == PopulateReason.New || reason == PopulateReason.Back)
                populated = await Populate(reason, parameter);
            if (await RequestAsync(parameter) || (!populated && reason != PopulateReason.Refresh))
                await Populate(PopulateReason.DataLoaded, parameter);
            if (reason != PopulateReason.Refresh)
                FirePopulateComplete();
            ReleaseBusy();
        }
        public virtual Task<bool> Populate(PopulateReason reason, T parameter)
        {
            //override this in your derived class to set your properties
            return Task.Run(() => false);
        }
        public virtual async Task<bool> RequestAsync(T parameter)
        {
            try
            {
                return await OnRequestAsync(parameter);
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected virtual Task<bool> OnRequestAsync(T parameter)
        {
            //override this in your derived class if you need to fetch anything remote
            return Task.Run(() => false);
        }
    }
}
