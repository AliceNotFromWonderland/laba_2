using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pis1
{
    public class Income
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

        // Виртуальный метод для наследников
        public virtual Income FromStr(string input)
        {
            string incomePattern = @"^([0-9]{4}\.[0-9]{2}\.[0-9]{2})\s+""(.*?)""\s+(\d+)$";
            Match match = Regex.Match(input, incomePattern);

            if (match.Success)
            {
                var (date, source, amount) = GetBasicDetails(match);
                return new Income(date, source, amount);
            }

            // Если формат не соответствует, кидаем исключение
            throw new FormatException("Неверный формат записи для Income.");
        }

        protected static (DateTime date, string source, int amount) GetBasicDetails(Match match)
        {
            DateTime date = DateTime.ParseExact(match.Groups[1].Value, "yyyy.MM.dd", CultureInfo.InvariantCulture);
            string source = match.Groups[2].Value;
            int amount = int.Parse(match.Groups[3].Value);
            return (date, source, amount);
        }

        public override string ToString()
        {
            return $"Дата: {Date:yyyy.MM.dd}, Источник: {Source}, Сумма: {Amount}";
        }
    }
}
