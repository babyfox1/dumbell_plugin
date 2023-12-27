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
        /// Словарь, содержащий параметры с их типами.
        /// </summary>
        public Dictionary<ParameterType, Parameter> ParametersDict =
            new Dictionary<ParameterType, Parameter>();

 /*       /// <summary>
        /// Отступ от возможных граничных параметров детали.
        /// </summary>
        private readonly double _borderOffset = 49;*/

        /// <summary>
        /// Инициализирует новый экземпляр класса Parameters со значениями
        /// параметров по умолчанию.
        /// </summary>
        public Parameters()
        {
            ParametersDict.Add(
                ParameterType.LengthHandle,
                new Parameter(340, 500, 300));
            ParametersDict.Add(
                ParameterType.DiameterHandle,
                new Parameter(25, 50, 20));
            ParametersDict.Add(
                ParameterType.WidthFasten,
                new Parameter(10, 10, 10));
            ParametersDict.Add(
                ParameterType.DiameterFasten,
                new Parameter(60, 90, 50));
            ParametersDict.Add(
                ParameterType.AmountDisk,
                new Parameter(3, 5, 1));
            ParametersDict.Add(
                ParameterType.OuterDiameterDisk,
                new Parameter(250, 400, 150));
            ParametersDict.Add(
                ParameterType.InnerDiameterDisk,
                new Parameter(50, 90, 40));
            ParametersDict.Add(
                ParameterType.WidthDisk,
                new Parameter(20, 30, 10));
        }



        public Dictionary<ParameterType, double> GetParametersCurrentValues()
        {
            var parametersCurrentValues = new Dictionary<ParameterType, double>();

            foreach (var item in ParametersDict)
            {
                parametersCurrentValues.Add(item.Key, item.Value.CurrentValue);
            }

            return parametersCurrentValues;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameterType"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        public double GetParameter(ParameterType parameterType)
        {
            if (ParametersDict.ContainsKey(parameterType))
            {
                return ParametersDict[parameterType].CurrentValue;
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
            ParametersDict[parameterType].CurrentValue = value;
            ChangeParametersRangeValues(parameterType, parameter);
        }

        /// <summary>
        /// Изменяет значения диапазона других параметров в зависимости от
        /// изменения указанного параметра.
        /// </summary>
        /// <param name="parameterType">Тип параметра, который был изменен.</param>
        /// <param name="parameter">Измененный параметр.</param>
        public void ChangeParametersRangeValues(
        ParameterType parameterType,
        Parameter parameter)
        {
            switch (parameterType)
            {
                case ParameterType.LengthHandle:
                    // Длина рукоятки H (Hmin = 320 — Hmax = 400мм)
                    ParametersDict[ParameterType.LengthHandle].MinValue = 300.0; // Hmin = 320
                    ParametersDict[ParameterType.LengthHandle].MaxValue = 500.0; //  Hmax = 400
                    break;

                case ParameterType.DiameterHandle:
                    // Диаметр рукоятки R (Rmin = 20 — Rmax = 40мм)
                    ParametersDict[ParameterType.DiameterHandle].MinValue = 20.0; // Rmin = 20
                    ParametersDict[ParameterType.DiameterHandle].MaxValue = 50.0; // Rmax = 40
                    break;

                case ParameterType.WidthFasten:
                    // Ширина крепления l2 (10мм)
                    ParametersDict[ParameterType.WidthFasten].MinValue = 10.0;
                    ParametersDict[ParameterType.WidthFasten].MaxValue = 10.0;
                    break;

                case ParameterType.DiameterFasten:
                    // Диаметр крепления k (Rmin*2 — Rmax *2мм)
                    ParametersDict[ParameterType.DiameterFasten].MinValue = 50.0; // Rmin*2 = 20*2
                    ParametersDict[ParameterType.DiameterFasten].MaxValue = 90.0; // Rmax*2 = 40*2
                    break;

                case ParameterType.AmountDisk:
                    // Общее количество дисков n (1 - 5)
                    ParametersDict[ParameterType.AmountDisk].MinValue = 1;
                    ParametersDict[ParameterType.AmountDisk].MaxValue = 5;
                    break;

                case ParameterType.OuterDiameterDisk:
                    // Внешний диаметр дисков w1 (w1min = 100 — w1max = 150мм)
                    ParametersDict[ParameterType.OuterDiameterDisk].MinValue = 150.0;
                    ParametersDict[ParameterType.OuterDiameterDisk].MaxValue = 400.0;
                    break;

                case ParameterType.InnerDiameterDisk:
                    // Внутренний диаметр дисков w2 (R*m, где m [1.05-1.1])
                    ParametersDict[ParameterType.InnerDiameterDisk].MinValue = 40.0; // R*1.05, где Rmin = 20
                    ParametersDict[ParameterType.InnerDiameterDisk].MaxValue = 90.0; // R*1.1, где Rmax = 40
                    break;

                case ParameterType.WidthDisk:
                    // Ширина диска l1 (10 - 50мм)
                    ParametersDict[ParameterType.WidthDisk].MinValue = 10.0;
                    ParametersDict[ParameterType.WidthDisk].MaxValue = 30.0;
                    break;

                default:
                    var message = "Введен некорректный тип параметра";
                    throw new ArgumentException(message);
            }

        }

        /*public object GetParameter()
        {
            throw new NotImplementedException();
        }*/
    }
}