// <copyright file="ParametersTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>
namespace DumbellPlugin.Tests
{
    using DumbellPlugin.Model;
    using NUnit.Framework;

    /// <summary>
    /// Юнит-тесты.
    /// </summary>
    [TestFixture]
    public class ParametersTests
    {
        /// <summary>
        /// Проверяет, что при создании объекта Parameters
        /// устанавливаются значения параметров по умолчанию.
        /// </summary>
        [Test]
        public void CreateParametersSetsDefaultValues()
        {
            var parameters = new Parameters();

            // TODO: код дублируется. Посмотрите в сторону атрибута TestCase, в который можно послать параметры.
            // В вашем случае это два параметра: ParameterType и число, которому должно оно быть равно.
            // Так сделать во всех тестах, где можно.
            Assert.That(parameters.GetParameter(ParameterType.LengthHandle), Is.EqualTo(500));
            Assert.That(parameters.GetParameter(ParameterType.DiameterHandle), Is.EqualTo(30));
            Assert.That(parameters.GetParameter(ParameterType.WidthFasten), Is.EqualTo(10));
            Assert.That(parameters.GetParameter(ParameterType.DiameterFasten), Is.EqualTo(50));
            Assert.That(parameters.GetParameter(ParameterType.AmountDisk), Is.EqualTo(3));
            Assert.That(parameters.GetParameter(ParameterType.OuterDiameterDisk), Is.EqualTo(200));
            Assert.That(parameters.GetParameter(ParameterType.InnerDiameterDisk), Is.EqualTo(31));
            Assert.That(parameters.GetParameter(ParameterType.WidthDisk), Is.EqualTo(20));
        }

        /// <summary>
        /// Проверяет, что вызов метода GetParameter с неверным типом
        /// параметра генерирует исключение ArgumentException.
        /// </summary>
        [Test]
        public void GetParameterWithInvalidTypeThrowsException()
        {
            var parameters = new Parameters();

            Assert.That(
                () => parameters.GetParameter(ParameterType.LengthHandle),
                Throws.TypeOf<ArgumentException>());

            Assert.That(
                () => parameters.GetParameter(ParameterType.DiameterHandle),
                Throws.TypeOf<ArgumentException>());

            Assert.That(
                () => parameters.GetParameter(ParameterType.WidthFasten),
                Throws.TypeOf<ArgumentException>());

            Assert.That(
                () => parameters.GetParameter(ParameterType.DiameterFasten),
                Throws.TypeOf<ArgumentException>());

            Assert.That(
                () => parameters.GetParameter(ParameterType.AmountDisk),
                Throws.TypeOf<ArgumentException>());

            Assert.That(
                () => parameters.GetParameter(ParameterType.OuterDiameterDisk),
                Throws.TypeOf<ArgumentException>());

            Assert.That(
                () => parameters.GetParameter(ParameterType.InnerDiameterDisk),
                Throws.TypeOf<ArgumentException>());

            Assert.That(
                () => parameters.GetParameter(ParameterType.WidthDisk),
                Throws.TypeOf<ArgumentException>());
        }

        /// <summary>
        /// Проверяет установку нового валидного значения параметра
        /// и корректность его сохранения.
        /// </summary>
        [Test]
        public void SetParameterWithInRangeValueSetsValue()
        {
            var parameters = new Parameters();

            parameters.AssertParameter(
              ParameterType.LengthHandle,
              parameters.ParametersDict[ParameterType.LengthHandle],
              550);

            Assert.That(parameters.GetParameter(ParameterType.LengthHandle), Is.EqualTo(550));

            parameters.AssertParameter(
                ParameterType.DiameterHandle,
                parameters.ParametersDict[ParameterType.DiameterHandle],
                25);
            Assert.That(parameters.GetParameter(ParameterType.DiameterHandle), Is.EqualTo(25));

            parameters.AssertParameter(
                ParameterType.WidthFasten,
                parameters.ParametersDict[ParameterType.WidthFasten],
                10);
            Assert.That(parameters.GetParameter(ParameterType.WidthFasten), Is.EqualTo(15));

            parameters.AssertParameter(
              ParameterType.DiameterFasten,
              parameters.ParametersDict[ParameterType.DiameterFasten],
              35);

            Assert.That(parameters.GetParameter(ParameterType.DiameterFasten), Is.EqualTo(35));

            parameters.AssertParameter(
                ParameterType.AmountDisk,
                parameters.ParametersDict[ParameterType.AmountDisk],
                4);
            Assert.That(parameters.GetParameter(ParameterType.AmountDisk), Is.EqualTo(4));

            parameters.AssertParameter(
                ParameterType.OuterDiameterDisk,
                parameters.ParametersDict[ParameterType.OuterDiameterDisk],
                150);
            Assert.That(parameters.GetParameter(ParameterType.OuterDiameterDisk), Is.EqualTo(150));

            parameters.AssertParameter(
                ParameterType.InnerDiameterDisk,
                parameters.ParametersDict[ParameterType.InnerDiameterDisk],
                26);
            Assert.That(parameters.GetParameter(ParameterType.InnerDiameterDisk), Is.EqualTo(26));

            parameters.AssertParameter(
                ParameterType.WidthDisk,
                parameters.ParametersDict[ParameterType.WidthDisk],
                15);
            Assert.That(parameters.GetParameter(ParameterType.WidthDisk), Is.EqualTo(15));
        }

        /// <summary>
        /// Проверяет генерацию исключения при попытке установить
        /// значение параметра вне допустимого диапазона.
        /// </summary>
        [Test]
        public void SetParameterWithOutOfRangeValueThrowsException()
        {
            var parameters = new Parameters();

            Assert.That(
                () => parameters.AssertParameter(
                ParameterType.LengthHandle,
                parameters.ParametersDict[ParameterType.LengthHandle],
                700),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => parameters.AssertParameter(
                ParameterType.DiameterHandle,
                parameters.ParametersDict[ParameterType.DiameterHandle],
                700),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => parameters.AssertParameter(
                ParameterType.WidthFasten,
                parameters.ParametersDict[ParameterType.WidthFasten],
                12),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => parameters.AssertParameter(
                ParameterType.DiameterFasten,
                parameters.ParametersDict[ParameterType.DiameterFasten],
                100),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => parameters.AssertParameter(
                ParameterType.AmountDisk,
                parameters.ParametersDict[ParameterType.AmountDisk],
                10),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => parameters.AssertParameter(
                ParameterType.OuterDiameterDisk,
                parameters.ParametersDict[ParameterType.OuterDiameterDisk],
                700),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => parameters.AssertParameter(
                ParameterType.InnerDiameterDisk,
                parameters.ParametersDict[ParameterType.InnerDiameterDisk],
                40),
                Throws.TypeOf<ArgumentOutOfRangeException>());

            Assert.That(
                () => parameters.AssertParameter(
                ParameterType.WidthDisk,
                parameters.ParametersDict[ParameterType.WidthDisk],
                40),
                Throws.TypeOf<ArgumentOutOfRangeException>());
        }
    }
}