using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pis1
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input = "2023.09.24 \"Ежемесячная стипендия\" 100000000; " +
                           "2023.09.25 \"Премия\" 5000000 \"Газпром\" \"Начисление\"; " +
                           "2023.09.26 \"Дополнительный доход\" 2000000 13.5";          
           

            List<Income> incomes = IncomeParser.ProcessEntries(input);

            foreach (var income in incomes)
            {
                Console.WriteLine(income);
            }

            Console.ReadKey();
        }

    }
}
