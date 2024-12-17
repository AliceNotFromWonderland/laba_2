using Microsoft.VisualStudio.TestPlatform.TestHost;
using pis1;
namespace UnitTest
{
    public class Tests
    {       

        [Test]
        public void ProcessEntries_ValidAndInvalidEntries_ShouldCategorizeCorrectly()
        {
            string input = "2023.09.24 \"Ежемесячная стипендия\" 100000000; " +
                           "2023.09.25 \"Премия\" 5000000 \"Газпром\" \"Начисление\"; " +
                           "54646374;" +
                           "2023.09.26 \"Дополнительный доход\" 2000000 13.5; " +
                           "     ";

            var (incomes, errors) = IncomeFactory.ProcessEntries(input);

            Assert.That(incomes.Count, Is.EqualTo(3));
            Assert.That(errors.Count, Is.EqualTo(2));

            Assert.That(errors[0], Is.EqualTo("Ошибка: запись не соответствует формату: \"54646374\"."));
            Assert.That(errors[1], Is.EqualTo("Ошибка: пустая запись."));
        }

        [Test]
        public void ChooseIncomeType_ValidTaxedIncome_ShouldReturnTaxedIncome()
        {
            string input = "2023.09.26 \"Дополнительный доход\" 2000000 13.5";

            var income = IncomeFactory.ChooseIncomeType(input);

            Assert.IsInstanceOf<TaxedIncome>(income);
            Assert.That(income.Amount, Is.EqualTo(2000000));
        }

        [Test]
        public void ChooseIncomeType_ValidOrganizationIncome_ShouldReturnOrganizationIncome()
        {
            string input = "2023.09.25 \"Премия\" 5000000 \"Газпром\" \"Начисление\"";

            var income = IncomeFactory.ChooseIncomeType(input);

            Assert.IsInstanceOf<OrganizationIncome>(income);
            Assert.That(((OrganizationIncome)income).OrganizationName, Is.EqualTo("Газпром"));
        }

        [Test]
        public void ChooseIncomeType_InvalidFormat_ShouldReturnNull()
        {
            string input = "Некорректный формат записи";

            var income = IncomeFactory.ChooseIncomeType(input);

            Assert.IsNull(income);
        }

        [Test]
        public void TryParse_ValidInput_ShouldParseSuccessfully()
        {
            string input = "2023.09.25 \"Премия\" 5000000";
            var income = new Income();

            bool result = income.TryParse(input, out Income parsedIncome);

            Assert.IsTrue(result);
            Assert.IsNotNull(parsedIncome);
            Assert.That(parsedIncome.Date, Is.EqualTo(new DateTime(2023, 9, 25)));
            Assert.That(parsedIncome.Source, Is.EqualTo("Премия"));
            Assert.That(parsedIncome.Amount, Is.EqualTo(5000000));
        }

        [Test]
        public void TryParse_InvalidInput_ShouldReturnFalse()
        {
            string input = "Некорректный формат записи";
            var income = new Income();

            bool result = income.TryParse(input, out Income parsedIncome);

            Assert.IsFalse(result);
            Assert.IsNull(parsedIncome);
        }

        [Test]
        public void TryParse_EmptyInput_ShouldReturnFalse()
        {
            string input = "";
            var income = new Income();

            bool result = income.TryParse(input, out Income parsedIncome);

            Assert.IsFalse(result);
            Assert.IsNull(parsedIncome);
        }

        [Test]
        public void TryParse_InvalidDate_ShouldReturnFalse()
        {
            string input = "2023-09-25 \"Премия\" 5000000";
            var income = new Income();

            bool result = income.TryParse(input, out Income parsedIncome);

            Assert.IsFalse(result);
            Assert.IsNull(parsedIncome);
        }
        [Test]
        public void Income_ToString_ShouldReturnFormattedString()
        {
            var income = new Income
            {
                Date = new DateTime(2023, 9, 24),
                Source = "Ежемесячная стипендия",
                Amount = 100000
            };

            var result = income.ToString();

            Assert.That(result, Is.EqualTo("Дата: 2023.09.24, Источник: Ежемесячная стипендия, Сумма: 100000"));
        }

        [Test]
        public void OrganizationIncome_ToString_ShouldIncludeOrganizationDetails()
        {
            var orgIncome = new OrganizationIncome
            {
                Date = new DateTime(2023, 9, 25),
                Source = "Премия",
                Amount = 50000,
                OrganizationName = "Газпром",
                OperationType = "Начисление"
            };

            var result = orgIncome.ToString();

            Assert.That(result, Is.EqualTo("Дата: 2023.09.25, Источник: Премия, Сумма: 50000, Организация: Газпром, Тип операции: Начисление"));
        }

        [Test]
        public void TaxedIncome_ToString_ShouldIncludeTaxDetails()
        {
            var taxedIncome = new TaxedIncome
            {
                Date = new DateTime(2023, 9, 26),
                Source = "Дополнительный доход",
                Amount = 20000,
                TaxRate = 13.5
            };

            var result = taxedIncome.ToString();

            var expectedNetIncome = taxedIncome.Amount - (taxedIncome.Amount * taxedIncome.TaxRate / 100);
            Assert.That(result, Is.EqualTo($"Дата: 2023.09.26, Источник: Дополнительный доход, Сумма: 20000, Налоговая ставка: 13,5%, Доход после налогообложения: {expectedNetIncome}"));
        }

        [Test]
        public void TaxedIncome_GetNetIncome_ShouldCalculateCorrectly()
        {
            var taxedIncome = new TaxedIncome
            {
                Amount = 20000,
                TaxRate = 13.5
            };

            var netIncome = taxedIncome.GetNetIncome();

            Assert.That(netIncome, Is.EqualTo(20000 - (20000 * 13.5 / 100)));
        }
        [Test]
        public void ProcessEntries_InvalidEntry_ShouldCatchExceptionAndAddToErrors()
        {
            string input = "Некорректная запись";

            var (_, errors) = IncomeFactory.ProcessEntries(input);

            Assert.That(errors.Count, Is.EqualTo(1), "Список ошибок должен содержать одну запись.");
            StringAssert.Contains("Запись не соответствует ни одному известному формату", errors[0], "Сообщение об ошибке некорректно.");
        }

    }
}