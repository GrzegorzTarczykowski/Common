using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Common.Extensions
{
    static class EnumerableExtension
    {
        public static IEnumerable<T> SelectManyInNested<T>(this IEnumerable<T> collection, Func<T, IEnumerable<T>> selector)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            foreach (var item in collection)
            {
                yield return item;
                IEnumerable<T> children = selector(item).SelectManyInNested(selector);
                foreach (var child in children)
                {
                    yield return child;
                }
            }
        }


        public static IEnumerable<T> SelectManyInNested<T>(this IEnumerable collection, Func<T, IEnumerable> selector)
        {
            if (collection == null)
            {
                throw new ArgumentNullException(nameof(collection));
            }

            if (selector == null)
            {
                throw new ArgumentNullException(nameof(selector));
            }

            Stack<IEnumerable<T>> stack = new Stack<IEnumerable<T>>();
            stack.Push(collection.OfType<T>());

            while (stack.Count > 0)
            {
                IEnumerable<T> items = stack.Pop();
                foreach (var item in items)
                {
                    yield return item;
                    IEnumerable<T> children = selector(item).OfType<T>();
                    stack.Push(children);
                }
            }
        }
    }
}
