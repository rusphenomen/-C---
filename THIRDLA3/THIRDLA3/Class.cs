using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Services;
using System.Text;
using System.Threading.Tasks;

namespace THIRDLA3
{
    internal class Class<T>
    {
        public static T EnterElem()
        {
            Console.WriteLine("Enter elem: ");
            string value = Console.ReadLine();
            return (T)Convert.ChangeType(value, typeof(T));
        }
    }
}
