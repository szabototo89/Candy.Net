using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Candy.Linq
{
    public static class NumberExtensions
    {
        public static IEnumerable<Int32> To(this Int32 start, Int32 end)
        {
            unchecked
            {
                for (var i = start; i < end + 1; i++)
                    yield return i;
            }
        }
    }
}
