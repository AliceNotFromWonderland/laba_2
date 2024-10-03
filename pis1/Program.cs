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

            List<Income> incomes = ProcessEntries(input);

            foreach (var income in incomes)
            {
                Console.WriteLine(income);
            }

            Console.ReadKey();
        }

        public static List<Income> ProcessEntries(string input)
        {
            List<Income> incomes = new List<Income>();
            string[] entries = input.Split(';');

            foreach (string entry in entries)
            {
                if (string.IsNullOrWhiteSpace(entry))
                {
                    Console.WriteLine("Ошибка: запись пуста.");
                    return incomes; // Завершаем метод, если запись пустая
                }

                try
                {
                    Income income = ChooseIncomeType(entry.Trim());

                    // Проверка на корректный объект Income
                    if (income != null && income.Date != DateTime.MinValue && !string.IsNullOrWhiteSpace(income.Source) && income.Amount > 0)
                    {
                        incomes.Add(income);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: запись не соответствует формату.");
                        return incomes; // Завершаем метод, если формат записи некорректен
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
                    return incomes; // Завершаем метод в случае исключения
                }
            }

            return incomes;
        }



        private static Income ChooseIncomeType(string input)
        {          
            Income income;

            try
            {
                income = new OrganizationIncome(DateTime.MinValue, "", 0, "", "").FromStr(input);
            }
            catch (FormatException)
            {
                try
                {
                    income = new TaxedIncome(DateTime.MinValue, "", 0, 0).FromStr(input);
                }
                catch (FormatException)
                {
                    income = new Income(DateTime.MinValue, "", 0).FromStr(input);
                }
            }

            return income;
        }
    }
}
