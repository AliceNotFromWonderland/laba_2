using Microsoft.VisualStudio.TestPlatform.TestHost;
using pis1;
namespace UnitTest
{
    public class Tests
    {       

        [Test]
        public void TestFromStr_ValidIncomeString_ReturnsIncomeObject()
        {
            string input = "2023.09.24 \"����������� ���������\" 100000";
            Income income = new Income(DateTime.MinValue, "", 0);
            var result = income.FromStr(input);

            Assert.IsInstanceOf<Income>(result);
            Assert.That(result.Date, Is.EqualTo(new DateTime(2023, 9, 24)));
            Assert.That(result.Source, Is.EqualTo("����������� ���������"));
            Assert.That(result.Amount, Is.EqualTo(100000));

        }

        [Test]
        public void TestFromStr_InvalidIncomeString_ThrowsFormatException()
        {
            string input = "�������� ������";

            Income income = new Income(DateTime.MinValue, "", 0);

            Assert.Throws<FormatException>(() => income.FromStr(input));
        }

        [Test]
        public void TestFromStr_ValidOrganizationIncomeString_ReturnsOrganizationIncomeObject()
        {
            string input = "2023.09.25 \"������\" 5000000 \"�������\" \"����������\"";
            OrganizationIncome orgIncome = new OrganizationIncome(DateTime.MinValue, "", 0, "", "");

            var result = orgIncome.FromStr(input);

            Assert.IsInstanceOf<OrganizationIncome>(result);
            Assert.That(result.Date, Is.EqualTo(new DateTime(2023, 9, 25)));
            Assert.That(result.Source, Is.EqualTo("������"));
            Assert.That(result.Amount, Is.EqualTo(5000000));
            Assert.That(((OrganizationIncome)result).OrganizationName, Is.EqualTo("�������"));
            Assert.That(((OrganizationIncome)result).OperationType, Is.EqualTo("����������"));

        }

        [Test]
        public void TestChooseIncomeType_ValidInput_ReturnsIncome()
        {
            string input = "2023.09.24 \"����������� ���������\" 100000";
            var result = IncomeParser.ChooseIncomeType(input);

            Assert.IsInstanceOf<Income>(result);
        }

        [Test]
        public void TestChooseIncomeType_InvalidInput_ThrowsException()
        {
            string input = "������������ ������";

            Assert.Throws<ArgumentException>(() => IncomeParser.ChooseIncomeType(input));
        }

        [Test]
        public void TestProcessEntries_ValidInput_ReturnsListOfIncomes()
        {
            string input = "2023.09.24 \"����������� ���������\" 100000; " +
                           "2023.09.25 \"������\" 5000000 \"�������\" \"����������\"";

            List<Income> incomes = IncomeParser.ProcessEntries(input);

            Assert.That(incomes.Count, Is.EqualTo(2));
        }

        [Test]
        public void TestProcessEntries_InvalidInput_OutputsErrorMessage()
        {
            string input = "������������ ������; ";

            var sw = new StringWriter();
            Console.SetOut(sw); // �������������� ����� � StringWriter

            List<Income> incomes = IncomeParser.ProcessEntries(input);

            string output = sw.ToString();
            Assert.IsTrue(output.Contains("������"));
            Assert.That(incomes.Count, Is.EqualTo(0));
        }
    }
}