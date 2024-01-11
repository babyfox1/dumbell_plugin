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

        /// <summary>
        /// Неизвестный тип параметра.
        /// </summary>
        // TODO: нигде не использует это значение,
        // только в TextBox_TextChanged для задания значения в parameterType.
        // Вместо этого можно использовать тип "ParameterType?".
        // Данный тип говорит о том, что он может иметь значения, что и в перечисление,
        // а также может принимать значение null.
        // В вашем примере можно будет заменить так "ParameterType? parameterType = null;".
        Unknown,
    }
}
