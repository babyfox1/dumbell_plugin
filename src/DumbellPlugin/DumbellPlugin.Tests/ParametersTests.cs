namespace DumbellPlugin.Tests
{
    using DumbellPlugin.Model;
    using NUnit.Framework;

    /// <summary>
    /// Тесты для класса Parameters.
    /// </summary>
    public class ParametersTests
    {
        // TODO: RSDN
        private Parameters parameters;

        [SetUp]
        public void Setup()
        {
            parameters = new Parameters();
        }

        /// <summary>
        /// Проверка, что конструктор корректно инициализирует параметры.
        /// </summary>
        [Test]
        public void Ctor_InitializesParameters()
        {
            Assert.That(parameters.ParametersDict, Is.Not.Null);
            Assert.That(parameters.ParametersDict.Count, Is.EqualTo(8));
        }

        /// <summary>
        /// Проверка, что метод GetParameter возвращает корректное значение
        /// для допустимого типа параметра.
        /// </summary>
        [Test]
        public void GetParameter_WithValidType_ReturnsValue()
        {
            var length = parameters.GetParameter(ParameterType.LengthHandle);

            Assert.That(length, Is.EqualTo(500));
        }

        /// <summary>
        /// Проверка, что метод GetParameter выбрасывает исключение ArgumentException 
        /// для недопустимого типа параметра.
        /// </summary>
        [Test]
        public void GetParameter_WithInvalidType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                parameters.GetParameter((ParameterType)100));
        }

        /// <summary>
        /// Проверка, что метод AssertParameter корректно устанавливает 
        /// новое значение для допустимых данных.
        /// </summary>
        [Test]
        public void AssertParameter_WithValidData_SetsNewValue()
        {
            parameters.AssertParameter(ParameterType.LengthHandle,
               parameters.ParametersDict[ParameterType.LengthHandle], 550);

            var length = parameters.GetParameter(ParameterType.LengthHandle);

            Assert.That(length, Is.EqualTo(550));
        }

        /// <summary>
        /// Проверка, что метод AssertParameter выбрасывает исключение 
        /// ArgumentException для значения параметра вне допустимого диапазона.
        /// </summary>
        [Test]
        public void AssertParameter_ValueOutOfRange_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                parameters.AssertParameter(
                    ParameterType.LengthHandle,
                    parameters.ParametersDict[ParameterType.LengthHandle],
                    700)
            );
        }
    }
}
