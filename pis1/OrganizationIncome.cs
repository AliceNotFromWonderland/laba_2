using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace pis1
{
    public class OrganizationIncome : Income
    {
        public string OrganizationName { get; set; }
        public string OperationType { get; set; }

        public OrganizationIncome(DateTime date, string source, int amount, string organizationName, string operationType)
            : base(date, source, amount)
        {
            OrganizationName = organizationName;
            OperationType = operationType;
        }

        // Переопределяем FromStr для организации
        public override Income FromStr(string input)
        {
            string orgPattern = @"^([0-9]{4}\.[0-9]{2}\.[0-9]{2})\s+""(.*?)""\s+(\d+)\s+""(.*?)""\s+""(.*?)""$";
            Match match = Regex.Match(input, orgPattern);

            if (match.Success)
            {
                var (date, source, amount) = GetBasicDetails(match);
                string organizationName = match.Groups[4].Value;
                string operationType = match.Groups[5].Value;

                return new OrganizationIncome(date, source, amount, organizationName, operationType);
            }

            // Если формат не соответствует, кидаем исключение
            return base.FromStr(input); // Пробуем обработать через базовый метод
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Организация: {OrganizationName}, Тип операции: {OperationType}";
        }
    }
}
