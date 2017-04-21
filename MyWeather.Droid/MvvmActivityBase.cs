using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using MyWeather.Core.Helpers;
using static Android.Renderscripts.ScriptGroup;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;

namespace MyWeather.Droid
{
    public class MvvmActivityBase : ActivityBase
    {
        //trick the linker into believing we use events
        // See https://developer.xamarin.com/guides/android/advanced_topics/linking/
        private static bool _falseFlag = false;
        private Button _fakeButton = null;

        protected readonly List<GalaSoft.MvvmLight.Helpers.Binding> _bindings = new List<GalaSoft.MvvmLight.Helpers.Binding>();
        protected IContentViewModelBase ViewModel { get; set; }
        protected NavigationService Nav => ServiceLocator.Current.GetInstance<INavigationService>() as NavigationService;
        protected object Parameter { get; set; }

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetupBindings();
            Parameter = Nav.GetAndRemoveParameter<object>(Intent);
            ViewModel.OnNavigatedTo(Parameter, NavigationMode.New);

            if (_falseFlag)
            {
                _fakeButton.Click += (s, e) => { };
            }
        }

        public override void OnBackPressed()
        {
            base.OnBackPressed();
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
        }

        public virtual void SetupBindings()
        {
            // override here to add stuff
        }
    }
}