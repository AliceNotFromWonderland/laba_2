using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis1
{
    public class IncomeFactory
    {
        public static (List<Income> incomes, List<string> errors) ProcessEntries(string input)
        {
            List<Income> incomes = new List<Income>();
            List<string> errors = new List<string>();
            string[] entries = input.Split(';');

            foreach (string entry in entries)
            {
                string trimmedEntry = entry.Trim();

                if (string.IsNullOrWhiteSpace(trimmedEntry))
                {
                    errors.Add("Ошибка: пустая запись.");
                    continue;
                }
                try
                {
                    Income income = ChooseIncomeType(trimmedEntry);
                    incomes.Add(income);
                }
                catch (Exception ex)
                {
                    errors.Add(ex.Message);
                }
            }

            return (incomes, errors);
        }


        public static Income ChooseIncomeType(string input)
        {
            Income income;

            if (new OrganizationIncome().TryParse(input, out income))
                return income;

            if (new TaxedIncome().TryParse(input, out income))
                return income;

            if (new Income().TryParse(input, out income))
                return income;

            throw new FormatException("Запись не соответствует ни одному известному формату.");
        }

    }
}
