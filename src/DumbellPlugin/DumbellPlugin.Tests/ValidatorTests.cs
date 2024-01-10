
namespace DumbellPlugin.Tests
{
    using DumbellPlugin.Model;
    using NUnit.Framework;

    [TestFixture]
    public class ValidatorTests
    {
        [TestCase(100, -100, 100,
            Description = "Check if the maximum number is within the specified range")]
        [TestCase(-100, -100, 100,
            Description = "Check if the minimum number is within the specified range")]
        [TestCase(0, -100, 100,
            Description = "Check if zero is within the specified range")]
        [TestCase(100, 100, 100,
            Description = "Check if both maximum and minimum numbers are within the specified range")]
        public void TestIsValueInRange_ValueInRange_ResultEqual(
            double currentValue,
            double minValue,
            double maxValue)
        {
            // Act
            var result = Validator.IsNumberInRange(currentValue, minValue, maxValue);

            // Assert
            Assert.IsTrue(result);
        }

        [TestCase(101, -100, 100,
            Description = "Check if the specified number is beyond the maximum range")]
        [TestCase(-100.01, -100, 100,
            Description = "Check if the specified number is beyond the minimum range")]
        public void TestIsValueInRange_ValueNotInRange_ResultEqual(
            double currentValue,
            double minValue,
            double maxValue)
        {
            // Act
            var result = Validator.IsNumberInRange(currentValue, minValue, maxValue);

            // Assert
            Assert.IsFalse(result);
        }
    }
}
