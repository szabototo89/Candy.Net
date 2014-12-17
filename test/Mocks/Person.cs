using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Candy.Net.Tests.Mocks
{
    internal class Person
    {
        public String Name { get; set; }
        
        public Int32 Age { get; set; }

        public Address Address { get; set; }
    }
}
