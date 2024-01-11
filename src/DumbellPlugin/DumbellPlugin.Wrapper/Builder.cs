﻿// <copyright file="Builder.cs" company="PlaceholderCompany">
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
        /// Экземпляр класса обертки.
        /// </summary>
        private readonly Wrapper wrapper = new Wrapper();

        /// <summary>
        /// Парраметр.
        /// </summary>
        private Parameters parameters = new Parameters();

        /// <summary>
        /// Initializes a new instance of the <see cref="Builder"/> class.
        /// Конструктор.
        /// </summary>
        // TODO: если не используется, то убрать
        public Builder()
        {
        }

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
        /// Ladder.
        /// </summary>
        /// <param name="parameters">Patram.</param>
        public void BuildDetail30Ladder(Parameters parameters)
        {
            this.parameters = parameters;
            wrapper.ConnectToKompas();
            wrapper.CreateDocument3D();

            BuildHandle();
            BuildDisks30Ladder();
            BuildFastening();
        }

        /// <summary>
        /// Ladder.
        /// </summary>
        /// <param name="parameters">Patram.</param>
        public void BuildDetail45Ladder(Parameters parameters)
        {
            this.parameters = parameters;
            wrapper.ConnectToKompas();
            wrapper.CreateDocument3D();

            BuildHandle();
            BuildDisks45Ladder();
            BuildFastening();
        }

        /// <summary>
        /// Строит рукоятку.
        /// </summary>
        public void BuildHandle()
        {
            double lengthHandle = parameters.GetParameter(ParameterType.LengthHandle);
            double diameterHandle = parameters.GetParameter(ParameterType.DiameterHandle);

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
        // TODO: код дублируется с BuildDisks30Ladder и BuildDisks45Ladder
        private void BuildDisks()
        {
            double amountDisk = parameters.GetParameter(ParameterType.AmountDisk);
            double outerDiameterDisk = parameters.GetParameter(ParameterType.OuterDiameterDisk);
            double widthDisk = parameters.GetParameter(ParameterType.WidthDisk);
            double diameterHandle = parameters.GetParameter(ParameterType.DiameterHandle);

            // Создаем внутренний диаметр диска чуть больше диаметра рукоятки
            // TODO: RSDN Все еще неправильное оформление (см. https://rsdn.org/article/mag/200401/codestyle.XML). Также ниже
            double hANDLE_INNER_DISK_PROP = 1.05;
            double changeInnerDiameterDisk = diameterHandle * hANDLE_INNER_DISK_PROP;

            // Создаем расстояние от центра до начала выдавливания дисков
            // TODO: RSDN
            double eXTRUSION_LENGTH_DISK = 85.0;

            // Физическое расстояние между дисками
            // TODO: RSDN
            double lENGHT_BTW_DISKS = 2.0;

            // Расстояние между началом выдавливания дисков
            double offset = widthDisk + lENGHT_BTW_DISKS;

            for (int i = 0; i < amountDisk; i++)
            {
                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                var sketch = wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, eXTRUSION_LENGTH_DISK + (i * offset));
                var document2d = (ksDocument2D)sketch.BeginEdit();

                // Внешний круг радиусом 200
                document2d.ksCircle(0, 0, outerDiameterDisk, 1);

                // Внутренний круг радиусом 50
                document2d.ksCircle(0, 0, changeInnerDiameterDisk, 1);
                sketch.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                wrapper.CreateExtrusion(sketch, widthDisk, false);
            }

            for (int i = 0; i < amountDisk; i++)
            {
                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                var sketch = wrapper.CreateSketch(
                    Obj3dType.o3d_planeXOZ,
                    (-1 * eXTRUSION_LENGTH_DISK) - widthDisk - (i * offset));
                var document2d = (ksDocument2D)sketch.BeginEdit();

                // Внешний круг радиусом 200
                document2d.ksCircle(0, 0, outerDiameterDisk, 1);

                // Внутренний круг радиусом 50
                document2d.ksCircle(0, 0, changeInnerDiameterDisk, 1);
                sketch.EndEdit();

                // Выдавливаем первый диск в положительном направлении на 20
                wrapper.CreateExtrusion(sketch, widthDisk, false);
            }
        }

        // TODO: XML
        private void BuildDisks30Ladder()
        {
            double amountDisk = parameters.GetParameter(ParameterType.AmountDisk);
            double outerDiameterDisk = parameters.GetParameter(ParameterType.OuterDiameterDisk);
            double widthDisk = parameters.GetParameter(ParameterType.WidthDisk);
            double diameterHandle = parameters.GetParameter(ParameterType.DiameterHandle);

            // Создаем внутренний диаметр диска чуть больше диаметра рукоятки
            // TODO: RSDN
            double hANDLE_INNER_DISK_PROP = 1.05;
            double changeInnerDiameterDisk = diameterHandle * hANDLE_INNER_DISK_PROP;

            // Создаем расстояние от центра до начала выдавливания дисков
            // TODO: RSDN
            double eXTRUSION_LENGTH_DISK = 85.0;

            // Физическое расстояние между дисками
            // TODO: RSDN
            double lENGHT_BTW_DISKS = 2.0;

            // Присваиваем значение внешнего радиуса временной переменной, которая будет уменьшаться.
            double prevOuterDiameter = outerDiameterDisk;

            // Расстояние между началом выдавливания дисков
            double offset = widthDisk + lENGHT_BTW_DISKS;

            double sIN_60 = 0.866;

            for (int i = 0; i < amountDisk; i++)
            {
                double newOuterDiameter;
                if (i != 0)
                {
                    newOuterDiameter = prevOuterDiameter * sIN_60;
                }
                else
                {
                    newOuterDiameter = prevOuterDiameter;
                }

                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                var sketch = wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, eXTRUSION_LENGTH_DISK + (i * offset));
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

            prevOuterDiameter = outerDiameterDisk;

            for (int i = 0; i < amountDisk; i++)
            {
                double newOuterDiameter;
                if (i != 0)
                {
                    newOuterDiameter = prevOuterDiameter * sIN_60;
                }
                else
                {
                    newOuterDiameter = prevOuterDiameter;
                }

                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                var sketch = wrapper.CreateSketch(
                    Obj3dType.o3d_planeXOZ,
                    (-1 * eXTRUSION_LENGTH_DISK) - widthDisk - (i * offset));
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

        // TODO: XML
        private void BuildDisks45Ladder()
        {
            double amountDisk = parameters.GetParameter(ParameterType.AmountDisk);
            double outerDiameterDisk = parameters.GetParameter(ParameterType.OuterDiameterDisk);
            double widthDisk = parameters.GetParameter(ParameterType.WidthDisk);
            double diameterHandle = parameters.GetParameter(ParameterType.DiameterHandle);

            // Создаем внутренний диаметр диска чуть больше диаметра рукоятки
            // TODO: RSDN
            double hANDLE_INNER_DISK_PROP = 1.05;
            double changeInnerDiameterDisk = diameterHandle * hANDLE_INNER_DISK_PROP;

            // Создаем расстояние от центра до начала выдавливания дисков
            // TODO: RSDN
            double eXTRUSION_LENGTH_DISK = 85.0;

            // Физическое расстояние между дисками
            // TODO: RSDN
            double lENGHT_BTW_DISKS = 2.0;

            // Присваиваем значение внешнего радиуса временной переменной, которая будет уменьшаться.
            double prevOuterDiameter = outerDiameterDisk;

            // Расстояние между началом выдавливания дисков
            double offset = widthDisk + lENGHT_BTW_DISKS;

            double sIN_45 = 0.707;

            for (int i = 0; i < amountDisk; i++)
            {
                double newOuterDiameter;
                if (i != 0)
                {
                    newOuterDiameter = prevOuterDiameter * sIN_45;
                }
                else
                {
                    newOuterDiameter = prevOuterDiameter;
                }

                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                var sketch = wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, eXTRUSION_LENGTH_DISK + (i * offset));
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

            prevOuterDiameter = outerDiameterDisk;

            for (int i = 0; i < amountDisk; i++)
            {
                double newOuterDiameter;
                if (i != 0)
                {
                    newOuterDiameter = prevOuterDiameter * sIN_45;
                }
                else
                {
                    newOuterDiameter = prevOuterDiameter;
                }

                // Создаем эскиз для первого диска на расстоянии 120 от начала координат
                var sketch = wrapper.CreateSketch(
                    Obj3dType.o3d_planeXOZ,
                    (-1 * eXTRUSION_LENGTH_DISK) - widthDisk - (i * offset));
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

        private void BuildFastening()
        {
            // double WidthDisk = _parameters.GetParameter(ParameterType.WidthDisk);
            double widthFasten =    parameters.GetParameter(ParameterType.WidthFasten);
            double diameterHandle = parameters.GetParameter(ParameterType.DiameterHandle);

            // Соотношение диаметров крепления и рукоятки
            // TODO: RSDN
            double rATIO_DIAMETER_HANDLE_FASTEN = 2.0;
            double changeDiameterFasten = diameterHandle * rATIO_DIAMETER_HANDLE_FASTEN;

            // Расстояние от центра, где будет начинать выдавливаться крепления
            // TODO: RSDN
            double lENGTH_FASTEN_CREATE = 75.0;

            // Создаем эскиз для первого диска на расстоянии 120 от начала координат
            var sketch1 = wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, (-1 * lENGTH_FASTEN_CREATE) - widthFasten);

            // TODO: RSDN
            var document2d_1 = (ksDocument2D)sketch1.BeginEdit();
            document2d_1.ksCircle(0, 0, changeDiameterFasten, 1); // Внешний круг радиусом 200

            sketch1.EndEdit();

            // Выдавливаем первый диск в положительном направлении на 20
            wrapper.CreateExtrusion(sketch1, widthFasten, false);

            var sketch2 = wrapper.CreateSketch(Obj3dType.o3d_planeXOZ, 75);

            // TODO: RSDN
            var document2d_2 = (ksDocument2D)sketch2.BeginEdit();
            document2d_2.ksCircle(0, 0, changeDiameterFasten, 1); // Внешний круг радиусом 200

            sketch2.EndEdit();

            // Выдавливаем первый диск в положительном направлении на 20
            wrapper.CreateExtrusion(sketch2, widthFasten, false);
        }
    }
}
