namespace DumbellPlugin.Tests
{
    using NUnit.Framework;
    using DumbellPlugin.Model;
    // TODO: нет XML-комментариев или описаний тестов
    // TODO: тесты на валидатор не покрывают полностью код модели валидатора
    public class ValidatorTests
    {
        [Test]
        public void ValidateRange_ValueInRange_DoesNotThrow()
        {
            // Arrange & Act
            double current = 10;
            double min = 5;
            double max = 15;

            // TODO: использовать Assert.DoesNotThrow
            Validator.ValidateRange(current, min, max);

            // Assert
            // Тест пройден, если не было исключения 
        }

        [Test]
        public void ValidateRange_ValueOutOfRange_ThrowsException()
        {
            // Arrange & Act & Assert
            // TODO: в Assert писать только то, что должно тестироваться. Подготовка данных отдельно делается
            Assert.Throws<ArgumentException>(() => {
                double current = 20;
                double min = 5;
                double max = 15;

                Validator.ValidateRange(current, min, max);
            });
        }

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
            // TODO: вместо переменной послать сразу вызов метода
            // TODO: так писать нельзя "result == true". Есть другой способ "result"
            Assert.That(result == true);
        }

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
            // TODO: вместо переменной послать сразу вызов метода
            // TODO: так писать нельзя "result == false". Есть другой способ "!result"
            Assert.That(result == false);
        }
    }
}