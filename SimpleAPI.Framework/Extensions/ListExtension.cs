using System;
using System.Collections.Generic;

namespace SimpleAPI.Framework.Extensions
{
    public static class ListExtension
    {
        public static IList<TDestination> As<TSource, TDestination>(this IList<TSource> list, Func<TSource, TDestination> func)
        {
            List<TDestination> destinations = new List<TDestination>();

            foreach (var item in list)
            {
                destinations.Add(func(item));
            }

            return destinations;
        }
    }
}
