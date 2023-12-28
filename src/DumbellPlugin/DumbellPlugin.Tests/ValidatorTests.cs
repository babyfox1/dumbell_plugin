// <copyright file="ValidatorTests.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

using DumbellPlugin.Model;
using NUnit.Framework;

/// <summary>
/// Тесты валидации.
/// </summary>
[TestFixture]
public class ValidatorTests
{
    /// <summary>
    /// Тест1.
    /// </summary>
    /// <param name="value">Парам3.</param>
    /// <param name="minValue">Парам1.</param>
    /// <param name="maxValue">Парам2.</param>
    [TestCase(5.0, 1.0, 10.0)]
    [TestCase(7.5, 5.0, 10.0)]
    public void ValidateRange_ShouldNotThrowException(
        double value,
        double minValue,
        double maxValue)
    {
        // Act, Assert
        Assert.DoesNotThrow(() =>
            Validator.ValidateRange(value, minValue, maxValue));
    }

    /// <summary>
    /// Тест2.
    /// </summary>
    /// <param name="value">Парам3.</param>
    /// <param name="minValue">Парам1.</param>
    /// <param name="maxValue">Парам2.</param>
    [TestCase(5.0, 10.0, 1.0)]
    [TestCase(7.5, 10.0, 5.0)]
    public void ValidateRange_ShouldThrowArgumentException(
        double value,
        double minValue,
        double maxValue)
    {
        // Act, Assert
        Assert.Throws<ArgumentException>(() =>
            Validator.ValidateRange(value, minValue, maxValue));
    }

    /// <summary>
    /// Тест3.
    /// </summary>
    /// <param name="minValue">Парам1.</param>
    /// <param name="maxValue">Парам2.</param>
    [TestCase(5.0, 10.0)]
    [TestCase(0.5, 5.0)]
    public void ValidateMinMax_ShouldNotThrowException(
        double minValue,
        double maxValue)
    {
        // Act, Assert
        Assert.DoesNotThrow(() =>
            Validator.ValidateMinMax(minValue, maxValue));
    }

    /// <summary>
    /// Тест4.
    /// </summary>
    /// <param name="minValue">Парам1.</param>
    /// <param name="maxValue">Парам2.</param>
    [TestCase(5.0, 1.0)]
    [TestCase(8, 7.5)]
    public void ValidateMinMax_ShouldThrowArgumentException(
        double minValue,
        double maxValue)
    {
        // Act, Assert
        Assert.Throws<ArgumentException>(() =>
            Validator.ValidateMinMax(minValue, maxValue));
    }

    /// <summary>
    /// Тест5.
    /// </summary>
    /// <param name="value">Парам1.</param>
    [TestCase(5.0)]
    [TestCase(7.5)]
    public void ValidateNonNegative_ShouldNotThrowException(double value)
    {
        // Act, Assert
        Assert.DoesNotThrow(() => Validator.ValidateNonNegative(value));
    }

    /// <summary>
    /// Test1.
    /// </summary>
    /// <param name="value">Парам1.</param>
    [TestCase(-5.0)]
    [TestCase(0)]
    public void ValidateNonNegative_ShouldThrowArgumentException(
        double value)
    {
        // Act, Assert
        Assert.Throws<ArgumentException>(() =>
            Validator.ValidateNonNegative(value));
    }

    /// <summary>
    /// Test1.
    /// </summary>
    /// <param name="value">Парам1.</param>
    /// <param name="minValue">Парам2.</param>
    /// <param name="maxValue">Парам3.</param>
    [TestCase(5.0, 1.0, 10.0)]
    [TestCase(7.5, 5.0, 10.0)]
    public void AssertNumberIsInRange_ShouldNotThrowException(
        double value,
        double minValue,
        double maxValue)
    {
        // Act, Assert
        Assert.DoesNotThrow(() =>
            Validator.AssertNumberIsInRange(value, minValue, maxValue));
    }

    /// <summary>
    /// Test2.
    /// </summary>
    /// <param name="value">Парам1.</param>
    /// <param name="minValue">Парам2.</param>
    /// <param name="maxValue">Парам3.</param>
    [TestCase(5.0, 10.0, 1.0)]
    [TestCase(7.5, 10.0, 5.0)]
    public void AssertNumberIsInRange_ShouldThrowException(
        double value,
        double minValue,
        double maxValue)
    {
        // Act, Assert
        Assert.Throws<ArgumentException>(() =>
            Validator.AssertNumberIsInRange(value, minValue, maxValue));
    }
}