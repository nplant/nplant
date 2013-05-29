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

        public T this[int index]
        {
            get
            {
                T item;

                return this.TryGetValueByIndex(index, out item) ? item : default(T);
            }
        }

        public T this[string key]
        {
            get
            {
                T item;

                return this.TryGetValue(key, out item) ? item : default(T);
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