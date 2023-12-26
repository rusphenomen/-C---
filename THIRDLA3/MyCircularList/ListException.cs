using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace MyCircularList
{
    public class ListIsEmpty : Exception
    {
       public ListIsEmpty(): base("List is Empty") { }   
    }

    public class UnableToCreateSubList : Exception
    {
        public int index { get; set; }
        public UnableToCreateSubList(int index) : base("Unable to create sub list!")
        {
            this.index = index;
        }
    }
    public class UnableToCreateSubList2 : UnableToCreateSubList
    {
        public int index2 { get; set; }

        public UnableToCreateSubList2(int index, int index2): base(index)
        {
            this.index = index;
            this.index2 = index2;
        }
    }

    public class ImmutableListChange : Exception
    {
        public ImmutableListChange(string message) : base(message) { }
    }
}
