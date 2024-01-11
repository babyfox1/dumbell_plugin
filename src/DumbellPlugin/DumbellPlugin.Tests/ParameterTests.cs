namespace DumbellPlugin.Tests
{
    using NUnit.Framework;
    using DumbellPlugin.Model;
    using NUnit.Framework.Legacy;

    public class ParameterTests
    {
        [TestCase(5, 10, 7)]
        public void ValidInitialization_SetsProperties(
            double min, double max, double current)
        {
            Parameter parameter = new Parameter(current, max, min);

            Assert.That(parameter.CurrentValue == current);
            Assert.That(parameter.MaxValue == max);
            Assert.That(parameter.MinValue == min);
        }

        [Test]
        public void CurrentMoreThanMax_ThrowsException()
        {
            double min = 5;
            double max = 10;
            double current = 15;

            var exception = Assert.Throws<ArgumentException>(() =>
                new Parameter(current, max, min));

            StringAssert.Contains("Текущее значение должно быть",
                exception.Message);
        }

        [Test]
        public void MinMoreThanMax_ThrowsException()
        {
            double min = 15;
            double max = 10;
            double current = 12;

            var exception = Assert.Throws<ArgumentException>(() =>
                new Parameter(current, max, min));

            StringAssert.Contains("Минимальное значение не может быть больше максимального",
                exception.Message);
        }
    }
}
