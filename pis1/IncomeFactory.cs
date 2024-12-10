using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis1
{
    public class IncomeFactory
    {
        public static List<Income> ProcessEntries(string input)
        {
            List<Income> incomes = new List<Income>();
            string[] entries = input.Split(';');

            foreach (string entry in entries)
            {
                string trimmedEntry = entry.Trim();
                if (string.IsNullOrWhiteSpace(trimmedEntry))
                {
                    Console.WriteLine("Ошибка: присутствует пустая запись.");
                    continue;
                }

                try
                {
                    Income income = ChooseIncomeType(trimmedEntry);
                    if (income != null)
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


        public static Income ChooseIncomeType(string input)
        {
            // Попытка разобрать как OrganizationIncome
            try
            {
                return new OrganizationIncome(DateTime.MinValue, "", 0, "", "").FromStr(input);
            }
            catch (FormatException) { }

            // Попытка разобрать как TaxedIncome, если предыдущая не сработала
            try
            {
                return new TaxedIncome(DateTime.MinValue, "", 0, 0).FromStr(input);
            }
            catch (FormatException) { }

            // Попытка разобрать как базовый Income, если предыдущие не сработали
            try
            {
                return new Income(DateTime.MinValue, "", 0).FromStr(input);
            }
            catch (FormatException) { }

            throw new ArgumentException("Неверный формат записи для Income.", nameof(input));
        }


    }
}
