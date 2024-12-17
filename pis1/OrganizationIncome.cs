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
        public OrganizationIncome() { }

        public OrganizationIncome(DateTime date, string source, int amount, string organizationName, string operationType)
            : base(date, source, amount)
        {
            OrganizationName = organizationName;
            OperationType = operationType;
        }

        public override bool TryParse(string input, out Income income)
        {
            income = null;
            string orgPattern = @"^\s*([0-9]{4}\.[0-9]{2}\.[0-9]{2})\s+""(.*?)""\s+(\d+)\s+""(.*?)""\s+""(.*?)""\s*$";
            Match match = Regex.Match(input, orgPattern);

            if (match.Success)
            {
                var (date, source, amount) = GetBasicDetails(match);
                string organizationName = match.Groups[4].Value;
                string operationType = match.Groups[5].Value;

                income = new OrganizationIncome(date, source, amount, organizationName, operationType);
                return true;
            }
            return false;
        }


        public override string ToString()
        {
            return $"{base.ToString()}, Организация: {OrganizationName}, Тип операции: {OperationType}";
        }
    }
}
