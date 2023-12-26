using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;


namespace MyCircularList
{
    public class Node<T>
    {

        public T Value { get; set; }

        public Node<T> Next { get; internal set; }

        public Node<T> Previous { get; internal set; }

        public Node(T item)
        {
            this.Value = item;
        }
    }
    public class CircularLinkedList<T>: IList<T>
    {
       
        Node<T> head = null;

       
        Node<T> tail = null;

       
        int count = 0;


        public int Count { get { return count; } }

        public T this[int index]
        {
            get
            {
                
                if (index >= count || index < 0)
                    throw new ArgumentOutOfRangeException("index");
                else
                {
                    Node<T> node = this.head;
                    for (int i = 0; i < index; i++)
                        node = node.Next;
                    return node.Value;
                }
            }
            set
            { Node<T> node=null; node.Value = value; }
        }

        public void Print()
        {
            if (head == null)
            {
                Console.WriteLine("List is empty!");
                return;
            }
            else
            foreach (var item in this)
                Console.Write(item + " ");
        }
        public void Add(T item)
        {
            // if head is null, then this will be the first item
            if (head == null)
                this.AddFirstItem(item);
            else
            {
                Node<T> newNode = new Node<T>(item);
                tail.Next = newNode;
                newNode.Next = head;
                newNode.Previous = tail;
                tail = newNode;
                head.Previous = tail;
            }
            ++count;
        }

        public void AddFirstItem(T item)
        {
            head = new Node<T>(item);
            tail = head;
            head.Next = tail;
            head.Previous = tail;

        }


        public void AddAfterNode(Node<T> node, T item)
        {
            if (node == tail)
                this.Add(item);
            else
            {
                Node<T> newNode = new Node<T>(item);
                newNode.Next = node.Next;
                node.Next.Previous = newNode;
                newNode.Previous = node;
                node.Next = newNode;
            }


            ++count;
        }
        public void Insert(int index, T item)
        {

                Node<T> temp = this.FindNode(index);
                this.AddAfterNode(temp, item);
 
        }

        public Node<T> Find(int index)
        {
            
            Node<T> node = FindNode(index);
            return node;
        }

        public void RemoveAt(int index)
        {
            Node<T> nodeToRemove = this.Find(index);
             if (nodeToRemove != null)
                this.RemoveNode(nodeToRemove);
 

        }

        bool RemoveNode(Node<T> nodeToRemove)
        {

            // if this is head, we need to update the head reference
            if (count == 1)
            {
                head = null;
                tail = null;
            }
            else
            {
                Node<T> previous = nodeToRemove.Previous;
                previous.Next = nodeToRemove.Next;
                nodeToRemove.Next.Previous = nodeToRemove.Previous;

                if (head == nodeToRemove)
                    head = nodeToRemove.Next;
                else if (tail == nodeToRemove)
                    tail = tail.Previous;
            }

 
            --count;
            return true;
        }


        public void Clear()
        {
            if (count == 0)
                Console.WriteLine("List empty! ");
            else
            {
                head = null;
                tail = null;
                count = 0;
            }
           
        }


        public bool RemoveHead()
        {
            return this.RemoveNode(head);
        }


        public bool RemoveTail()
        {
            return this.RemoveNode(tail);
        }

        public Node<T> FindNode(int index)
        {
            if (count == 0)
                throw new ListIsEmpty();

            if (index < 0 || index >= count)
                throw new IndexOutOfRangeException();

            Node<T> result = head;
            int c = 0;

            while (c < index)
            {
                result = result.Next;
                c++;
            }

            return result;
        }
        public void Remove(T item)
        {
            if (count == 0)
                throw new ListIsEmpty();

            Node<T> node = head, nodeToRemove=null;

            do
            {
                if (node.Value.Equals(item))
                    nodeToRemove = node;
                node = node.Next;

            } while (node.Next != head);

            if (nodeToRemove != null)
                this.RemoveNode(nodeToRemove);
            else
                throw new ArgumentOutOfRangeException("There are no such elem in list!");
        }

        public IEnumerator<T> GetEnumerator()
        {
            Node<T> current = head;
            if (current != null)
            {
                do
                {
                    yield return current.Value;
                    current = current.Next;
                } while (current != head);
            }
        }


        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int IndexOf(T item)
        {
            if (count == 0)
                throw new ListIsEmpty();
            int index = -1;

                for (int i = 0; i < count; i++)
                    if (this[i].Equals(item))
                        index = i;
            if (index == -1)
                throw new ArgumentOutOfRangeException();
            else
            return index;
        }
        public bool Contains(T item)
        {
            if (count == 0)
                throw new ListIsEmpty();

            foreach(var p in this)
                if(p.Equals(item))
                    return true;

            return false;
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException("array");
            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException("arrayIndex");

            Node<T> node = head;
            do
            {
                array[arrayIndex++] = node.Value;
                node = node.Next;
            } while (node != head);
        }
       public IList<T> SubList(int find, int sind)
        {

            if (count == 0)
                throw new ListIsEmpty();

            CircularLinkedList<T> sublist = new CircularLinkedList<T>();

            if ((find < 0 || find >= count) && sind>=0 && sind<count)
                throw new UnableToCreateSubList(find);
            else
            {
                if ((sind < 0 || sind >= count) && find >= 0 && find < count)
                    throw new UnableToCreateSubList(sind);
                else
                   if ((find < 0 || find>=count) & (sind <0 || find >= count))
                    throw new UnableToCreateSubList2(find, sind);
                    
            }

            Node<T> fnode = Find(find);
            Node<T> snode = Find(sind);

            sublist.AddFirstItem(fnode.Value);
            fnode = fnode.Next;
            while (fnode != snode)
            {
                sublist.Add(fnode.Value);
                fnode = fnode.Next;
            }
            sublist.Add(snode.Value);

            return sublist;
        }
    }
}
