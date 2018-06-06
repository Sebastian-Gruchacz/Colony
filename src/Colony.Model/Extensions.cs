using System;
using Colony.Model.Core;

namespace Colony.Model
{
    public static class Extensions
    {
        /// <summary>
        /// Shuffles randomly given array
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="array"></param>
        /// <param name="rnd"></param>
        public static void Shuffle<T>(this T[] array, RandomProvider rnd)
        {
            // Knuth shuffle algorithm :: courtesy of Wikipedia :)
            for (int t = 0; t < array.Length; t++)
            {
                T tmp = array[t];
                int r = rnd.NextInt(t, array.Length);
                array[t] = array[r];
                array[r] = tmp;
            }
        }

        /// <summary>
        /// Retrieves custom attribute from type
        /// </summary>
        /// <typeparam name="TAttribute"></typeparam>
        /// <param name="type"></param>
        /// <returns></returns>
        public static TAttribute GetAttribute<TAttribute>(this Type type) where TAttribute : Attribute
        {
            var attributes = type.GetCustomAttributes(true);
            foreach (object attribute in attributes)
            {
                TAttribute att = attribute as TAttribute;
                if (att != null)
                {
                    return att;
                }
            }

            return null;
        }
    }
}
