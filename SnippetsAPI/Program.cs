using DeltaSockets;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SnippetsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            List<ulong> longs = (new ulong[] { 1, 2, 3, 6, 7, 8, 12, 13, 14, 17 }).ToList();

            //Los dos metodos explicados
            ulong numb = 0;
            bool b = FindFirstMissingNumberFromSequence(longs, out numb); //Se devuelve una bool para saber si realmente había algún número que faltaba, puesto que por defecto se devolverá el 0, y el 0 puede formar parte de una secuencia a la cual no pertenece.

            Console.WriteLine("First missing number: {0}\nMissing numbers: {1}", numb, string.Join(", ", FindMissingNumbersFromSequence(longs).Select(x => x.ToString()))); //Para comprobar si la secuencia estaba incompleta solo habría que comprobar x.Count() > 0...
            Console.Read();
        }

        public static bool FindFirstMissingNumberFromSequence<T>(IEnumerable<T> arr, out T n)
        {
            if (!arr.Any(x => x.IsNumericType()))
            {
                Console.WriteLine("Type '{0}' can't be used as a numeric type!", typeof(T).Name);
                n = default(T);
                return false;
            }

            IOrderedEnumerable<T> list = arr.OrderBy(x => x);
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

        public static IEnumerable<T> FindMissingNumbersFromSequence<T>(IEnumerable<T> arr) where T : struct
        {
            if (!arr.Any(x => x.IsNumericType()))
            {
                Console.WriteLine("Type '{0}' can't be used as a numeric type!", typeof(T).Name);
                yield break;
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
    }
}