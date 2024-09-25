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

            string input = "2023.09.24 \"Ежемесячная стипендия\" 100000000; 2023.09.25 \"Премия\" 5000000";
           
            ProcessEntries(input);

            Console.ReadKey();
        }

        public static void ProcessEntries(string input)
        {
            string[] entries = input.Split(';');

            foreach (string entry in entries)
            {
                DisplayIncome(entry.Trim());
            }
        }

        public static void DisplayIncome(string entry)
        {
            Income income = StrToIncome(entry);
            if (income != null)
            {
                Console.WriteLine(income.ToString());
            }
            else
            {
                Console.WriteLine("Некорректные данные дохода.");
            }
        }

        public static Income StrToIncome(string input)
        {
            string pattern = @"([0-9]{4}\.[0-9]{2}\.[0-9]{2})\s+\""(.*?)\""\s+(\d+)";
            Match match = Regex.Match(input, pattern);
            if (match.Success)
            {
                DateTime date = DateTime.ParseExact(match.Groups[1].Value, "yyyy.MM.dd", CultureInfo.InvariantCulture);
                string source = match.Groups[2].Value;
                int amount = int.Parse(match.Groups[3].Value);

                return new Income(date, source, amount);
            }
            else
            {
                return null;
            }
        }
    }
}
