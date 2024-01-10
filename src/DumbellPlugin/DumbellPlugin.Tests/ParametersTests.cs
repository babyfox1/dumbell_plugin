namespace DumbellPlugin.Tests
{
    using DumbellPlugin.Model;
    using NUnit.Framework;
    using System.Collections.Generic;

    [TestFixture]
    public class ParametersTests
    {
        [Test]
        public void Parameters_InitializedCorrectly()
        {
            // Arrange
            var parameters = new Parameters();

            // Act

            // Assert
            Assert.That(parameters.ParametersDict, Is.Not.Null);
            Assert.That(parameters.ParametersDict, Is.InstanceOf<Dictionary<ParameterType, Parameter>>());
        }

        [Test]
        public void Parameters_SetAndGetMethods_WorkingCorrectly()
        {
            // Arrange
            var hairbrushParameters = new Parameters();
            var newParameters = new Dictionary<ParameterType, Parameter>
            {
                { ParameterType.LengthHandle, new Parameter(60, 180, 120) },
            };

            // Act
            hairbrushParameters.ParametersDict = newParameters;
            var retrievedParameters = hairbrushParameters.ParametersDict;

            // Assert
            Assert.That(retrievedParameters, Is.EqualTo(newParameters));
        }

        [TestCase(ParameterType.LengthHandle, 50,
            Description = "Ensure that the value of LengthHandle is within the specified minimum and maximum values")]
        [TestCase(ParameterType.DiameterHandle, 20,
            Description = "Ensure that the value of DiameterHandle is within the specified minimum and maximum values")]
        // Add more test cases to cover different parameter types and values
        public void Parameters_ValuesInRange(ParameterType parameterType, int expectedValue)
        {
            // Arrange
            var parameters = new Parameters();

            // Act
            var actualValue = parameters.ParametersDict[parameterType].CurrentValue;

            // Assert
            Assert.That(actualValue, Is.GreaterThanOrEqualTo(parameters.ParametersDict[parameterType].MinValue));
            Assert.That(actualValue, Is.LessThanOrEqualTo(parameters.ParametersDict[parameterType].MaxValue));
        }
    }
}
