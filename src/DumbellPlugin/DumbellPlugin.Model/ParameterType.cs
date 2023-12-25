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
        Unknown,
    }
}
