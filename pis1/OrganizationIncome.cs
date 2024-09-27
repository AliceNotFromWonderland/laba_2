using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pis1
{
    internal class OrganizationIncome : Income
    {
        public string OrganizationName { get; set; }
        public string OperationType { get; set; }

        public OrganizationIncome(DateTime date, string source, int amount, string organizationName, string operationType)
            : base(date, source, amount)
        {
            OrganizationName = organizationName;
            OperationType = operationType;
        }

        public override string ToString()
        {
            return $"{base.ToString()}, Организация: {OrganizationName}, Тип операции: {OperationType}";
        }

    }
}
