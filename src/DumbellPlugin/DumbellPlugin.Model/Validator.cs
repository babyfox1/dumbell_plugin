// <copyright file="Validator.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DumbellPlugin.Model
{
    using System;

    /// <summary>
    /// Статический класс, предоставляющий методы для проверки диапазона
    /// чисел.
    /// </summary>
    public static class Validator
    {
        /// <summary>
        /// Проверяет, что текущее значение находится в пределах от
        /// минимального до максимального.
        /// </summary>
        /// <param name="currentValue">Текущее значение параметра.</param>
        /// <param name="minValue">Минимальное значение параметра.</param>
        /// <param name="maxValue">Максимальное значение параметра.</param>
        public static void ValidateRange(
            double currentValue,
            double minValue,
            double maxValue)
        {
            if (currentValue < minValue || currentValue > maxValue)
            {
                var message = "Текущее значение должно быть в "
                              + "пределах от минимального до максимального.";
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Проверяет, что минимальное значение не больше максимального.
        /// </summary>
        /// <param name="minValue">Минимальное значение параметра.</param>
        /// <param name="maxValue">Максимальное значение параметра.</param>
        public static void ValidateMinMax(double minValue, double maxValue)
        {
            if (minValue > maxValue)
            {
                var message = "Минимальное значение не может "
                              + "быть больше максимального.";
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Проверяет, что число не меньше нуля.
        /// </summary>
        /// <param name="value">Проверяемое значение.</param>
        public static void ValidateNonNegative(double value)
        {
            if (value <= 0)
            {
                var message = "Значение должно быть больше нуля.";
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Проверяет, что значение находится в заданном числовом диапазоне.
        /// </summary>
        /// <param name="value">Проверяемое число.</param>
        /// <param name="min">Минимальное значение диапазона.</param>
        /// <param name="max">Максимальное значение диапазона.</param>
        /// <exception cref="Exception">Выбрасывается, если число не
        /// находится в допустимом диапазоне.</exception>
        public static void AssertNumberIsInRange(
            double value,
            double min,
            double max)
        {
            if (IsNumberInRange(value, min, max))
            {
                return;
            }

            var message = "Ваше число не попадает в диапазон доступных чисел!";
            throw new ArgumentException(message);
        }

        /// <summary>
        /// Проверяет, что значение находится в заданном числовом диапазоне.
        /// </summary>
        /// <param name="value">Проверяемое число.</param>
        /// <param name="min">Минимальное значение диапазона.</param>
        /// <param name="max">Максимальное значение диапазона.</param>
        /// <returns>True, если число находится в допустимом диапазоне. В
        /// противном случае - false.</returns>
        public static bool IsNumberInRange(
            double value,
            double min,
            double max)
        {
            return min <= value && value <= max;
        }
    }
}