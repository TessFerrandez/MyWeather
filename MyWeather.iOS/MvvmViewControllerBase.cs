using GalaSoft.MvvmLight.Helpers;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using MyWeather.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using UIKit;

namespace MyWeather.iOS
{
    public class MvvmViewControllerBase : UIViewController
    {
        protected readonly List<Binding> _bindings = new List<Binding>();
        protected IContentViewModelBase ViewModel { get; set; }
        protected NavigationService Nav => ServiceLocator.Current.GetInstance<INavigationService>() as NavigationService;
        protected object Parameter { get; set; }

        public MvvmViewControllerBase(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            Parameter = (object)Nav.GetAndRemoveParameter(this);
            ViewModel.OnNavigatedTo(Parameter, NavigationMode.New);
            base.ViewDidAppear(animated);
        }

        public override void ViewDidDisappear(bool animated)
        {
            ViewModel.OnNavigatedFrom(Parameter, NavigationMode.Back);
            base.ViewDidDisappear(animated);
        }

        public override void ViewWillDisappear(bool animated)
        {
            bool cancel = false;
            ViewModel.OnNavigatingFrom(ref cancel, Parameter, NavigationMode.Back);
            base.ViewWillDisappear(animated);
        }

        public override void ViewDidLoad()
        {
            // call override 
            SetupBindings();
            base.ViewDidLoad();
        }

        public override void ViewDidUnload()
        {
            base.ViewDidUnload();
        }

        public override void ViewWillAppear(bool animated)
        {
            base.ViewWillAppear(animated);
        }

        public virtual void SetupBindings()
        {
            // Override in derived classes to do stuff
        }
    }
}
