using System;
using System.Collections.Generic;
using System.Text;

namespace DataBenchmark.Unique
{
    internal class HashSetUnique
    {
        private HashSet<int> _items;

        public HashSetUnique()
        {
            _items = new HashSet<int>();
        }

        public int Count => _items.Count;

        public bool InsertUnique(int value)
        {
            return _items.Add(value);
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}