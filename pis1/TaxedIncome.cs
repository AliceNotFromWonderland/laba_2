using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis1
{
    internal class TaxedIncome : Income
    {
        public double TaxRate { get; set; }  // Налоговая ставка (в процентах)

        public TaxedIncome(DateTime date, string source, int amount, double taxRate)
            : base(date, source, amount)
        {
            TaxRate = taxRate;
        }

        // Метод для вычисления дохода после налогообложения
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
