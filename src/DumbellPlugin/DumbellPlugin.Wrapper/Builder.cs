// <copyright file="Builder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DumbellPlugin.Wrapper
{
    using DumbellPlugin.Model;
    using Kompas6API5;
    using Kompas6Constants3D;

    /// <summary>
    /// Класс для построения деталей в KOMPAS-3D.
    /// </summary>
    public class Builder
    {
        /// <summary>
        /// Значение угла для лесенки из дисков.
        /// </summary>
        public double Degrees;

        /// <summary>
        /// Экземпляр класса обертки.
        /// </summary>
        private readonly Wrapper _wrapper = new Wrapper();

        /// <summary>
        /// Парраметр.
        /// </summary>
        private Parameters _parameters = new Parameters();

        /// <summary>
        /// Строит деталь на основе заданных параметров.
        /// </summary>
        /// <param name="parameters">Параметры для построения детали.</param>
        public void BuildDetail(Parameters parameters)
        {
            this._parameters = parameters;
            _wrapper.ConnectToKompas();
            _wrapper.CreateDocument3D();

            BuildHandle();
            BuildDisks();
            BuildFastening();
        }

        /// <summary>
        /// Строит рукоятку.
        /// </summary>
        public void BuildHandle()
        {
            double lengthHandle = _parameters.GetParameter(ParameterType.LengthHandle);
            double diameterHandle = _parameters.GetParameter(
                ParameterType.DiameterHandle);

            // Создаем эскиз влево
            var sketchHandle = _wrapper.CreateSketch(
                Obj3dType.o3d_planeXOZ, -1 * lengthHandle / 2);

            var document2d = (ksDocument2D)sketchHandle.BeginEdit();

            // Создаем круг радиусом 50(радиус рукоятки)
            document2d.ksCircle(0, 0, diameterHandle, 1);

            sketchHandle.EndEdit();

            // Теперь передаем этот эскиз в метод CreateExtrusion
            _wrapper.CreateExtrusion(sketchHandle, lengthHandle, false);
        }

        /// <summary>
        /// Строит диски детали.
        /// </summary>
        private void BuildDisks()
        {
            double amountDisk = _parameters.GetParameter(ParameterType.AmountDisk);
            double outerDiameterDisk = _parameters.GetParameter(ParameterType.OuterDiameterDisk);
            double widthDisk = _parameters.GetParameter(ParameterType.WidthDisk);
            double diameterHandle = _parameters.GetParameter(ParameterType.DiameterHandle);

            // Создаем внутренний диаметр диска чуть больше диаметра рукоятки
            const double handleInnerDiskProp = 1.05;
            double changeInnerDiameterDisk = diameterHandle * handleInnerDiskProp;

            // Создаем расстояние от центра до начала выдавливания дисков
            const double extrusionLengthDisk = 85.0;

            // Физическое расстояние между дисками
            const double lengthBetweenDisk = 2.0;

            // Присваиваем значение внешнего радиуса временной переменной,
            // которая будет уменьшаться.
            double prevOuterDiameter = outerDiameterDisk;

            // Расстояние между началом выдавливания дисков
            double offset = widthDisk + lengthBetweenDisk;

            // Синусы углов.
            const double sin60 = 0.866;
            const double sin45 = 0.707;
            const double sin90 = 1;

            // Переменная, которой как раз и будет присваиваться значение синуса.
            double tempSin;

            if (Degrees == 30)
            {
                tempSin = sin60;
            }
            else if (Degrees == 45)
            {
                tempSin = sin45;
            }
            else
            {
                tempSin = sin90;
            }

            for (int i = 0; i < amountDisk; i++)
            {
                double newOuterDiameter;
                if (i != 0)
                {
                    newOuterDiameter = prevOuterDiameter * tempSin;
                }
                else
                {
                    newOuterDiameter = prevOuterDiameter;
                }

                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                var sketch = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, extrusionLengthDisk + (i * offset));
                var document2d = (ksDocument2D)sketch.BeginEdit();

                // Внешний круг радиусом 200
                document2d.ksCircle(0, 0, newOuterDiameter, 1);

                // Внутренний круг радиусом 50
                document2d.ksCircle(0, 0, changeInnerDiameterDisk, 1);
                sketch.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                _wrapper.CreateExtrusion(sketch, widthDisk, false);
                prevOuterDiameter = newOuterDiameter;
            }

            for (int i = 0; i < amountDisk; i++)
            {
                double newOuterDiameter;
                if (i != 0)
                {
                    newOuterDiameter = prevOuterDiameter * tempSin;
                }
                else
                {
                    newOuterDiameter = prevOuterDiameter;
                }

                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                var sketch = _wrapper.CreateSketch(
                    Obj3dType.o3d_planeXOZ,
                    (-1 * extrusionLengthDisk) - widthDisk - (i * offset));
                var document2d = (ksDocument2D)sketch.BeginEdit();

                // Внешний круг радиусом 200
                document2d.ksCircle(0, 0, newOuterDiameter, 1);

                // Внутренний круг радиусом 50
                document2d.ksCircle(0, 0, changeInnerDiameterDisk, 1);
                sketch.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                _wrapper.CreateExtrusion(sketch, widthDisk, false);
                prevOuterDiameter = newOuterDiameter;
            }
        }

        /// <summary>
        /// Построение крепления.
        /// </summary>
        private void BuildFastening()
        {
            // double WidthDisk = _parameters.GetParameter(ParameterType.WidthDisk);
            double widthFasten =    _parameters.GetParameter(ParameterType.WidthFasten);
            double diameterHandle = _parameters.GetParameter(ParameterType.DiameterHandle);

            // Соотношение диаметров крепления и рукоятки
            const double ratioDiameterHandleFasten = 2.0;
            double changeDiameterFasten = diameterHandle * ratioDiameterHandleFasten;

            // Расстояние от центра, где будет начинать выдавливаться крепления
            const double lengthCreateFasten = 75.0;

            // Создаем эскиз для первого диска на расстоянии 120 от начала координат
            var sketch1 = _wrapper.CreateSketch(
                Obj3dType.o3d_planeXOZ, (-1 * lengthCreateFasten) - widthFasten);

            // Внешний круг радиусом 200.
            var document2d_1 = (ksDocument2D)sketch1.BeginEdit();
            document2d_1.ksCircle(0, 0, changeDiameterFasten, 1);

            sketch1.EndEdit();

            // Выдавливаем первый диск в положительном направлении на 20
            _wrapper.CreateExtrusion(sketch1, widthFasten, false);

            var sketch2 = _wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, 75);

            // Внешний круг радиусом 200.
            var document2d_2 = (ksDocument2D)sketch2.BeginEdit();
            document2d_2.ksCircle(0, 0, changeDiameterFasten, 1);

            sketch2.EndEdit();

            // Выдавливаем первый диск в положительном направлении на 20
            _wrapper.CreateExtrusion(sketch2, widthFasten, false);
        }
    }
}
