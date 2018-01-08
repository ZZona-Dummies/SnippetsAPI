using System;
using System.Collections.Generic;
using System.Linq;

namespace DeltaSockets
{
    public class MinMax<T>
    {
        public T min, max;

        private MinMax() { }

        public MinMax(T mn, T mx)
        {
            min = mn;
            max = mx;
        }
    }

    public static class SocketExtensions
    {
        public static T ObtainFreeID<T>(this IEnumerable<T> arr)
        {
            T id = (dynamic)1; //routingTable.Count > 0 ? routingTable.Keys.Max() : 1;

            if (arr.Count() > 0)
            {
                T n = (dynamic)0,
                      m = (dynamic)arr.Max();

                if (arr.FindFirstMissingNumberFromSequence(out n, new MinMax<T>((dynamic)1, m)))
                    id = n;
                else
                    id = m;
            }

            return id;
        }

        public static bool FindFirstMissingNumberFromSequence<T>(this IEnumerable<T> arr, out T n, MinMax<T> mnmx = null)
        {
            //Dupe

            if (!arr.Any(x => x.IsNumericType()))
            {
                Console.WriteLine("Type '{0}' can't be used as a numeric type!", typeof(T).Name);
                n = default(T);
                return false;
            }

            if (mnmx != null)
            {
                arr.Add(mnmx.min);
                arr.Add(mnmx.max);
            }

            IOrderedEnumerable<T> list = arr.OrderBy(x => x);

            //End dupe

            bool b = false;
            n = default(T);

            foreach (T num in list)
            {
                b = (dynamic)num - n > 1;
                if (b) break;
                else n = (dynamic)num;
            }

            n = (dynamic)n + 1;

            return b;
        }

        public static IEnumerable<T> FindMissingNumbersFromSequence<T>(IEnumerable<T> arr, MinMax<T> mnmx = null) where T : struct
        {
            if (!arr.Any(x => x.IsNumericType()))
            {
                Console.WriteLine("Type '{0}' can't be used as a numeric type!", typeof(T).Name);
                yield break;
            }

            if (mnmx != null)
            {
                arr.Add(mnmx.min);
                arr.Add(mnmx.max);
            }

            IOrderedEnumerable<T> list = arr.OrderBy(x => x);
            T n = default(T);

            foreach (T num in list)
            {
                T op = (dynamic)num - n;
                if ((dynamic)op > 1)
                {
                    int max = op.ConvertValue<int>();
                    for (int l = 1; l < max; ++l)
                        yield return (dynamic)n + l.ConvertValue<T>();
                }
                n = (dynamic)num;
            }
        }

        public static T ConvertValue<T>(this object o) where T : struct
        {
            return (T)Convert.ChangeType(o, typeof(T));
        }

        public static bool IsNumericType<T>(this T o)
        {
            return typeof(T).IsNumericType();
        }

        public static bool IsNumericType(this Type type)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Byte:
                case TypeCode.SByte:
                case TypeCode.UInt16:
                case TypeCode.UInt32:
                case TypeCode.UInt64:
                case TypeCode.Int16:
                case TypeCode.Int32:
                case TypeCode.Int64:
                case TypeCode.Decimal:
                case TypeCode.Double:
                case TypeCode.Single:
                    return true;
                default:
                    return false;
            }
        }

        public static IEnumerable<T> Add<T>(this IEnumerable<T> e, T value)
        {
            foreach (var cur in e)
            {
                yield return cur;
            }
            yield return value;
        }
    }
}