using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MyCircularList
{
    public class ListUtils<T>
    {
        public delegate IList<TO> ListConstructorDelegate<TO>();

        public delegate T ActionDelegate(T item);

        public delegate bool CheckDelegate(T item);

        public delegate U ConvertDelegate<TI, U>(TI item);

        public delegate bool CompareDelegate(T x, T y);
        public static bool Exists(IList<T> list, CheckDelegate ch)
        {
            if (list.Count == 0) throw new ListIsEmpty();

            foreach (T i in list)
            {
                if (ch(i))
                    return true;
            }
            return false;
        }
        public static T FindFirst(IList<T> list, CheckDelegate condition)
        {
            if (list.Count == 0) throw new ListIsEmpty();

            foreach (T i in list)
                if (condition(i))
                    return i;

            return default;
        }
        public static T FindLast(IList<T> list, CheckDelegate condition)
        {
            if (list.Count == 0) throw new ListIsEmpty();

            T temp = default;
            foreach (T i in list)
                if (condition(i))
                    temp = i;

            return temp;
        }
        public static int FindFirstInd(IList<T> list, CheckDelegate condition)
        {
            if (list.Count == 0) throw new ListIsEmpty();

            foreach (T node in list)
                if (condition(node))
                    return list.IndexOf(node);

            return -1;
        }

        public static int FindLastInd(IList<T> list, CheckDelegate condition)
        {
            if (list.Count == 0) throw new ListIsEmpty();

            int count = -1;
            foreach (var node in list)
            {
                if (condition(node))
                    count = list.IndexOf(node);
            }
            return count;
        }

        public static IList<T> FindAll(IList<T> list, CheckDelegate condition, ListConstructorDelegate<T> newConstructor)
        {
            if (list.Count == 0) throw new ListIsEmpty();

            IList<T> resultList = newConstructor.Invoke();

            for (int i = 0; i < list.Count; i++)
                if (condition(list[i]))
                    resultList.Add(list[i]);

            return resultList;
        }
        public static void ForEach(IList<T> list, ActionDelegate action)
        {
            if (list.Count == 0)
                throw new ListIsEmpty();

            for (int i = 0; i < list.Count; i++)
                list[i] = action(list[i]);
        }

        public static void Sort(IList<T> list, CompareDelegate comparer)
        {
            if (list.Count == 0)
                throw new ListIsEmpty();

            if (list.Count < 2)
            {
                return;
            }

            for (int i = 0; i < list.Count - 1; i++)
                for (int j = 0; j < list.Count - i - 1; j++)
                {
                    if (comparer(list[j], list[j + 1]))
                    {
                        T temp = list[j];
                        list[j] = list[j + 1];
                        list[j + 1] = temp;
                    }
                }

        }
        public static bool CheckForAll(IList<T> list, CheckDelegate condition)
        {
            if (list.Count == 0)
                throw new ListIsEmpty();

            foreach (T item in list)
                if (!condition(item))
                    return false;
            return true;
        }

        public static IList<TO> ConvertAll<TI, TO>(IList<TI> list, ConvertDelegate<TI, TO> converter, ListConstructorDelegate<TO> newConstructor)
        {
            if (list.Count == 0) throw new ListIsEmpty();

            IList<TO> resultList = newConstructor();

            foreach (TI item in list)
            {
                resultList.Add(converter(item));
            }

            return resultList;
        }
        public static ListConstructorDelegate<T> ArrayListConstructor<T>()
        {
            return new ListConstructorDelegate<T>(Activator.CreateInstance<CircularArrayList<T>>);
        }
        public static ListConstructorDelegate<T> LinkedListConstructor<T>()
        {
            return new ListConstructorDelegate<T>(Activator.CreateInstance<CircularLinkedList<T>>);
        }


    }
}