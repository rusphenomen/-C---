using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MyCircularList
{
    public class ImmutableLinkedList<T> : IList<T>
    {
        private readonly IList<T> List;
        public int count { get; private set; }

        public ImmutableLinkedList(IList<T> list)
        {
            List = list;
            count = list.Count();
        }

        public T this[int index]
        {
            get => List[index];
            set => throw new ImmutableListChange("List is immutable.");
        }

        public void Print()
        {
            List.Print();
        }

        public int Count { get { return count; } }
        public void Add(T value)
        {
            throw new ImmutableListChange("List is immutable.");
        }

        public void Clear()
        {
            throw new ImmutableListChange("List is immutable.");
        }

        public bool Contains(T value)
        {
            return List.Contains(value);
        }

        public int IndexOf(T value)
        {
            return List.IndexOf(value);
        }

        public void Insert(int index, T value)
        {
            throw new ImmutableListChange("List is immutable.");
        }

        public void Remove(T value)
        {
            throw new ImmutableListChange("List is immutable.");
        }

        public void RemoveAt(int index)
        {
            throw new ImmutableListChange("List is immutable.");
        }

        public IList<T> SubList(int fromIndex, int toIndex)
        {
            IList<T> sublist = List.SubList(fromIndex, toIndex);
            return new ImmutableLinkedList<T>(sublist);
        }

        public IEnumerator<T> GetEnumerator()
        {
            return List.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
