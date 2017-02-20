using MyWeather.Core.Helpers;
using Windows.UI.Core;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MyWeather.Views
{
    public class PageViewBase : Page
    {
        public IContentViewModelBase ViewModelBase { get { return DataContext as IContentViewModelBase; } }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var view = SystemNavigationManager.GetForCurrentView();
            view.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Visible;
            view.BackRequested += SystemNavigationManagerBackRequested;
            ViewModelBase.OnNavigatedTo(e.Parameter, FromNativeNavigationMode(e.NavigationMode));
        }
        private void SystemNavigationManagerBackRequested(object sender, BackRequestedEventArgs e)
        {
            if (Frame.CanGoBack)
            {
                e.Handled = true;
                Frame.GoBack();
            }
        }
        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            var view = SystemNavigationManager.GetForCurrentView();
            view.BackRequested -= SystemNavigationManagerBackRequested;
            view.AppViewBackButtonVisibility = AppViewBackButtonVisibility.Collapsed;

            base.OnNavigatingFrom(e);
            bool cancel = e.Cancel;
            ViewModelBase.OnNavigatingFrom(ref cancel, e.Parameter, FromNativeNavigationMode(e.NavigationMode));
            e.Cancel = cancel;
        }
        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            ViewModelBase.OnNavigatedFrom(e.Parameter, FromNativeNavigationMode(e.NavigationMode));
        }
        private Core.Helpers.NavigationMode FromNativeNavigationMode(Windows.UI.Xaml.Navigation.NavigationMode nativeMode)
        {
            switch (nativeMode)
            {
                case Windows.UI.Xaml.Navigation.NavigationMode.Back: return Core.Helpers.NavigationMode.Back;
                case Windows.UI.Xaml.Navigation.NavigationMode.Forward: return Core.Helpers.NavigationMode.Forward;
                case Windows.UI.Xaml.Navigation.NavigationMode.New: return Core.Helpers.NavigationMode.New;
                case Windows.UI.Xaml.Navigation.NavigationMode.Refresh: return Core.Helpers.NavigationMode.Refresh;
                default: return Core.Helpers.NavigationMode.Refresh;
            }
        }
    }
}
