using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pis1
{
    public class TaxedIncome : Income
    {
        public double TaxRate { get; set; }
        public TaxedIncome() { }

        public TaxedIncome(DateTime date, string source, int amount, double taxRate)
            : base(date, source, amount)
        {
            TaxRate = taxRate;
        }

        public override bool TryParse(string input, out Income income)
        {
            income = null;
            string taxPattern = @"^\s*([0-9]{4}\.[0-9]{2}\.[0-9]{2})\s+""(.*?)""\s+(\d+)\s+(\d+[.,]?\d*)\s*$";
            Match match = Regex.Match(input, taxPattern);

            if (match.Success)
            {
                var (date, source, amount) = GetBasicDetails(match);
                double taxRate = double.Parse(match.Groups[4].Value, CultureInfo.InvariantCulture);

                income = new TaxedIncome(date, source, amount, taxRate);
                return true;
            }
            return false;
        }


        public double GetNetIncome()
        {
            return Amount - (Amount * TaxRate / 100);
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Налоговая ставка: {TaxRate}%, Доход после налогообложения: {GetNetIncome()}";
        }
    }
}
