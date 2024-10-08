using Microsoft.VisualStudio.TestPlatform.TestHost;
using pis1;
namespace UnitTest
{
    public class Tests
    {       

        [Test]
        public void TestFromStr_ValidIncomeString_ReturnsIncomeObject()
        {
            string input = "2023.09.24 \"Ежемесячная стипендия\" 100000";
            Income income = new Income(DateTime.MinValue, "", 0);
            var result = income.FromStr(input);

            Assert.IsInstanceOf<Income>(result);
            Assert.That(result.Date, Is.EqualTo(new DateTime(2023, 9, 24)));
            Assert.That(result.Source, Is.EqualTo("Ежемесячная стипендия"));
            Assert.That(result.Amount, Is.EqualTo(100000));

        }

        [Test]
        public void TestFromStr_InvalidIncomeString_ThrowsFormatException()
        {
            string input = "Неверный формат";

            Income income = new Income(DateTime.MinValue, "", 0);

            Assert.Throws<FormatException>(() => income.FromStr(input));
        }

        [Test]
        public void TestFromStr_ValidOrganizationIncomeString_ReturnsOrganizationIncomeObject()
        {
            string input = "2023.09.25 \"Премия\" 5000000 \"Газпром\" \"Начисление\"";
            OrganizationIncome orgIncome = new OrganizationIncome(DateTime.MinValue, "", 0, "", "");

            var result = orgIncome.FromStr(input);

            Assert.IsInstanceOf<OrganizationIncome>(result);
            Assert.That(result.Date, Is.EqualTo(new DateTime(2023, 9, 25)));
            Assert.That(result.Source, Is.EqualTo("Премия"));
            Assert.That(result.Amount, Is.EqualTo(5000000));
            Assert.That(((OrganizationIncome)result).OrganizationName, Is.EqualTo("Газпром"));
            Assert.That(((OrganizationIncome)result).OperationType, Is.EqualTo("Начисление"));

        }

        [Test]
        public void TestChooseIncomeType_ValidInput_ReturnsIncome()
        {
            string input = "2023.09.24 \"Ежемесячная стипендия\" 100000";
            var result = IncomeParser.ChooseIncomeType(input);

            Assert.IsInstanceOf<Income>(result);
        }

        [Test]
        public void TestChooseIncomeType_InvalidInput_ThrowsException()
        {
            string input = "Некорректные данные";

            Assert.Throws<ArgumentException>(() => IncomeParser.ChooseIncomeType(input));
        }

        [Test]
        public void TestProcessEntries_ValidInput_ReturnsListOfIncomes()
        {
            string input = "2023.09.24 \"Ежемесячная стипендия\" 100000; " +
                           "2023.09.25 \"Премия\" 5000000 \"Газпром\" \"Начисление\"";

            List<Income> incomes = IncomeParser.ProcessEntries(input);

            Assert.That(incomes.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestProcessEntries_InvalidInput_OutputsErrorMessage()
        {
            string input = "Некорректные данные; ";

            var sw = new StringWriter();
            Console.SetOut(sw); // Перенаправляем вывод в StringWriter

            List<Income> incomes = IncomeParser.ProcessEntries(input);

            string output = sw.ToString();
            Assert.IsTrue(output.Contains("Ошибка"));
            Assert.That(incomes.Count, Is.EqualTo(0));
        }
    }
}