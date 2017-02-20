using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using Microsoft.Practices.ServiceLocation;
using MyWeather.Core.Providers;
using MyWeather.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace MyWeather.Core.Helpers
{
    public interface IViewModelBase { }
    public class AppViewModelBase : ViewModelBase, IViewModelBase
    {
        protected CancellationTokenSource _cts = new CancellationTokenSource();
        protected IDataAccessService DataAccessService { get; private set; } = ServiceLocator.Current.GetInstance<IDataAccessService>();
        protected INavigationService NavigationService { get; private set; } = ServiceLocator.Current.GetInstance<INavigationService>();
        protected IDispatcherProvider DispatcherProvider { get; private set; } = SimpleIoc.Default.GetInstance<IDispatcherProvider>();
        public CancellationToken CancelToken { get { return _cts.Token; } }
        public AppViewModelBase()
        {
        }
        public void SafeRaisePropertyChanged(string propertyName)
        {
            RunAsync(() => RaisePropertyChanged(propertyName));
        }
        public Task RunAsync(Action a)
        {
            return DispatcherProvider.RunOnUIThreadAsync(a);
        }
        protected void UpdateCollection<T>(ObservableCollection<T> oldCollection, IList<T> newList) where T : class
        {
            int indexOld = 0,
                indexNew = 0;

            if (newList == null)
            {
                oldCollection.Clear();
                return;
            }

            do
            {
                var oldItem = (indexOld < oldCollection.Count ? oldCollection[indexOld] : null);
                var newItem = (indexNew < newList.Count ? newList[indexNew] : null);

                if (oldItem == null && newItem == null)
                    // we are done
                    break;
                else if (oldItem == null)
                {
                    // add new item
                    oldCollection.Add(newItem);
                    indexOld++;
                    indexNew++;
                }
                else if (newItem == null)
                {
                    oldCollection.RemoveAt(indexOld);
                }
                else
                {
                    if (oldItem.GetHashCode() == newItem.GetHashCode())
                    {
                        if (!oldItem.Equals(newItem))
                        {
                            oldCollection[indexOld] = newList[indexNew];
                        }
                        indexOld++;
                        indexNew++;
                    }
                    else if (!oldItem.Equals(newItem))
                    {
                        bool handled = false;

                        // look for the new item further down in old list
                        for (int index = indexOld + 1; index < oldCollection.Count; index++)
                        {
                            if (oldCollection[index].Equals(newItem))
                            {
                                for (int i = 0; i < (index - indexOld); i++)
                                    oldCollection.RemoveAt(indexOld);
                                handled = true;
                                break;
                            }
                        }

                        if (!handled)
                        {
                            // look for the old item further down in new list
                            for (int index = indexNew + 1; index < newList.Count; index++)
                            {
                                if (newList[index].Equals(oldItem))
                                {
                                    for (int i = 0; index > indexOld; i++)
                                    {
                                        oldCollection.Insert(indexOld, newList[indexNew + i]);
                                        indexOld++;
                                    }
                                    indexNew += index - indexNew;
                                    handled = true;
                                    break;
                                }
                            }
                        }

                        if (!handled)
                        {
                            oldCollection[indexOld] = newItem;
                            indexOld++;
                            indexNew++;
                        }
                    }
                    else
                    {
                        indexOld++;
                        indexNew++;
                    }
                }
            } while (true);
        }
        protected void UpdateGroupedCollection<K, T>(ObservableCollection<Group<K, T>> oldCollection, ObservableCollection<Group<K, T>> newCollection) where T : class
        {
            // strategy: two runs, first the groups with a call to UpdateCollection, 
            // and then a loop of all groups and a call tou UpdateCollection for each subcollection
            UpdateCollection(oldCollection, newCollection);
            for (int i = 0; i < oldCollection.Count; i++)
            {
                var oldGroup = oldCollection[i] as ObservableCollection<T>;
                var newGroup = newCollection[i] as ObservableCollection<T>;
                UpdateCollection(oldGroup, newGroup);
            }
        }
    }
}
