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
            // Проверка для стандартного Income
            string incomePattern = @"^([0-9]{4}\.[0-9]{2}\.[0-9]{2})\s+""(.*?)""\s+(\d+)$";
            Match match = Regex.Match(input, incomePattern);
            if (match.Success)
            {
                var (date, source, amount) = GetBasicDetails(match);
                return new Income(date, source, amount);
            }

            // Проверка для OrganizationIncome (имеет организацию и тип операции)
            string orgPattern = @"^([0-9]{4}\.[0-9]{2}\.[0-9]{2})\s+""(.*?)""\s+(\d+)\s+""(.*?)""\s+""(.*?)""$";
            match = Regex.Match(input, orgPattern);
            if (match.Success)
            {
                var (date, source, amount) = GetBasicDetails(match);
                string organizationName = match.Groups[4].Value;
                string operationType = match.Groups[5].Value;

                return new OrganizationIncome(date, source, amount, organizationName, operationType);
            }

            // Проверка для TaxedIncome (имеет налоговую ставку)
            string taxPattern = @"^([0-9]{4}\.[0-9]{2}\.[0-9]{2})\s+""(.*?)""\s+(\d+)\s+(\d+\.?\d*)$";
            match = Regex.Match(input, taxPattern);
            if (match.Success)
            {
                var (date, source, amount) = GetBasicDetails(match);
                double taxRate = double.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture);

                return new TaxedIncome(date, source, amount, taxRate);
            }

            return null;
        }

        
        public static (DateTime date, string source, int amount) GetBasicDetails(Match match)
        {
            DateTime date = DateTime.ParseExact(match.Groups[1].Value, "yyyy.MM.dd", CultureInfo.InvariantCulture);
            string source = match.Groups[2].Value;
            int amount = int.Parse(match.Groups[3].Value);
            return (date, source, amount);
        }

    }
}
