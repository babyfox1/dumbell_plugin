namespace DumbellPlugin.Tests
{
    using NUnit.Framework;
    using DumbellPlugin.Model;

    /// <summary>
    /// Тесты для класса Validator.
    /// </summary>
    public class ValidatorTests
    {
        /// <summary>
        /// Проверяет, что метод ValidateRange не выбрасывает исключение,
        /// если текущее значение находится в пределах от минимального до
        /// максимального значения.
        /// </summary>
        [Test]
        public void ValidateRange_ValueInRange_DoesNotThrow()
        {
            // Arrange
            double current = 10;
            double min = 5;
            double max = 15;

            // Assert
            Assert.DoesNotThrow(() => Validator.ValidateRange(current, min, max));
        }

        /// <summary>
        /// Проверяет, что метод ValidateRange выбрасывает исключение типа
        /// ArgumentException, если текущее значение находится вне пределов
        /// от минимального до максимального значения.
        /// </summary>
        [Test]
        public void ValidateRange_ValueOutOfRange_ThrowsException()
        {
            // Arrange
            double current = 20;
            double min = 5;
            double max = 15;

            // Act & Assert
            Assert.Throws<ArgumentException>(() => {
                Validator.ValidateRange(current, min, max);
            });
        }

        /// <summary>
        /// Проверяет, что метод ValidateMinMax не выбрасывает исключение,
        /// если минимальное значение не больше максимального.
        /// </summary>
        [Test]
        public void ValidateMinMax_ValidValues_DoesNotThrow()
        {
            // Arrange
            double min = 5;
            double max = 15;

            // Act & Assert
            Assert.DoesNotThrow(() => {
                Validator.ValidateMinMax(min, max);
            });
        }

        /// <summary>
        /// Проверяет, что метод ValidateMinMax выбрасывает исключение типа
        /// ArgumentException, если минимальное значение больше максимального.
        /// </summary>
        [Test]
        public void ValidateMinMax_MinMoreThanMax_ThrowsException()
        {
            double min = 15;
            double max = 10;
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => {
                Validator.ValidateMinMax(min, max);
            });
        }

        /// <summary>
        /// Проверяет, что метод ValidateNonNegative не выбрасывает исключение,
        /// если значение положительное.
        /// </summary>
        [Test]
        public void ValidateNonNegative_PositiveValue_DoesNotThrow()
        {
            double value = 10;
            // Arrange & Act & Assert
            Assert.DoesNotThrow(() => {
                Validator.ValidateNonNegative(value);
            });
        }

        /// <summary>
        /// Проверяет, что метод ValidateNonNegative выбрасывает исключение типа
        /// ArgumentException, если значение отрицательное.
        /// </summary>
        [Test]
        public void ValidateNonNegative_NegativeValue_ThrowsException()
        {
            double value = -5;
            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => {
                Validator.ValidateNonNegative(value);
            });
        }

        /// <summary>
        /// Проверяет, что метод AssertNumberIsInRange не выбрасывает исключение,
        /// если значение находится в пределах от минимального до максимального.
        /// </summary>
        [Test]
        public void AssertNumberIsInRange_ValueInRange_DoesNotThrow()
        {
            double value = 10;
            double min = 5;
            double max = 15;
            // Arrange & Act & Assert
            Assert.DoesNotThrow(() => {
                Validator.AssertNumberIsInRange(value, min, max);
            });
        }

        /// <summary>
        /// Проверяет, что метод AssertNumberIsInRange выбрасывает исключение типа
        /// ArgumentException, если значение находится вне пределов от
        /// минимального до максимального.
        /// </summary>
        [Test]
        public void AssertNumberIsInRange_ValueOutOfRange_ThrowsException()
        {
            double value = 20;
            double min = 5;
            double max = 15;

            // Arrange & Act & Assert
            Assert.Throws<ArgumentException>(() => {

                Validator.AssertNumberIsInRange(value, min, max);
            });
        }

        /// <summary>
        /// Проверяет, что метод IsNumberInRange возвращает true, если значение
        /// находится в пределах от минимального до максимального.
        /// </summary>
        [Test]
        public void IsNumberInRange_InRange_ReturnsTrue()
        {
            // Arrange 
            double value = 10;
            double min = 5;
            double max = 15;

            // Act
            bool result = Validator.IsNumberInRange(value, min, max);

            // Assert
            Assert.That(result == true);
        }

        /// <summary>
        /// Проверяет, что метод IsNumberInRange возвращает false, если значение
        /// находится вне пределов от минимального до максимального.
        /// </summary>
        [Test]
        public void IsNumberInRange_OutOfRange_ReturnsFalse()
        {
            // Arrange
            double value = 20;
            double min = 5;
            double max = 15;

            // Act 
            bool result = Validator.IsNumberInRange(value, min, max);

            // Assert
            Assert.That(result == false);
        }
    }
}
