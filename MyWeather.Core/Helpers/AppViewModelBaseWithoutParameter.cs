using System;
using System.Threading.Tasks;

namespace MyWeather.Core.Helpers
{
    public interface IViewModelBaseWithoutParameter
    {
        Task LoadAsync(PopulateReason reason);
        Task<bool> Populate(PopulateReason reason);
        Task<bool> RequestAsync();
    }
    public class AppViewModelBaseWithoutParameter : AppContentViewModelBase, IViewModelBaseWithoutParameter
    {
        public override async void OnNavigatedTo(object parameter, NavigationMode navigationMode)
        {
            base.OnNavigatedTo(parameter, navigationMode);
            await RunAsync(async () =>
            {
                await LoadAsync(navigationMode == NavigationMode.New ? PopulateReason.New :
                                navigationMode == NavigationMode.Back ? PopulateReason.Back :
                                PopulateReason.Refresh);
            });
        }
        protected override async void OnOnlineChanged(bool isOnline)
        {
            await RunAsync(async () =>
            {
                if (isOnline && IsActive)
                    await LoadAsync(PopulateReason.Refresh);
            });
        }
        public virtual async Task LoadAsync(PopulateReason reason)
        {
            bool populated = false;
            AddBusy();
            if (reason == PopulateReason.New || reason == PopulateReason.Back)
                populated = await Populate(reason);
            if (await RequestAsync() || (!populated && reason != PopulateReason.Refresh))
                await Populate(PopulateReason.DataLoaded);
            if (reason != PopulateReason.Refresh)
                FirePopulateComplete();
            ReleaseBusy();
        }
        public virtual Task<bool> Populate(PopulateReason reason)
        {
            return Task.Run(() => false);
        }
        public virtual async Task<bool> RequestAsync()
        {
            try
            {
                return await OnRequestAsync();
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        protected virtual Task<bool> OnRequestAsync()
        {
            //override if you need to fetch anything remote
            return Task.Run(() => { return false; });
        }
    }
}
