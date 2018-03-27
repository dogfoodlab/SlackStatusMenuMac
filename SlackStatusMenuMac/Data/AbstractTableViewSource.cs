using AppKit;
using Foundation;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using SlackStatusMenuMac.Views;

namespace SlackStatusMenuMac.Data
{
    public abstract class AbstractTableViewSource<T> : NSTableViewSource, IList<T>
        where T : new()
    {
        protected List<T> source = new List<T>();

        public AbstractTableViewSource()
        {
        }

        public override nint GetRowCount(NSTableView tableView)
        {
            return this.source.Count;
        }

        public override NSObject GetObjectValue(NSTableView tableView, NSTableColumn tableColumn, nint row)
        {
            var record = this.source[(int)row];

            var prop = typeof(T).GetProperty(tableColumn.Identifier,
                                             BindingFlags.Public | BindingFlags.Instance);
            if (prop == null) { return null; }

            var val = prop.GetValue(record);
            if (val == null) { return null; }

            return NSObject.FromObject(val);
        }

        public T this[int index]
        {
            get => this[index];
            set => this[index] = value;
        }

        public int Count
            => this.source.Count;

        public bool IsReadOnly
            => false;

        public void Add(T item)
            => this.source.Add(item);

        public void AddRange(IEnumerable<T> collection)
            => this.source.AddRange(collection);

        public void Clear()
            => this.source.Clear();

        public bool Contains(T item)
            => this.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
            => this.source.CopyTo(array, arrayIndex);

        public IEnumerator<T> GetEnumerator()
            => this.source.GetEnumerator();

        public int IndexOf(T item)
            => this.source.IndexOf(item);

        public void Insert(int index, T item)
            => this.source.Insert(index, item);

        public bool Remove(T item)
            => this.source.Remove(item);

        public void RemoveAt(int index)
            => this.source.RemoveAt(index);

        IEnumerator IEnumerable.GetEnumerator()
            => this.source.GetEnumerator();
    }


}
