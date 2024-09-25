using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pis1
{
    internal class Income
    {
        public DateTime Date { get; set; }
        public string Source { get; set; }
        public int Amount { get; set; }

        public Income(DateTime date, string source, int amount)
        {
            Date = date;
            Source = source;
            Amount = amount;
        }

        public override string ToString()
        {
            return $"Дата: {Date:yyyy.MM.dd}, Источник: {Source}, Сумма: {Amount}";
        }       
    }
}
