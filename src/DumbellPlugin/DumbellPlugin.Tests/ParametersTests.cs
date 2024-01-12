namespace DumbellPlugin.Tests
{
    using DumbellPlugin.Model;
    using NUnit.Framework;

    public class ParametersTests
    {
        private Parameters parameters;

        [SetUp]
        public void Setup()
        {
            parameters = new Parameters();
        }

        [Test]
        public void Ctor_InitializesParameters()
        {
            Assert.That(parameters.ParametersDict, Is.Not.Null);
            Assert.That(parameters.ParametersDict.Count, Is.EqualTo(8));
        }

        [Test]
        public void GetParameter_WithValidType_ReturnsValue()
        {
            var length = parameters.GetParameter(ParameterType.LengthHandle);

            Assert.That(length, Is.EqualTo(500));
        }

        [Test]
        public void GetParameter_WithInvalidType_ThrowsException()
        {
            Assert.Throws<ArgumentException>(() =>
                parameters.GetParameter((ParameterType)100)); 
        }

        [Test]
        public void AssertParameter_WithValidData_SetsNewValue()
        {
            parameters.AssertParameter(ParameterType.LengthHandle,
               parameters.ParametersDict[ParameterType.LengthHandle], 550);

            var length = parameters.GetParameter(ParameterType.LengthHandle);

            Assert.That(length, Is.EqualTo(550));
        }

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