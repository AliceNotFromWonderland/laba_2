using Microsoft.VisualStudio.TestPlatform.TestHost;
using pis1;
namespace UnitTest
{
    public class Tests
    {
        //Проверяет, что метод FromStr корректно разбирает строку с данными о доходе и возвращает объект Income с правильными значениями даты, источника и суммы.
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

        //Проверяет, что метод FromStr выбрасывает исключение FormatException, когда передается строка с некорректным форматом данных о доходе.
        [Test]
        public void TestFromStr_InvalidIncomeString_ThrowsFormatException()
        {
            string input = "Неверный формат";

            Income income = new Income(DateTime.MinValue, "", 0);

            Assert.Throws<FormatException>(() => income.FromStr(input));
        }

        //Проверяет, что метод FromStr для класса OrganizationIncome корректно разбирает строку с данными о доходе и возвращает объект OrganizationIncome с правильными значениями даты, источника, суммы, имени организации и типа операции.
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

        //Проверяет, что метод ChooseIncomeType возвращает объект Income при передаче корректной строки с данными о доходе.
        [Test]
        public void TestChooseIncomeType_ValidInput_ReturnsIncome()
        {
            string input = "2023.09.24 \"Ежемесячная стипендия\" 100000";
            var result = IncomeFactory.ChooseIncomeType(input);

            Assert.IsInstanceOf<Income>(result);
        }

        //Проверяет, что метод ChooseIncomeType выбрасывает исключение ArgumentException, когда передается строка с некорректными данными, которые не могут быть разобраны в доход.
        [Test]
        public void TestChooseIncomeType_InvalidInput_ThrowsException()
        {
            string input = "Некорректные данные";

            Assert.Throws<ArgumentException>(() => IncomeFactory.ChooseIncomeType(input));
        }

        //Проверяет, что метод ProcessEntries корректно разбирает строку с несколькими записями о доходе и возвращает список из двух объектов Income или OrganizationIncome.
        [Test]
        public void TestProcessEntries_ValidInput_ReturnsListOfIncomes()
        {
            string input = "2023.09.24 \"Ежемесячная стипендия\" 100000; " +
                           "2023.09.25 \"Премия\" 5000000 \"Газпром\" \"Начисление\"";

            List<Income> incomes = IncomeFactory.ProcessEntries(input);

            Assert.That(incomes.Count, Is.EqualTo(2));
        }

        //Проверяет, что метод ProcessEntries выводит сообщение об ошибке в консоль, когда передается некорректная строка. Также подтверждает, что список доходов остается пустым.
        [Test]
        public void TestProcessEntries_InvalidInput_OutputsErrorMessage()
        {
            string input = "Некорректные данные; ";

            var check = new StringWriter(); // StringWriter - объект, который является текстовым буфером для хранения строки.
            Console.SetOut(check); // Перенаправляем вывод в StringWriter, а не в консольный ConcoleWriteLine  - используется для перехвата всех сообщений об ошибках или других выводах программы, которые обычно отображаются в консоли, и их проверки. Затем тест проверяет, содержит ли захваченный вывод строку "Ошибка", что подтверждает правильное поведение программы при обработке некорректных данных.

            List<Income> incomes = IncomeFactory.ProcessEntries(input);

            string output = check.ToString();
            Assert.IsTrue(output.Contains("Ошибка"));
            Assert.That(incomes.Count, Is.EqualTo(0));
        }
    }
}