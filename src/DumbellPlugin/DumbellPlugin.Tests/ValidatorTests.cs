namespace DumbellPlugin.Tests
{
    using NUnit.Framework;
    using DumbellPlugin.Model;
    public class ValidatorTests
    {
        [Test]
        public void ValidateRange_ValueInRange_DoesNotThrow()
        {
            // Arrange & Act
            double current = 10;
            double min = 5;
            double max = 15;

            Validator.ValidateRange(current, min, max);

            // Assert
            // Тест пройден, если не было исключения 
        }

        [Test]
        public void ValidateRange_ValueOutOfRange_ThrowsException()
        {
            // Arrange & Act & Assert
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
            Assert.That(result == false);
        }
    }
}