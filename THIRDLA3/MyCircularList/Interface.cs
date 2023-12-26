using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCircularList
{
    public interface IList<T> : IEnumerable<T>
    {
        int Count { get; }

        void Add(T item);

        void Clear();

        bool Contains(T item);

        T this[int index] { get; set; }

        void Print();

        int IndexOf(T item);


        void Insert(int index, T item);
        

        void RemoveAt(int index);


        void Remove(T item);

        IList<T> SubList(int find, int sind);
    }
}
