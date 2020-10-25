using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoSenseTask
{
    class Program
    {
        static void Main(string[] args)
        {

            string dataInString = Console.ReadLine();

            IEnumerable<string> dataInSeparated = dataInString.Split(',');

            List<int> dataInt = new List<int>();

            foreach (var item in dataInSeparated)
            {
                try
                {
                    dataInt.Add(Convert.ToInt32(item));
                }
                catch (FormatException)
                {

                }

            }



            dataInt.ThisDoesNotMakeAnySense(item => ((item > 9) && (item < 100)), item =>
            {
                item = 0;
                Console.WriteLine(Convert.ToString(item) + "\tAction applied!");
            });

            /*
            Console.WriteLine();
            foreach (var item in dataInt)
            {
                Console.Write(Convert.ToString(item) + ' ');
            }
            */
            Console.ReadLine();

        }
    }



    public static class AdditionalClass
    {
        public static void ThisDoesNotMakeAnySense<T>(this IEnumerable<T> IEnum, Predicate<T> predicate, Action<T> action)
        {
            if (IEnum == null || IEnum.Count() == 0)
            {
                Console.WriteLine("BAD");
                throw new ArgumentNullException("Input data is null!\n");
            }



            foreach (var item in IEnum)
            {
                if (!predicate(item))
                {
                    action(item);
                }
                else
                {
                    Console.WriteLine(Convert.ToString(item) + "\tData is good!");
                }
            }

            //return IEnum;

        }


    }
}
