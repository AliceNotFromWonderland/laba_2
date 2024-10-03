using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis1
{
    internal class IncomeParser
    {
        public static List<Income> ProcessEntries(string input)
        {
            List<Income> incomes = new List<Income>();
            string[] entries = input.Split(';');

            foreach (string entry in entries)
            {
                string trimmedEntry = entry.Trim();

                // Проверяем на пустую запись
                if (string.IsNullOrWhiteSpace(trimmedEntry))
                {
                    Console.WriteLine("Ошибка: присутствует пустая запись.");
                    continue;
                }

                try
                {
                    Income income = ChooseIncomeType(trimmedEntry);

                    // Проверка на корректный объект Income
                    if (income != null &&
                        income.Date != DateTime.MinValue &&
                        !string.IsNullOrWhiteSpace(income.Source) &&
                        income.Amount > 0)
                    {
                        incomes.Add(income);
                    }
                    else
                    {
                        Console.WriteLine("Ошибка: запись не соответствует формату.");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Ошибка: {ex.Message}");
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
