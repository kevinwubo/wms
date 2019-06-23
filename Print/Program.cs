using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Print
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<string> a = new List<string> { "1", "2", "3", "4", "5" };
            //List<string> b = new List<string> { "1", "2", "3", "4", "5" };
            //a = a.Except(b).ToList();

            //foreach (string item in a)
            //{
            //    Console.WriteLine(item);
            //}

            string a = "asdjasahjkdhasjk.jpg";
            Console.WriteLine(a.Substring(0, a.IndexOf(".")));

            Console.Read();
        }
    }
}
