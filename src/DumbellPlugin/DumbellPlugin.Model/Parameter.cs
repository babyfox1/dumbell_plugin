// <copyright file="Parameter.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DumbellPlugin.Model
{
    /// <summary>
    /// Представляет параметр с текущим значением, максимальным и минимальным
    /// значениями.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// Инициализирует новый экземпляр класса Parameter с указанными
        /// значениями.
        /// </summary>
        /// <param name="currentValue">Текущее значение параметра.</param>
        /// <param name="maxValue">Максимальное значение параметра.</param>
        /// <param name="minValue">Минимальное значение параметра.</param>
        public Parameter(
            double currentValue,
            double maxValue,
            double minValue)
        {
            Validator.ValidateNonNegative(minValue);
            Validator.ValidateMinMax(minValue, maxValue);
            Validator.ValidateRange(currentValue, minValue, maxValue);

            this.CurrentValue = currentValue;
            this.MaxValue = maxValue;
            this.MinValue = minValue;
        }

        /// <summary>
        /// Gets or sets получает или задает текущее значение параметра.
        /// </summary>
        public double CurrentValue { get; set; }

        /// <summary>
        /// Gets or sets получает или задает максимальное значение параметра.
        /// </summary>
        public double MaxValue { get; set; }

        /// <summary>
        /// Gets or sets получает или задает минимальное значение параметра.
        /// </summary>
        public double MinValue { get; set; }
    }
}
