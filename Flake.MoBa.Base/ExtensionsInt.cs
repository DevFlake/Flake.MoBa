using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flake.MoBa.Base
{
    public static class ExtensionsInt
    { 
        public static IEnumerable<int> AsEnumerable(this int value)
        {
            IEnumerable<int> ret = new List<int>() { value };
            return ret;
        }

    }
}
