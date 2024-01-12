// <copyright file="Builder.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace DumbellPlugin.Model
{
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
        private readonly Wrapper wrapper = new Wrapper();

        /// <summary>
        /// Парраметр.
        /// </summary>
        private Parameters parameters = new Parameters();

        /// <summary>
        /// Строит деталь на основе заданных параметров.
        /// </summary>
        /// <param name="parameters">Параметры для построения детали.</param>
        public void BuildDetail(Parameters parameters)
        {
            this.parameters = parameters;
            wrapper.ConnectToKompas();
            wrapper.CreateDocument3D();

            BuildHandle();
            BuildDisks();
            BuildFastening();
        }

        /// <summary>
        /// Строит рукоятку.
        /// </summary>
        public void BuildHandle()
        {
            double lengthHandle = parameters.GetParameter(ParameterType.LengthHandle);
            double diameterHandle = parameters.GetParameter(
                ParameterType.DiameterHandle);

            // Создаем эскиз влево
            var sketchHandle = wrapper.CreateSketch(
                Obj3dType.o3d_planeXOZ, -1 * lengthHandle / 2);

            var document2d = (ksDocument2D)sketchHandle.BeginEdit();

            // Создаем круг радиусом 50(радиус рукоятки)
            document2d.ksCircle(0, 0, diameterHandle, 1);

            sketchHandle.EndEdit();

            // Теперь передаем этот эскиз в метод CreateExtrusion
            wrapper.CreateExtrusion(sketchHandle, lengthHandle, false);
        }

        /// <summary>
        /// Строит диски детали.
        /// </summary>
        private void BuildDisks()
        {
            double amountDisk = parameters.GetParameter(ParameterType.AmountDisk);
            double outerDiameterDisk = parameters.GetParameter(ParameterType.OuterDiameterDisk);
            double widthDisk = parameters.GetParameter(ParameterType.WidthDisk);
            double diameterHandle = parameters.GetParameter(ParameterType.DiameterHandle);

            // Создаем внутренний диаметр диска чуть больше диаметра рукоятки
            // TODO: RSDN
            const double HANDLE_INNER_DISK_PROP = 1.05;
            double changeInnerDiameterDisk = diameterHandle * HANDLE_INNER_DISK_PROP;

            // Создаем расстояние от центра до начала выдавливания дисков
            const double EXTRUSION_LENGTH_DISK = 85.0;

            // Физическое расстояние между дисками
            const double LENGHT_BTW_DISKS = 2.0;

            // Присваиваем значение внешнего радиуса временной переменной,
            // которая будет уменьшаться.
            double prevOuterDiameter = outerDiameterDisk;

            // Расстояние между началом выдавливания дисков
            double offset = widthDisk + LENGHT_BTW_DISKS;

            // Синусы углов.
            const double SIN_60 = 0.866;
            const double SIN_45 = 0.707;
            const double SIN_90 = 1;

            // Переменная, которой как раз и будет присваиваться значение синуса.
            double tempSin;

            if (Degrees == 30)
            {
                tempSin = SIN_60;
            }
            else if (Degrees == 45)
            {
                tempSin = SIN_45;
            }
            else
            {
                tempSin = SIN_90;
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
                var sketch = wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, EXTRUSION_LENGTH_DISK + (i * offset));
                var document2d = (ksDocument2D)sketch.BeginEdit();

                // Внешний круг радиусом 200
                document2d.ksCircle(0, 0, newOuterDiameter, 1);

                // Внутренний круг радиусом 50
                document2d.ksCircle(0, 0, changeInnerDiameterDisk, 1);
                sketch.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                wrapper.CreateExtrusion(sketch, widthDisk, false);
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
                var sketch = wrapper.CreateSketch(
                    Obj3dType.o3d_planeXOZ,
                    (-1 * EXTRUSION_LENGTH_DISK) - widthDisk - (i * offset));
                var document2d = (ksDocument2D)sketch.BeginEdit();

                // Внешний круг радиусом 200
                document2d.ksCircle(0, 0, newOuterDiameter, 1);

                // Внутренний круг радиусом 50
                document2d.ksCircle(0, 0, changeInnerDiameterDisk, 1);
                sketch.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                wrapper.CreateExtrusion(sketch, widthDisk, false);
                prevOuterDiameter = newOuterDiameter;
            }
        }

        /// <summary>
        /// Построение крепления.
        /// </summary>
        private void BuildFastening()
        {
            // double WidthDisk = _parameters.GetParameter(ParameterType.WidthDisk);
            double widthFasten =    parameters.GetParameter(ParameterType.WidthFasten);
            double diameterHandle = parameters.GetParameter(ParameterType.DiameterHandle);

            // Соотношение диаметров крепления и рукоятки
            const double RATIO_DIAMETER_HANDLE_FASTEN = 2.0;
            double changeDiameterFasten = diameterHandle * RATIO_DIAMETER_HANDLE_FASTEN;

            // Расстояние от центра, где будет начинать выдавливаться крепления
            const double LENGTH_FASTEN_CREATE = 75.0;

            // Создаем эскиз для первого диска на расстоянии 120 от начала координат
            var sketch1 = wrapper.CreateSketch(
                Obj3dType.o3d_planeXOZ, (-1 * LENGTH_FASTEN_CREATE) - widthFasten);

            // Внешний круг радиусом 200.
            var document2d_1 = (ksDocument2D)sketch1.BeginEdit();
            document2d_1.ksCircle(0, 0, changeDiameterFasten, 1);

            sketch1.EndEdit();

            // Выдавливаем первый диск в положительном направлении на 20
            wrapper.CreateExtrusion(sketch1, widthFasten, false);

            var sketch2 = wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, 75);

            // Внешний круг радиусом 200.
            var document2d_2 = (ksDocument2D)sketch2.BeginEdit();
            document2d_2.ksCircle(0, 0, changeDiameterFasten, 1);

            sketch2.EndEdit();

            // Выдавливаем первый диск в положительном направлении на 20
            wrapper.CreateExtrusion(sketch2, widthFasten, false);
        }
    }
}
