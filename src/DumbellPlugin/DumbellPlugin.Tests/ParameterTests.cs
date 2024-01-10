namespace DumbellPlugin.Tests
{
    using DumbellPlugin.Model;
    using NUnit.Framework;

    /// <summary>
    /// Unit tests for the Parameter class.
    /// </summary>
    [TestFixture]
    public class ParameterTests
    {
        [TestCase(10, 50, 20,
            Description = "Initialization of parameter with valid values")]
        [TestCase(2, 5, 3,
            Description = "Initialization of parameter with other valid values")]
        [TestCase(10, 40, 20,
            Description = "Initialization of parameter with different valid values")]
        public void Parameter_Initialization_SetPropertiesCorrectly(
            double minValue,
            double maxValue,
            double currentValue)
        {
            // Arrange, Act
            var parameter = new Parameter(minValue, maxValue, currentValue);

            // Assert
            Assert.AreEqual(currentValue, parameter.CurrentValue);
            Assert.AreEqual(maxValue, parameter.MaxValue);
            Assert.AreEqual(minValue, parameter.MinValue);
        }

        [TestCase(0, 10.0, 20,
            Description = "Initialization of parameter with currentValue greater than maxValue")]
        [TestCase(150, 200, 50,
            Description = "Initialization of parameter with minValue greater than currentValue")]
        [TestCase(3, 20, 2,
            Description = "Initialization of parameter with currentValue less than minValue")]
        public void Parameter_Initialization_CurrentValueOutOfRange_ThrowArgumentException(
            double minValue,
            double maxValue,
            double currentValue)
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() =>
                new Parameter(minValue, maxValue, currentValue));
        }

        [TestCase(8, 5, 7,
            Description = "Initialization of parameter with minValue greater than maxValue")]
        [TestCase(30, 20, 25,
            Description = "Initialization of parameter with other values where minValue greater than maxValue")]
        [TestCase(250, 200, 225,
            Description = "Initialization of parameter with different values where minValue greater than maxValue")]
        public void Parameter_Initialization_MinValueGreaterThanMaxValue_ThrowArgumentException(
            double minValue,
            double maxValue,
            double currentValue)
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() =>
                new Parameter(minValue, maxValue, currentValue));
        }

        [TestCase(0, 9, -6,
            Description = "Initialization of parameter with negative currentValue")]
        [TestCase(0, 40, -20,
            Description = "Initialization of parameter with other negative currentValue")]
        [TestCase(0, 1000, -100,
            Description = "Initialization of parameter with another negative currentValue")]
        public void Parameter_Initialization_WithNegativeValues_ShouldThrowArgumentException(
            double minValue,
            double maxValue,
            double currentValue)
        {
            // Arrange, Act, Assert
            Assert.Throws<ArgumentException>(() =>
                new Parameter(minValue, maxValue, currentValue));
        }
    }
}
