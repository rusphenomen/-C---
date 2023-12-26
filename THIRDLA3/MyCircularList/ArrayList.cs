using System;
using System.CodeDom;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCircularList
{
    public class CircularArrayList<T> : IList<T>
    {
        private T[] _array;
        public int _head { get; set; }

        public CircularArrayList()
        {
            _array = new T[0];
            _head = 0;
        }

        public int count = 0;

        public T this[int index]
        {
            get
            {
                if (index < 0 || index >= count)
                    throw new IndexOutOfRangeException("Wrong index!");

                return _array[index];
            }
            set { _array[index] = value; }
        }

        public void SetHead(int index)
        {
            _head = index;
        }

        public int Count { get { return count; } }
        public void Print()
        {
            if (count == 0)
                Console.WriteLine("List is empty!");
            else
            {
                for (int i = 0; i < count; i++)
                    Console.Write(this[(i + _head) % count] + " ");
                Console.WriteLine();
            }
        }

        public void Add(T value)
        {
            Array.Resize(ref _array, count +1);
            _array[count] = value;
            count++;
        }

        public void Clear()
        {
            if (count == 0) throw new ListIsEmpty();
            _array = new T[0];
            _head = 0;
        }

        public bool Contains(T value)
        {
            bool flag = false;

            for (int i = 0; i < count; i++)
                if (_array[(i+_head)%count].Equals(value))
                    flag = true;

            return flag;
        }

        public int IndexOf(T value)
        {
            int index = -1;

            for (int i = 0; i < count; i++)
                if (value.Equals(_array[(i + _head) % count]))
                    index = i;

            if (index == -1)
                throw new ArgumentOutOfRangeException();
            else
                return index;
        }

        public void Insert(int index, T value)
        {
            if (index < 0 || index > _array.Length)
                throw new IndexOutOfRangeException("Index is out of range.");

            Array.Resize(ref _array, count + 1);

            if (index < count)
                Array.Copy(_array, index, _array, index+1, count - index);

            _array[index+1] = value;
            count++;

        }

        public void Remove(T value)
        {
            if (count == 0) throw new ListIsEmpty();
            int index = IndexOf(value);
            RemoveAt(index);
        }

        public void RemoveAt(int index)
        {
            if (count==0) throw new ListIsEmpty();
            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            Array.Copy(_array, index+1, _array, index, count - index-1);


            Array.Resize(ref _array, count - 1);
            count--;
        }

        public IList<T> SubList(int find, int sind)
        {
            if (count == 0)
                throw new ListIsEmpty();

            if ((find < 0 || find >= count) && sind >= 0 && sind < count)
                throw new UnableToCreateSubList(find);
            else
            {
                if ((sind < 0 || sind >= count) && find >= 0 && find < count)
                    throw new UnableToCreateSubList(sind);
                else
                   if ((find < 0 || find >= count) & (sind < 0 || find >= count))
                    throw new UnableToCreateSubList2(find, sind);

            }

            CircularArrayList<T> subList = new CircularArrayList<T>();
            for (int i = find; i <= sind; i++)
                subList.Add(_array[i]);

            return subList;
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < _array.Length; i++)
            {
                yield return _array[(i + _head) % _array.Length];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
