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
            string input = "2023.09.24 \"Ежемесячная стипендия\" 100000000";
            Income res = Income.StrToIncome(input);
            Console.WriteLine(res.ToString());
            Console.ReadKey();
        }
    }
}
