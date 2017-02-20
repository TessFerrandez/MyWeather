using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using MyWeather.Core.Providers;
using MyWeather.Core.ViewModels;
using MyWeather.Providers;
using MyWeather.Views;
using System;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace MyWeather
{
    sealed partial class App : Application
    {
        public App()
        {
            InitProviders();

            InitializeComponent();
            Suspending += OnSuspending;
        }
        private void InitProviders()
        {
            var nav = new NavigationService();
            nav.Configure(ViewModelLocator.WeatherPageKey, typeof(WeatherView));

            SimpleIoc.Default.Register<INavigationService>(() => nav);
            SimpleIoc.Default.Register<IDispatcherProvider, DispatcherProvider>();
            SimpleIoc.Default.Register<INetworkStatusProvider, NetworkStatusProvider>();
            //SimpleIoc.Default.Register<INavigationProvider>(() => new NavigationProvider(null), true);
            //_navigationProvider = SimpleIoc.Default.GetInstance<INavigationProvider>();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            Frame rootFrame = Window.Current.Content as Frame;

            // Do not repeat app initialization when the Window already has content,
            // just ensure that the window is active
            if (rootFrame == null)
            {
                // Create a Frame to act as the navigation context and navigate to the first page
                rootFrame = new Frame();

                rootFrame.NavigationFailed += OnNavigationFailed;

                if (e.PreviousExecutionState == ApplicationExecutionState.Terminated)
                {
                    //TODO: Load state from previously suspended application
                }

                // Place the frame in the current Window
                Window.Current.Content = rootFrame;
            }

            if (e.PrelaunchActivated == false)
            {
                if (rootFrame.Content == null)
                {
                    // When the navigation stack isn't restored navigate to the first page,
                    // configuring the new page by passing required information as a navigation
                    // parameter
                    rootFrame.Navigate(typeof(HomeView), e.Arguments);
                }
                // Ensure the current window is active
                Window.Current.Activate();
            }
        }
        void OnNavigationFailed(object sender, NavigationFailedEventArgs e)
        {
            throw new Exception("Failed to load Page " + e.SourcePageType.FullName);
        }
        private void OnSuspending(object sender, SuspendingEventArgs e)
        {
            var deferral = e.SuspendingOperation.GetDeferral();
            //TODO: Save application state and stop any background activity
            deferral.Complete();
        }
    }
}
