using MyCircularList;
using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace THIRDLA3
{
    internal class Program
    {
        static void Main(string[] args)
        {
            CircularArrayList<int> list = new CircularArrayList<int>();
            CircularLinkedList<int> listl = new CircularLinkedList<int>();

             //MethodsTest<int>.BasicMethodsTest(list);


            for (int i = 1; i < 10; i++)
            {
                list.Add(i);
                listl.Add(i);
            }
            //ListUtils<int>.Exists(list, item => item == 12);
            //ListUtils<int>.Exists(listl, item => item == 12);

            // ListUtils<int>.FindAll(list, item => item % 2 == 0, () => new CircularArrayList<int>()).Print();

            //ListUtils<int>.ForEach(list, item => item+1); list.Print();
            //ListUtils<int>.Sort(list,(x, y)=>x<y); list.Print();

            //Console.WriteLine(ListUtils<int>.CheckForAll(list, x => x < 0));

            IList<string> newlist = ListUtils<string>.ConvertAll(list, item => item.ToString(), ListUtils<string>.ArrayListConstructor<string>());
            ListUtils<string>.ForEach(newlist, item => item + 1); newlist.Print();

            // ImmutableLinkedList<int> imlist = new ImmutableLinkedList<int>(list);
            // imlist.Add(1);

            Console.ReadKey();
        }
    }
}