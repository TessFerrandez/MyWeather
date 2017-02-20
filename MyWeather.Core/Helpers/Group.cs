using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyWeather.Core.Helpers
{
    public class Group<K, T> : ObservableCollection<T>
    {
        public K Key { get; set; }
        public Group(K key, IEnumerable<T> items)
        {
            Key = key;
            foreach (var item in items)
            {
                Add(item);
            }
        }
        public override bool Equals(object obj)
        {
            var other = obj as Group<K, T>;
            return (other != null && Key.Equals(other.Key) ? true : false);
        }
        public override int GetHashCode()
        {
            return Key.GetHashCode();
        }
    }
}
