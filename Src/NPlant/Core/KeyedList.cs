using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace NPlant.Core
{
    public class KeyedList<T> where T : class, IKeyedItem
    {
        private readonly IList<T> _innerList = new List<T>();

        internal IList<T> InnerList
        {
            get { return _innerList; }
        }

        public int Count
        {
            get { return _innerList.Count; }
        }

        public void Add(T item)
        {
            item.CheckForNullArg("item");

            T existingItem = FindItem(item.Key);

            if (existingItem != null)
                _innerList.Remove(existingItem);

            _innerList.Add(item);
        }

        private T FindItem(string key)
        {
            return _innerList.FirstOrDefault(innerItem => key == innerItem.Key);
        }

        public bool TryGetValue(string key, out T item)
        {
            item = this.FindItem(key);

            return item != null;
        }

        public bool TryGetValueByIndex(int index, out T item)
        {
            item = null;

            if (index.IsWithin(0, _innerList.Count - 1))
            {
                item = _innerList[index];
            }

            return item != null;
        }

        public T this[int index, bool throwOnNotFound = true]
        {
            get
            {
                T item;

                if(this.TryGetValueByIndex(index, out item))
                    return item;

                if(throwOnNotFound)
                    throw new NPlantException("Failed to find item at index of {0} in the list of {1} instances".FormatWith(index, typeof(T).FullName));

                return default(T);
            }
        }

        public T this[string key, bool throwOnNotFound = true]
        {
            get
            {
                T item;

                if (this.TryGetValue(key, out item))
                    return item;

                if(throwOnNotFound)
                    throw new NPlantException("Failed to find item of key '{0}' in the list of {1} instances".FormatWith(key, typeof(T).FullName));

                return default(T);
            }
            set { this.Add(value); }
        }

        public void AddRange(T item, params T[] others)
        {
            item.CheckForNullArg("item");
            
            this.Add(item);

            if (others != null)
                this.AddRange(others);
        }

        public void AddRange(IEnumerable<T> items)
        {
            items.CheckForNullArg("items");

            foreach (var item in items)
            {
                this.Add(item);
            }
        }
    }

    public interface IKeyedItem
    {
        string Key { get; }
    }
}