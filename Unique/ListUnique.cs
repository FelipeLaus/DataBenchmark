using System;
using System.Collections.Generic;
using System.Text;

namespace DataBenchmark.Unique
{
    public class ListUnique
    {
        private List<int> _items;

        public ListUnique()
        {
            _items = new List<int>();
        }

        public int Count => _items.Count;

        public bool InsertUnique(int value)
        {
            if (_items.Contains(value))
            {
                return false;
            }

            _items.Add(value);
            return true;
        }

        public void Clear()
        {
            _items.Clear();
        }
    }
}
