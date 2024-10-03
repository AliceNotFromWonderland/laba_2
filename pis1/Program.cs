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


            // Тесты 
            string input1 = "2023.09.24 \"Премия\" 1500000; " +
                        "2023.09.25 \"Бонус\" 3000000;"; // полукорректный вход - одна из строк пустая

            string input2 = "2023.09.24 \"Недостаточный доход\" -1000;"; // некорректный вход - отрицательное значение

            string input3 = "2023.09.25 \"Премия\" 5000000 \"Некорректная\";"; // некорректный вход - не подходит ни одному формату (классу)

            string input4 = ""; // пустая строка



            List<Income> incomes = IncomeParser.ProcessEntries(input);

            foreach (var income in incomes)
            {
                Console.WriteLine(income);
            }

            Console.ReadKey();
        }

    }
}
