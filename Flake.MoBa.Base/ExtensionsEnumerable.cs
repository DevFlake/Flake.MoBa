using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Flake.MoBa.Base
{
    public static class ExtensionsEnumerable
    {
        public static IEnumerable<U> Function<T, U>(this IEnumerable<T> enumerable, Func<T, U> func)
        {
            foreach (T item in enumerable)
            {
                yield return func(item);
            }
        }

        public static void Foreach<T>(this IEnumerable<T> enumerable, Action<T> func)
        {
            if (enumerable != null)
            {
                foreach (T item in enumerable)
                {
                    func(item);
                }
            }
        }

    }
}
