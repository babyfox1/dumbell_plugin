// <copyright file="Parameters.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DumbellPlugin.Model
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Представляет набор параметров с соответствующими типами.
    /// </summary>
    public class Parameters
    {
        /// <summary>
        /// ParametersDict.
        /// </summary>
        public Dictionary<ParameterType, Parameter> ParametersDict =
            new Dictionary<ParameterType, Parameter>();

        /// <summary>
        /// Initializes a new instance of the <see cref="Parameters"/> class.
        /// Инициализирует новый экземпляр класса Parameters со значениями
        /// параметров по умолчанию.
        /// </summary>
        public Parameters()
        {
            this.ParametersDict.Add(
                ParameterType.LengthHandle,
                new Parameter(500, 650, 500));
            this.ParametersDict.Add(
                ParameterType.DiameterHandle,
                new Parameter(30, 30, 20));
            this.ParametersDict.Add(
                ParameterType.WidthFasten,
                new Parameter(10, 15, 10));
            this.ParametersDict.Add(
                ParameterType.DiameterFasten,
                new Parameter(50, 50, 35));
            this.ParametersDict.Add(
                ParameterType.AmountDisk,
                new Parameter(3, 5, 1));
            this.ParametersDict.Add(
                ParameterType.OuterDiameterDisk,
                new Parameter(200, 200, 120));
            this.ParametersDict.Add(
                ParameterType.InnerDiameterDisk,
                new Parameter(31, 31, 21));
            this.ParametersDict.Add(
                ParameterType.WidthDisk,
                new Parameter(20, 30, 10));
        }

        /// <summary>
        /// Получить параметр.
        /// </summary>
        /// <param name="parameterType">Параметр1.</param>
        /// <returns>Воз.</returns>
        /// <exception cref="ArgumentException">Параметр2.</exception>
        public double GetParameter(ParameterType parameterType)
        {
            if (this.ParametersDict.ContainsKey(parameterType))
            {
                return this.ParametersDict[parameterType].CurrentValue;
            }
            else
            {
                throw new ArgumentException("Некорректный тип параметра");
            }
        }

        /// <summary>
        /// Проверяет и задает значение для указанного параметра, а затем
        /// обновляет диапазоны других параметров.
        /// </summary>
        /// <param name="parameterType">Тип параметра.</param>
        /// <param name="parameter">Экземпляр параметра.</param>
        /// <param name="value">Значение параметра для установки.</param>
        public void AssertParameter(
            ParameterType parameterType,
            Parameter parameter,
            double value)
        {
            Validator.AssertNumberIsInRange(
                value,
                parameter.MinValue,
                parameter.MaxValue);
            this.ParametersDict[parameterType].CurrentValue = value;
        }
    }
}