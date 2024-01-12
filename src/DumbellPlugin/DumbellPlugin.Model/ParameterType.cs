// <copyright file="ParameterType.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DumbellPlugin.Model
{
    /// <summary>
    /// Типы параметров, используемые в расчетах.
    /// </summary>
    public enum ParameterType
    {
        /// <summary>
        /// Длина рукоятки.
        /// </summary>
        LengthHandle,

        /// <summary>
        /// Диаметр рукоятки.
        /// </summary>
        DiameterHandle,

        /// <summary>
        /// Ширина крепления.
        /// </summary>
        WidthFasten,

        /// <summary>
        /// Диаметр крепления.
        /// </summary>
        DiameterFasten,

        /// <summary>
        /// Количество дисков.
        /// </summary>
        AmountDisk,

        /// <summary>
        /// Внешний диаметр диска.
        /// </summary>
        OuterDiameterDisk,

        /// <summary>
        /// Внутренний диаметр диска.
        /// </summary>
        InnerDiameterDisk,

        /// <summary>
        /// Ширина диска.
        /// </summary>
        WidthDisk,
    }
}
