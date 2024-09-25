using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            string input = "2023.09.24 \"Ежемесячная стипендия\" 100000000; 2023.09.25 \"Премия\" 5000000";
            
            string[] entries = input.Split(';');

            foreach (string entry in entries)
            {
                Console.WriteLine(entry.Trim().ToString());
            }

            Console.ReadKey();
        }
    }
}
